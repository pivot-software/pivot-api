using System;
namespace ERP.Infrastructure.data;

public class PgsqlSettings
{
    public const string SectionName = "ConnectionStrings";

    public string SqlServer { get; set; } = null!;
}
