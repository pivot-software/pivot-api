using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ERP.Infrastructure.Data.Context;
using ERP.Infrastructure.Data.Repositories.Common;
using ERP.Shared.Abstractions;

namespace ERP.Infrastructure.Data.Repositories.Common;

public abstract class EfRepository<TEntity> : RepositoryBase<TEntity>, IAsyncRepository<TEntity>
    where TEntity : BaseEntity, IAggregateRoot
{
    protected EfRepository(ErpContext context) : base(context)
    {
    }

    public void Add(TEntity entity) =>
        DbSet.Add(entity);

    public void AddRange(IEnumerable<TEntity> entities) =>
        DbSet.AddRange(entities);

    public void Update(TEntity entity) =>
        DbSet.Update(entity);

    public void UpdateRange(IEnumerable<TEntity> entities) =>
        DbSet.UpdateRange(entities);

    public void Remove(TEntity entity) =>
        DbSet.Remove(entity);

    public void RemoveRange(IEnumerable<TEntity> entities) =>
        DbSet.RemoveRange(entities);


}
