using Microsoft.EntityFrameworkCore;
using ERP.Domain.Entities;
using ERP.Domain.Repositories;
using ERP.Infrastructure.Data.Context;
using ERP.Infrastructure.Data.Repositories.Common;

namespace ERP.Infrastructure.Data.Repositories;

public class ProfileRepository : EfRepository<Profile>, IProfileRepository
{
    public ProfileRepository(ErpContext context) : base(context)
    {
    }
    public async Task<Profile?> GetProfileById(int id)
    {
        return await DbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);
    }
}
