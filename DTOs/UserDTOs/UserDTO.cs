using MyApp.Models;

namespace MyApp.DTOs.UserDTOs;
public class UserDTO
{
    public int UserId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string Email { get; set; }

    public UserDTO(User user)
    {
        UserId = user.UserId;
        FirstName = user.FirstName;
        LastName = user.LastName;
        Email = user.Email;
    }
}