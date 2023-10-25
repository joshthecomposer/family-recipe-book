using System.ComponentModel.DataAnnotations;

namespace MyApp.Models;
public class Role : BaseEntity
{
    [Key]
    public int RoleId { get; set; }
    public Roles Value { get; set; } = Roles.USER;

    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
