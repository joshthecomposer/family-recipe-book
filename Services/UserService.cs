using Microsoft.AspNetCore.Identity;
using MyApp.DataStorage;
using MyApp.DTOs.UserDTOs;
using MyApp.Models;

namespace MyApp.Services;
public class UserService : IUserService
{
    private readonly DBContext _db;

    public UserService(DBContext db)
    {
        _db = db;
    }

    public async Task<UserDTO?> GetByIdAsync(int id)
    {
        User? user = await _db.Users.FindAsync(id);

        if (user == null) return null;

        return new UserDTO(user);
    }

    public async Task<bool> CreateAsync(User user)
    {
        PasswordHasher<User> hasher = new();
        user.Password = hasher.HashPassword(user, user.Password);
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<User?> DeactivateUserByIdAsync(int id)
    {
        User? user = await _db.Users.FindAsync(id);

        if (user == null) return null;

        user.IsActive = false;
        await _db.SaveChangesAsync();
        return user;
    }
}