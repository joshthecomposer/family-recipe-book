using Microsoft.AspNetCore.Identity;
using MyApp.DataStorage;
using MyApp.Models;

namespace MyApp.Services;
public class UserService : IUserService
{
    private readonly DBContext _db;

    public UserService(DBContext db)
    {
        _db = db;
    }

    public async Task<User> CreateAsync(User user)
    {
        PasswordHasher<User> hasher = new();
        user.Password = hasher.HashPassword(user, user.Password);
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return user;
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