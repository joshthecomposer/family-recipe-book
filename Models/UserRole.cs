#pragma warning disable 8618


namespace MyApp.Models;
public class UserRole : BaseEntity
{
    public int UserId { get; set; }
    public User? User { get; set; }

    public int RoleId { get; set; }
    public Role? Role { get; set; }
}
