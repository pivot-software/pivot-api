namespace ERP.Application.Responses;

public class UserResponse
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }

    public UserResponse(Guid id, string email, string username)
    {
        Id = id;
        Email = email;
        Username = username;
    }
}
