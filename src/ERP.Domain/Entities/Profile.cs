using System.ComponentModel.DataAnnotations.Schema;
namespace ERP.Domain.Entities;

public class Profile
{
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string ProfileName { get; set; } = null!;

    [Column("description")]
    public string Description { get; set; } = null!;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("deleted_by")]
    public Guid? DeletedBy { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }
}
