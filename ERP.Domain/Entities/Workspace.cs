using System;
using System.ComponentModel.DataAnnotations.Schema;
using ERP.Shared.Abstractions;

namespace ERP.Domain.Entities
{
    [Table("workspaces")]
    public class Workspace : BaseEntity, IAggregateRoot
    {

        public Workspace()
        {
        }

        public Workspace(string businessName, string businessLogo, string businessColor, string templateMode, Guid adminId)
        {
            CreatedAt = DateTime.UtcNow;
            BusinessName = businessName;
            BusinessLogo = businessLogo;
            BusinessColor = businessColor;
            TemplateMode = !string.IsNullOrEmpty(templateMode) ? templateMode[0] : '0';
            AdminId = adminId;
        }


        [Column("id")]
        public Guid Id { get; } = Guid.NewGuid();

        [Column("business_name")]
        public string BusinessName { get; set; } = null!;

        [Column("business_logo")]
        public string BusinessLogo { get; set; } = null!;

        [Column("business_color")]
        public string BusinessColor { get; set; }

        [Column("template_mode")]
        public char TemplateMode { get; set; } = '0';

        [Column("admin_id")]
        public Guid? AdminId { get; set; } = null!;

        [ForeignKey("AdminId")]
        public User? Admin { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}
