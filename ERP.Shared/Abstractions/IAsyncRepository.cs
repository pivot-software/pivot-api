using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Shared.Abstractions;

public interface IAsyncRepository<TEntity> : IRepository where TEntity : BaseEntity, IAggregateRoot
{
    void Add(TEntity entity);
    void AddRange(IEnumerable<TEntity> entities);
    void Update(TEntity entity);
    void UpdateRange(IEnumerable<TEntity> entities);
    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);
}
