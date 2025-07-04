using AutoMapper;
using DALCore.UnitofWorks.Contracts;
using DbCore.Entities.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ServicesCore.Email;
using ServicesCore.DTO.Users;
using ServicesCore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ServicesCore.Users
{
    public class Srv_Users : BaseService<AppUser,DTOUser>
    {
        private readonly SignInManager<AppUser> _SignInManager;
        private readonly UserManager<AppUser> _UserManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public readonly IEmailSender _emailSender;
        public Srv_Users(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager, IEmailSender emailSender,
            IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork,
             IMapper mapper) : base(unitOfWork, mapper)
        {
            _SignInManager = signInManager;
            _UserManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _roleManager = roleManager;
            _emailSender = emailSender;
        }

        private async Task<bool> ValidateUser(string UserName, string Password)
        {
            AppUser _user = await _UserManager.FindByNameAsync(UserName);
            var validPassword = await _UserManager.CheckPasswordAsync(_user, Password);
            await CountAccessFailedAsync(_user, _user != null && validPassword);
            return (_user != null && validPassword);
        }
        private async Task CountAccessFailedAsync(AppUser _user, bool Validation)
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
        public async Task<bool> EmailConfirmation(string UserName)
        {
            AppUser _user = await _UserManager.FindByNameAsync(UserName);
            return (_user.EmailConfirmed == true);
        }

        public async Task<AppUser> GetUser(ClaimsPrincipal User)
        {
            var user = await _UserManager.GetUserAsync(User);
            return user;
        }
        public async Task<AppUser> GetUser(string UserName)
        {
            AppUser _user = await _UserManager.FindByNameAsync(UserName);
            return _user;
        }
        public async Task<AppUser> GetUserByID(string Id)
        {
            AppUser _user = await _UserManager.FindByIdAsync(Id);
            return _user;
        }

        public List<string> GetRoles(string UserId)
        {
            AppUser user = GetUserByID(UserId).Result;
            var roles = _UserManager.GetRolesAsync(user).Result;
            return roles.ToList();
        }
        public List<string> GetRoles(ClaimsPrincipal User)
        {
            AppUser user = GetUser(User).Result;
            var roles = _UserManager.GetRolesAsync(user).Result;
            return roles.ToList();
        }



        public async Task<SrvResponse> CreateUser(DTOUser createUserViewModel)
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
                AppUser user = new AppUser()
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

                //var appUser = new MailRequest
                //{
                //    ToEmail = user.Email,
                //    Subject = "Email verification",
                //    Token = $"{domain}/api/Account/ConfirmEmailLink?token=" + token + "&email=" + user.Email
                //};
                //await _mailService.SendEmailAsync(appUser);
                #endregion
                return _response.Success(user);

            }
            catch (Exception ex)
            {
                return _response.Error(ex.Message);

            }
        }


        public DTOUser GetDTOUserById(string Id)
        {
            AppUser user = GetUserByID(Id).Result;
            return _Mapper.Map<DTOUser>(user);
        }
        public DTOUser GetDTOUserByUserName(string UserName)
        {
            AppUser user = GetUser(UserName).Result;
            return _Mapper.Map<DTOUser>(user);
        }


        public async Task<SrvResponse> SignInAsync(DTOUserLogin Model)
        {

            try
            {
                if (!await ValidateUser(Model.UserName, Model.Password))
                {
                    return _response.Error("User Name or Password is incorrect");
                }
                if (!await EmailConfirmation(Model.UserName))
                {
                    return _response.Error("Please Email Confirmation");

                }


                var user = await _UserManager.FindByNameAsync(Model.UserName);
                user.LastLoginDate = DateTime.Now;


                if (user.IsActive == false)
                {
                    return _response.Error("This User  Is Disabled");
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
                    return _response.Error($"Failed to sign in");
                } 
            }
            catch (Exception ex)
            {
                return _response.Error(ex.Message);
            }
        }


    }
}
