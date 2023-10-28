using System;
using Microsoft.EntityFrameworkCore;
using ERP.Infrastructure.Data.Context;

namespace ERP.Infrastructure.Data.Repositories.Common;

public abstract class RepositoryBase<TEntity> : IDisposable where TEntity : class
{
    protected readonly DbSet<TEntity> DbSet;
    private readonly ErpContext _context;

    protected RepositoryBase(ErpContext context)
    {
        _context = context;
        DbSet = context.Set<TEntity>();
    }

    #region IDisposable

    // To detect redundant calls.
    private bool _disposed;

    // Public implementation of Dispose pattern callable by consumers.
    ~RepositoryBase() => Dispose(false);

    // Public implementation of Dispose pattern callable by consumers.
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    // Protected implementation of Dispose pattern.
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        // Dispose managed state (managed objects).
        if (disposing)
            _context.Dispose();

        _disposed = true;
    }

    #endregion

}
