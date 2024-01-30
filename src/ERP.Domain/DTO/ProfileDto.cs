namespace ERP.Domain.DTO;

public class ProfileDto
{
    public Guid Id { get; set; }
    public string ProfileName { get; set; }
    public string Description { get; set; }

    public ProfileDto(Guid id, string profileName, string description)
    {
        Id = id;
        ProfileName = profileName;
        Description = description;
    }
}
