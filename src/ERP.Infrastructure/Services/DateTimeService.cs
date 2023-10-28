using System;
using ERP.Shared.Abstractions;

namespace ERP.Infrastructure.Services;

public class DateTimeService : IDateTimeService
{
    public DateTime Now => DateTime.Now;
}
