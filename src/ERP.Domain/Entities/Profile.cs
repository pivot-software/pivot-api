using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ERP.Shared.Abstractions;

namespace ERP.Domain.Entities;

[Table("profile")]
public class Profile: BaseEntity, IAggregateRoot
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string ProfileName { get; set; } = null!;

    [Column("description")]
    public string Description { get; set; } = null!;
    
    [Column("users")]
    public ICollection<User> Users { get; } = new List<User>(); 
    
    [Column("profiles_permissions")]
    public List<ProfilePermission> ProfilePermissions { get; } = new();

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("deleted_by")]
    public Guid? DeletedBy { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }
   
}
