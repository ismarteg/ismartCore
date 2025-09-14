    using AutoMapper;
using ISCore.DataBase.Entities.Contracts;
using ISCore.Entities.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ISCore.DAL.interfaces;
using ISCore.Services.Interface;
using ISCore.Services.DTO.Users;
using ISCore.Services.Mapper;
using ISCore.Utils.Enums;
using ISCore.Utils.Emails;
using ISCore.Entities.Users.Const;
using Microsoft.EntityFrameworkCore;
using ISCore.Services.HelperClass;

namespace ISCore.Services.Users
{
    public class GSrv_Users<TUser, TRole, TdtoUser> : IUsersBaseService<TUser, TRole, TdtoUser>
      where TUser : IdentityUser,IAppUser ,new()
      where TRole : IdentityRole
        where TdtoUser : IDTOUser
    {

        public IMapper _Mapper { get; }
        public SrvResponse _response { get; }
        public IUnitOfWork _UnitOfWork { get; set; }

        private readonly SignInManager<TUser> _SignInManager;
        private readonly UserManager<TUser> _UserManager;
        private readonly RoleManager<TRole> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public readonly IEmailServices _emailSender;
       
        public GSrv_Users(SignInManager<TUser> signInManager, UserManager<TUser> userManager,
            RoleManager<TRole> roleManager, IEmailServices emailSender,
            IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork,
            IMapper mapper) 
        {
            _SignInManager = signInManager;
            _UserManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _UnitOfWork = unitOfWork;
            _Mapper = mapper;
            _response = new SrvResponse();
        }
        
        //------private Methods 
        private async Task<ValideUserResponse> ValidateUser(string UserName, string Password)
        {
            ValideUserResponse Repo= new ValideUserResponse();
            string mesasge = "";
            TUser _user = await _UserManager.FindByNameAsync(UserName);
            Repo.IsValid = true;
            Repo.Message = "Valid User";
            if (_user == null)
            {
                Repo.IsValid= false;
                Repo.Message= "User Not Found,";
            }
            var validPassword = await _UserManager.CheckPasswordAsync(_user, Password);
           // await CountAccessFailedAsync(_user, _user != null && validPassword);
            if (!validPassword)
            {
                Repo.IsValid= false;
                Repo.Message += "Password is incorrect";
            }
            
            return Repo;
        }
        private async Task CountAccessFailedAsync(TUser _user, bool Validation)
        {
            if (Validation)
            {
                await _UserManager.ResetAccessFailedCountAsync(_user);
            }
            else if (_user != null)
            {
                await _UserManager.AccessFailedAsync(_user);
            }
        }
        //------End private Methods 

        //------Public Methods
        public async Task<bool> EmailConfirmation(string UserName)
        {
            TUser _user = await _UserManager.FindByNameAsync(UserName);
            return (_user.EmailConfirmed == true);
        }

        public async Task<TUser> GetUser(ClaimsPrincipal User)
        {
            var user = await _UserManager.GetUserAsync(User);
            return user;
        }
        public async Task<TUser> GetUser(string UserName)
        {
            TUser _user = await _UserManager.FindByNameAsync(UserName);
            return _user;
        }
        public async Task<TUser> GetUserByID(string Id)
        {
            TUser _user = await _UserManager.FindByIdAsync(Id);
            return _user;
        }

        public List<string> GetRoles(string UserId)
        {
            TUser user = GetUserByID(UserId).Result;
            var roles = _UserManager.GetRolesAsync(user).Result;
            return roles.ToList();
        }
        public List<string> GetRoles(ClaimsPrincipal User)
        {
            TUser user = GetUser(User).Result;
            var roles = _UserManager.GetRolesAsync(user).Result;
            return roles.ToList();
        }



        public async Task<SrvResponse> CreateUser(TdtoUser createUserViewModel)
        {
            try
            {
                // Check role is exists

                var roleExists = await _roleManager.RoleExistsAsync(createUserViewModel.Role);
                if (!roleExists)
                {
                    // Role doesn't exist, handle accordingly (e.g., return an error)
                    return _response.Error($"Role '{createUserViewModel.Role}' does not exist");
                }

                var emailExists = await _UserManager.FindByEmailAsync(createUserViewModel.Email);
                if (emailExists is not null)
                {
                    return _response._Return(
                        ResponseCode.BadRequest,
                        $"Email '{emailExists.Email}' does exist",
                        nameof(createUserViewModel.Email));
                }
                TUser user = new TUser()
                {
                    Email = createUserViewModel.Email,
                    UserName = createUserViewModel.Email,
                    FirstName = createUserViewModel.FirstName,
                    LastName = createUserViewModel.LastName,
                    EmailConfirmed = true
                };
                user.CreationDate = DateTime.Now;
                user.UserName = user.Email;

                var result = await _UserManager.CreateAsync(user, createUserViewModel.Password);
                if (!result.Succeeded)
                {
                    return _response._Return(ResponseCode.BadRequest, String.Join(",", result.Errors.Select(x => x.Description)));

                }
                result = await _UserManager.AddToRolesAsync(user, createUserViewModel.UserRolesString);
                if (!result.Succeeded)
                {
                    return _response._Return(ResponseCode.BadRequest, String.Join(",", result.Errors.Select(x => x.Description)));
                }
                #region SendEmail
                string code = HttpUtility.UrlEncode(await _UserManager.GenerateEmailConfirmationTokenAsync(user));
                var request = _httpContextAccessor.HttpContext.Request;
                var domain = $"{request.Scheme}://{request.Host}";
                var callbackUrl = $"{domain}/Account/ConfirmEmail?code=" + code + "&email=" + user.Email;
                #endregion

                #region Send email to clint to verify email
                var token = HttpUtility.UrlEncode(await _UserManager.GenerateEmailConfirmationTokenAsync(user));

                EmailMessage emailMessage = new EmailMessage()
                {
                    Subject = "Confirm Email",
                    ToEmail = user.Email,
                    EmailBody = $"<p>Dear {user.FirstName},</p>" +
                                $"<p>Thank you for registering with us. Please confirm your email address by clicking the link below:</p>" +
                                $"<p><a href='{callbackUrl}'>Confirm Email</a></p>" +
                                $"<p>If you did not register, please ignore this email.</p>" +
                                $"<p>Best regards,</p>"
                };
                await _emailSender.Send(emailMessage);
                #endregion
                return _response.Success(user);
            }
            catch (Exception ex)
            {
                return _response.Error(ex.Message);
            }
        }

        public TdtoUser GeTdtoUserById(string Id)
        {
            TUser user = GetUserByID(Id).Result;
            return user.MapItem<TdtoUser>();
        }
        public TdtoUser GeTdtoUserByUserName(string UserName)
        {
            TUser user = GetUser(UserName).Result;
            return  user.MapItem<TdtoUser>();
        }

        public async Task<SrvResponse> ChangePassword(Dto_ChangePassword changePassword)
        {
            var valideUser = await ValidateUser(changePassword.Username, changePassword.OldPassword);

            if (valideUser.IsValid)
            {
                return _response.Error("User Name or Password is incorrect");
            }
            var user = await _UserManager.FindByNameAsync(changePassword.Username);
            if (user == null)
                return _response.Error("User not found");

            var status = await _UserManager.ChangePasswordAsync(user, changePassword.OldPassword, changePassword.NewPassword);
            if (!status.Succeeded)
            {
                return _response.Error(string.Join(", ", status.Errors.Select(e => e.Description)));
            }
            return _response.Success();
        }
        public async Task<SrvResponse> ForceChangePassword(string UserId,string Password)
        {
            try
            {
                var user = await _UserManager.FindByIdAsync(UserId);
                var RemoveResult = await _UserManager.RemovePasswordAsync(user);
                if (RemoveResult.Succeeded)
                {
                    var addpassword = await _UserManager.AddPasswordAsync(user, Password);
                    if (addpassword.Succeeded)
                    {
                        return _response.Success();
                    }
                    else
                    {
                        return _response.Error(string.Join(", ", addpassword.Errors.Select(x => x.Description)));
                    }
                }
                else
                {
                    return _response.Error(string.Join(", ", RemoveResult.Errors.Select(x => x.Description)));
                }
            }
            catch (Exception ex)
            {
                return _response.Error(ex.Message);
            }
          
        }

        public async Task<SrvResponse> SignInAsync(DtoUserLogin Model)
        {

            try
            {
                var valideUser = await ValidateUser(Model.UserName, Model.Password);
                if (!valideUser.IsValid)
                {
                    return _response.Error(ResponseCode.LoginFaild,valideUser.Message);
                }
                if (!await EmailConfirmation(Model.UserName))
                {
                    return _response.Error(ResponseCode.emailNotConfirmed,"Please Email Confirmation");
                }


                var user = await _UserManager.FindByNameAsync(Model.UserName);
                user.LastLoginDate = DateTime.Now;


                if (user.IsActive == false)
                {
                    return _response.Error(ResponseCode.UserIsInActive,"This User Is Disabled");
                }


                var result = await _SignInManager.PasswordSignInAsync(Model.UserName, Model.Password, Model.IsRememberMe,
                    lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var roles = await _UserManager.GetRolesAsync(user);
                    return _response.Success(roles);
                }
                else
                {
                    return _response.Error(ResponseCode.LoginFaild,"Failed to sign in");
                }
            }
            catch (Exception ex)
            {
                return _response.Error(ex.Message);
            }
        }


        public async Task<SrvResponse> SignOut()
        {
            try
            {
                _SignInManager.SignOutAsync();
                return _response.Success();
            }
            catch (Exception ex)
            {
                return _response.Error(ex.Message);
            }
        }

        public SrvResponse GetUsres(string Searchtext, int PageIndex = 1, int PageSize = 20)
        {
            int Count = 0;
            try
            {
                List<AppUser> users = _UnitOfWork.repo<AppUser>().GetPage(PageIndex, PageSize,out Count,
                    predicate: x =>
                    (x.UserName.Contains(Searchtext) ||
                    x.Email.Contains(Searchtext) ||
                    x.FirstName.Contains(Searchtext) ||
                    x.LastName.Contains(Searchtext) ||
                    Searchtext == null)
                    && x.UserRoles.Any(x => x.Role.Name != Cnst_Roles.SuperAdmin),
                    includes: inc => inc.Include(Role =>
                Role.UserRoles).ThenInclude(x => x.Role)).ToList();

                List<TdtoUser> _Users = _Mapper.Map<List<TdtoUser>>(users);
                return _response.Success(_Users, Count);
            }
            catch (Exception ex)
            {
                return _response.Error(ex.Message);
            }
        }
        public async Task<SrvResponse> updateUser(TdtoUser dto_user)
        {
            var user = await GetUserByID(dto_user.Id);
            //user = dto_user.MapItem<TUser>();
            _Mapper.Map(dto_user, user);
            try
            {
                var result = await _UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return _response.Success();
                }
                return _response.Error(result.getError(),innerObject:result.Errors);
            }
            catch (Exception ex)
            {
                return _response.Error(ex.Message);
            }
        }
        public async Task<SrvResponse> updateUserRole(string userId, string OldRoleName, string NewRoleName)
        {
            try
            {
                TUser user = await _UserManager.FindByIdAsync(userId);
                var userRoles = await _UserManager.GetRolesAsync(user);
                _UserManager.RemoveFromRolesAsync(user, userRoles).Wait();
                IdentityResult RoleAddResult = _UserManager.AddToRoleAsync(user, NewRoleName).Result;
                return _response.Success();

            }
            catch (Exception ex)
            {
                return _response.Error(ex.Message);
            }
        }

    }
}
