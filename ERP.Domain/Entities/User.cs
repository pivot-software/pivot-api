using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Domain.Entities
{
    [Table("users")]
    public class User
    {
        [Column("id")]
        public Guid UserId { get; set; } = Guid.NewGuid();

        [Column("email")]
        public string Email { get; set; } = null!;

        [Column("username")]
        public string Username { get; set; } = null!;

        [Column("password")]
        public string Password { get; set; } = null!;

        [Column("avatar")]
        public string Avatar { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("invited_by")]
        public Guid? InvitedBy { get; set; }

        [Column("deleted_by")]
        public Guid? DeletedBy { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }


    }
}
