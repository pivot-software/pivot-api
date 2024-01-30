using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ERP.Shared.Abstractions;

namespace ERP.Domain.Entities;

[Table("profile")]
public class Profile: BaseEntity, IAggregateRoot
{
    [Column("name")]
    public string ProfileName { get; set; } = null!;

    [Column("description")]
    public string Description { get; set; } = null!;
    
    [Column("users")]
    public ICollection<User> Users { get; } = new List<User>(); 
    
    [Column("profiles_permissions")]
    public List<ProfilePermission> ProfilePermissions { get; } = new();

   
}
