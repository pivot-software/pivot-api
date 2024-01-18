using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ERP.Shared.Abstractions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ERP.Domain.Entities
{
    [Index(nameof(Email), IsUnique = true), Index(nameof(Username), IsUnique = true)]
    [Table("users")]
    public class User : BaseEntity, IAggregateRoot
    {
        public User(string email, string username, string password, int profileId)
        {
            Email = email;
            Username = username;
            Password = password;
            ProfileId = profileId;
            CreatedAt = DateTime.UtcNow;
            RevokeIn = DateTime.MinValue;
        }

        [Key]
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "O campo 'Email' é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de email inválido.")]
        [Column("email")]
        public string Email { get; set; } = null!;

        [Column("username")]
        public string Username { get; set; } = null!;

        [Column("password")]
        public string Password { get; set; } = null!;

        [Column("avatar")]
        public string? Avatar { get; set; }

        [Column("token")]
        public string? Token { get; set; }

        [Column("profile_id")]
        public int ProfileId { get; set; }
        
        [JsonIgnore]
        [Column("profile")]
        public Profile Profile { get; set; } = null!;

        [Column("token_refresh")]
        public string? TokenRefresh { get; set; }

        [Column("can_send_email")]
        public bool? CanSendEmail { get; set; } = true;

        [Column("can_send_sms")]
        public bool? CanSendSms { get; set; } = true;

        [Column("can_send_system_notification")]
        public bool? CanSendSystemNotification { get; set; } = true;

        [Column("revoke_in")]
        public DateTime RevokeIn { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("invited_by")]
        public Guid? InvitedBy { get; set; }

        [Column("deleted_by")]
        public Guid? DeletedBy { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        // Método para adicionar um token JWT
        public void AddToken(string token, string refreshToken, DateTime expiration)
        {
            Token = token;
            TokenRefresh = refreshToken;
            RevokeIn = expiration.ToUniversalTime();
        }

        // Método para revogar o token
        public void RevokeToken()
        {
            Token = null;
            TokenRefresh = string.Empty;
            RevokeIn = DateTime.MinValue; // Ou uma data no passado
        }

        // Método para verificar se o token foi revogado
        public bool IsTokenRevoked()
        {
            return string.IsNullOrEmpty(Token) && RevokeIn <= DateTime.UtcNow;
        }

        // Método para atualizar o token
        public void UpdateToken(string newToken, string newRefreshToken, DateTime newExpiration)
        {
            Token = newToken;
            TokenRefresh = newRefreshToken;
            RevokeIn = newExpiration;
        }

    }
}
