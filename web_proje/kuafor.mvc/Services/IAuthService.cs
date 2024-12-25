using kuafor.mvc.Dtos;
using Microsoft.AspNetCore.Identity;

namespace kuafor.mvc.Services
{
    public interface IAuthService
    {
        IEnumerable<IdentityRole> Roles { get; }
        IEnumerable<IdentityUser> GetAllUsers();
        Task<IdentityResult> CreateUser(UserDtoForCreation userDto);
        Task<UserDtoForUpdate> GetOneUserForUpdate(string userName);
        Task<IdentityUser> GetOneUser(string userName);
        Task Update(UserDtoForUpdate userDto);
        Task<IdentityResult> ResetPassword(ResetPasswordDto model);
        Task<IdentityResult> DeleteOneUser(string userName);
    }
}
