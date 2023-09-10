using MyApp.Models;
namespace MyApp.Services;
public interface IUserService
{
    Task<User> CreateAsync(User user);
    Task<User?> DeactivateUserByIdAsync(int id);
}