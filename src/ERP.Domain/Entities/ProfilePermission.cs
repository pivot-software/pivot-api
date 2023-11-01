using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ERP.Domain.Entities;

public class ProfilePermission
{
    [Key]
    [Column("id")]
    public int Id { get; private set; }

    [Column("profile_id")]
    public int ProfileId { get; private set; }

    [Column("permission_id")]
    public int PermissionId { get; private set; }
}
