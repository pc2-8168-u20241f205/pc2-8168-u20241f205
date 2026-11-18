namespace PC28168.U20241f205.Openpay.Platform.Shared.Domain.Model.ValueObjects;

/// <summary>Represents a non-negative monetary amount and its ISO currency code.</summary>
/// <remarks>Author: Candy Milagros Vizcarra Mamani.</remarks>
public sealed record FinancialAmount
{
    /// <summary>Gets the amount.</summary>
    public decimal Amount { get; private set; }
    /// <summary>Gets the currency code.</summary>
    public string CurrencyCode { get; private set; } = string.Empty;
    private FinancialAmount() { }

    /// <summary>Creates a validated financial amount.</summary>
    /// <param name="amount">Non-negative monetary amount.</param>
    /// <param name="currencyCode">Non-empty currency code.</param>
    public FinancialAmount(decimal amount, string currencyCode)
    {
        if (amount < decimal.Zero)
            throw new ArgumentOutOfRangeException(nameof(amount), amount, "Amount must be greater than or equal to zero.");
        if (currencyCode is null)
            throw new ArgumentNullException(nameof(currencyCode), "CurrencyCode is required.");
        if (string.IsNullOrWhiteSpace(currencyCode))
            throw new ArgumentException("CurrencyCode cannot be empty or whitespace.", nameof(currencyCode));
        Amount = amount;
        CurrencyCode = currencyCode.Trim().ToUpperInvariant();
    }

    /// <summary>Multiplies the amount and preserves its currency.</summary>
    /// <param name="factor">Multiplier.</param>
    /// <returns>A new FinancialAmount.</returns>
    public FinancialAmount MultiplyBy(decimal factor) => new(Amount * factor, CurrencyCode);
}
