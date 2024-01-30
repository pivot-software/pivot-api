using ERP.Domain.Entities;
using ERP.Shared.Abstractions;
namespace ERP.Domain.Repositories;

public interface IProfileRepository : IAsyncRepository<Profile>
{
    Task<Profile?> GetProfileById(Guid id);
}
