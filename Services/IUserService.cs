using MyApp.DTOs.TokenDTOs;
using MyApp.DTOs.UserDTOs;
using MyApp.Models;
namespace MyApp.Services;
public interface IUserService
{
    Task<bool> CreateAsync(User user);
    Task<User?> DeactivateUserByIdAsync(int id);
    Task<UserDTO?> GetByIdAsync(int id);
    Task<UserDTO?> ValidateUserPassword(LoginUser loginUser);
}