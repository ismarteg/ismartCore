using AutoMapper;
using ISCore.DataBase.Entities.Contracts;
using ISCore.DTO.Users;
using ISCore.Emails;
using ISCore.Enums;
using ISCore.Mapper;
using ISCore.Responses;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Web;


namespace ISCore.Interfaces
{
    public interface IUsersBaseService<TUser, TRole, TdtoUser>
        where TUser : IdentityUser, IAppUser, new()
      where TRole : IdentityRole
        where TdtoUser : IDTOUser
    {
        protected IUnitOfWork _UnitOfWork { get; }
        protected IMapper _Mapper { get; }
        protected SrvResponse _response { get; }

        Task<bool> EmailConfirmation(string UserName);
        Task<TUser> GetUser(ClaimsPrincipal User);
        Task<TUser> GetUser(string UserName);
        Task<TUser> GetUserByID(string Id);
        List<string> GetRoles(string UserId);
        List<string> GetRoles(ClaimsPrincipal User);
        Task<SrvResponse> CreateUser(TdtoUser createUserViewModel);
        TdtoUser GeTdtoUserById(string Id);
        IDTOUser GeTdtoUserByUserName(string UserName);
        Task<SrvResponse> SignInAsync<TdtoUserLogin>(TdtoUserLogin Model) where TdtoUserLogin : class;
        Task<SrvResponse> SignOut();

    }
}
