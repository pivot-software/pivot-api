using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ERP.Shared.Abstractions;

namespace ERP.Domain.Entities
{
    [Table("users")]
    public class User : BaseEntity, IAggregateRoot
    {

        public User(string email, string username, string password)
        {
            Email = email;
            Username = username;
            Password = password;
            CreatedAt = DateTime.UtcNow;
            RevokeIn = DateTime.MinValue;
        }


        [Key]
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

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

        [Column("token_refresh")]
        public string? TokenRefresh { get; set; }

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
