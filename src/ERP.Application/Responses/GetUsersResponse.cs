using ERP.Domain.DTO;
using ERP.Domain.Entities;
namespace ERP.Application.Responses;

public class GetUserResponse
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string? Avatar { get; set; }
    public ProfileDto Profile { get; set; }
    public DateTime CreatedAt { get; set; }


    public GetUserResponse(Guid id, string email, string username, string? avatar, ProfileDto profile, DateTime createdAt)
    {
        Id = id;
        Email = email;
        Username = username;
        Avatar = avatar;
        Profile = profile;
        CreatedAt = createdAt;
    }
}
