using System.Data;
using Microsoft.Extensions.Options;
using Npgsql;

namespace ERP.Infrastructure.data;

public class PgsqlContext
{
    private readonly PgsqlSettings _pgsqlSettings;

    public PgsqlContext(IOptions<PgsqlSettings> pgsqlSettings)
    {
        _pgsqlSettings = pgsqlSettings.Value;
    }

    public IDbConnection CreateConnection()
        => new NpgsqlConnection(_pgsqlSettings.PostgreSQL);
}
