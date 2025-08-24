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
        TdtoUser GeTdtoUserByUserName(string UserName);
        Task<SrvResponse> SignInAsync(DtoUserLogin Model);
        Task<SrvResponse> SignOut();
        Task<SrvResponse> updateUser(TdtoUser dto_user);
        SrvResponse GetUsres(string Searchtext, int PageIndex = 1, int PageSize = 20);
        Task<SrvResponse> ChangePassword(Dto_ChangePassword changePassword);
        Task<SrvResponse> ForceChangePassword(string UserId, string Password);
        Task<SrvResponse> updateUserRole(string userId, string OldRoleName, string NewRoleName);
    }
}
