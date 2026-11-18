namespace PC28168.U20241f205.Openpay.Platform.Shared.Application.Patterns;

/// <summary>Represents a successful or failed application operation.</summary>
/// <typeparam name="TValue">Successful value type.</typeparam>
/// <typeparam name="TError">Error type.</typeparam>
/// <remarks>Author: Candy Milagros Vizcarra Mamani.</remarks>
public abstract record Result<TValue, TError>
{
    /// <summary>Successful operation.</summary>
    public sealed record Success(TValue Value) : Result<TValue, TError>;
    /// <summary>Failed operation.</summary>
    public sealed record Failure(TError Error) : Result<TValue, TError>;
    /// <summary>Indicates whether the operation succeeded.</summary>
    public bool IsSuccess => this is Success;
    /// <summary>Transforms a successful value.</summary>
    public Result<TNext, TError> Map<TNext>(Func<TValue, TNext> map) => this switch
    {
        Success success => new Result<TNext, TError>.Success(map(success.Value)),
        Failure failure => new Result<TNext, TError>.Failure(failure.Error),
        _ => throw new InvalidOperationException("Unknown result type.")
    };
}
