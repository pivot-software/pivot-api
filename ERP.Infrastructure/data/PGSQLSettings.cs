using System;
namespace ERP.Infrastructure.data;

public class PgsqlSettings
{
    public const string SectionName = "ConnectionStrings";

    public string PostgreSQL { get; set; } = null!;
}
