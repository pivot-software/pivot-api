using Microsoft.EntityFrameworkCore;
using ERP.Domain.Entities;
using ERP.Domain.Repositories;
using ERP.Domain.ValueObjects;
using ERP.Infrastructure.Data.Context;
using ERP.Infrastructure.Data.Repositories.Common;

namespace ERP.Infrastructure.Data.Repositories;

public class UserRepository : EfRepository<User>, IUserRepository
{
    public UserRepository(ErpContext context) : base(context)
    {
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        var user = await DbSet.FirstOrDefaultAsync(u => u.Email == email);

        return user;
    }

    public async Task<IUserNotificationSettings> GetNotificationSettings(Guid id)
    {
        var settings = new UserNotificationSettings(true, true, true, true);

        return await Task.FromResult(settings);
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        return await DbSet.Include(b => b.Profile)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<User?> GetUserById(Guid id)
    {
        return await DbSet
            .Include(b => b.Profile)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);
    }

}
