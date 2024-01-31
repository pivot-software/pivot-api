using System.ComponentModel.DataAnnotations.Schema;
using ERP.Shared.Abstractions;

namespace ERP.Domain.Entities;

public class ProfilePermission: BaseEntity, IAggregateRoot
{

    [Column("profile_id")]
    public Guid ProfileId { get; private set; }

    [Column("permission_id")]
    public Guid PermissionId { get; private set; }
}
