using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ERP.Domain.Entities;

[Table("permissions")]
public class Permissions
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string PermissionName { get; set; } = null!;

    [Column("description")]
    public string Description { get; set; } = null!;

    [Column("resource")]
    public string Resource { get; set; } = null!;

    [Column("action_type")]
    public string ActionType { get; set; } = null!;
    
    [Column("profiles_permissions")]
    public List<ProfilePermission> ProfilePermissions { get; } = new();

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("deleted_by")]
    public Guid? DeletedBy { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }
}
