using MyApp.DataTransfer.Users;
using MyApp.Models;
namespace MyApp.Services;
public interface IUserService
{
    Task<bool> CreateAsync(User user);
    Task<User?> DeactivateUserByIdAsync(int id);
    Task<UserDto?> GetByIdAsync(int id);
    Task<UserDto?> ValidateUserPassword(LoginUser loginUser);
    Task<UserDto> UpdatePasswordAsync(PasswordUpdateDto passUpdate, int userId);
}
