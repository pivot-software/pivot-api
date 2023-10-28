using ERP.Domain.Entities;
using ERP.Domain.Repositories;
using ERP.Infrastructure.Data.Context;
using ERP.Infrastructure.Data.Repositories.Common;
namespace ERP.Infrastructure.Data.Repositories;

public class WorkspaceRepository : EfRepository<Workspace>, IWorkspaceRepository
{
    public WorkspaceRepository(ErpContext context) : base(context)
    {
    }

}
