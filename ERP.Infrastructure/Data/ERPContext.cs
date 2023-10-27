using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;
using ERP.Domain.Entities;
using ERP.Infrastructure.Extensions;
using ERP.Shared.AppSettings;

namespace ERP.Infrastructure.Data.Context;

public sealed class ErpContext : DbContext
{
    private readonly string _collation;

    public ErpContext(DbContextOptions<ErpContext> dbOptions)
        : base(dbOptions)
    {
    }

    public ErpContext(IOptions<ConnectionStrings> options, DbContextOptions<ErpContext> dbOptions)
        : this(dbOptions)
    {

        _collation = options.Value.Collation;

    }

    public override ChangeTracker ChangeTracker
    {
        get
        {
            base.ChangeTracker.LazyLoadingEnabled = false;
            base.ChangeTracker.CascadeDeleteTiming = CascadeTiming.OnSaveChanges;
            base.ChangeTracker.CascadeDeleteTiming = CascadeTiming.OnSaveChanges;
            return base.ChangeTracker;
        }
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Workspace> Workspaces => Set<Workspace>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (!string.IsNullOrWhiteSpace(_collation))
            modelBuilder.UseCollation(_collation);

        modelBuilder
            .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly())
            .RemoveCascadeDeleteConvention();
    }
}
