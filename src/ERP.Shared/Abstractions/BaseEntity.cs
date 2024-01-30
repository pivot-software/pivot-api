using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Shared.Abstractions;

/// <summary>
/// Entidade com a chave (PK) tipada em <see cref="Guid"/>.
/// </summary>
public abstract class BaseEntity : IEntityKey<Guid>
{
    [Key]
    [Column("id")]
    public Guid Id { get; private init; } = Guid.NewGuid();
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("deleted_by")]
    public Guid? DeletedBy { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }
}
