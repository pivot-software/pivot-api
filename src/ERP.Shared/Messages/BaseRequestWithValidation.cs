namespace ERP.Shared.Messages;

/// <inheritdoc />
public abstract class BaseRequestWithValidation : IRequest
{
    protected BaseRequestWithValidation() =>
        ValidationResult = new FluentValidation.Results.ValidationResult();

    [Newtonsoft.Json.JsonIgnore]
    public FluentValidation.Results.ValidationResult ValidationResult { get; protected set; }

    /// <summary>
    /// Indica se a requisição é valida.
    /// </summary>
    [Newtonsoft.Json.JsonIgnore]
    public bool IsValid =>
        ValidationResult.IsValid;

    /// <summary>
    /// Valida a requisição.
    /// </summary>
    public abstract Task ValidateAsync();
}
