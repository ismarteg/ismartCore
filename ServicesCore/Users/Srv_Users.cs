using AutoMapper;
using DALCore.UnitofWorks.Contracts;
using DbCore.Entities.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ServicesCore.Email;
using ServicesCore.TDO.Users;
using ServicesCore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ServicesCore.Users
{
    public class Srv_Users : BaseService<AppUser>
    {
        private readonly SignInManager<AppUser> _SignInManager;
        private readonly UserManager<AppUser> _UserManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public readonly IEmailSender _emailSender;
        public Srv_Users(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,IEmailSender emailSender,
            IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork,
             IMapper mapper) : base(unitOfWork, mapper)
        {
            _SignInManager = signInManager;
            _UserManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _roleManager = roleManager;
            _emailSender = emailSender;
        }

        public async Task<SrvResponse> CreateUser(TDOUser createUserViewModel)
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
    }
}
