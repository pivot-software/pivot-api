using System;

namespace ERP.Shared.Abstractions;

/// <summary>
/// Entidade com a chave (PK) tipada em <see cref="Guid"/>.
/// </summary>
public abstract class BaseEntity : IEntityKey<Guid>
{
    public Guid Id { get; private init; } = Guid.NewGuid();
}
