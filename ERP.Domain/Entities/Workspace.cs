using System;
using System.ComponentModel.DataAnnotations.Schema;
using ERP.Shared.Abstractions;

namespace ERP.Domain.Entities
{
    [Table("workspaces")]
    public class Workspace : BaseEntity, IAggregateRoot
    {
        [Column("id")]
        public Guid WorkspaceId { get; set; } = Guid.NewGuid();

        [Column("business_name")]
        public string BusinessName { get; set; } = null!;

        [Column("business_logo")]
        public string BusinessLogo { get; set; } = null!;

        [Column("business_color")]
        public string BusinessColor { get; set; }

        [Column("template_mode")]
        public char TemplateMode { get; set; }

        [Column("admin_id")]
        public Guid AdminId { get; set; }

        [ForeignKey("AdminId")]
        public User Admin { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}
