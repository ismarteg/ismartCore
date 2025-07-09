using AutoMapper;
using ISCore.DAL.interfaces;
using ISCore.DataBase.Entities.Contracts;
using ISCore.Services.DTO.Users;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;


namespace ISCore.Services.Interface
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
