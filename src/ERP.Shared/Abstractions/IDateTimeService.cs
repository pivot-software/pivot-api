using System;

namespace ERP.Shared.Abstractions;

public interface IDateTimeService
{
    DateTime Now { get; }
}
