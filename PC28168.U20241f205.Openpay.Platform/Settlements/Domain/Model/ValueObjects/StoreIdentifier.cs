namespace PC28168.U20241f205.Openpay.Platform.Settlements.Domain.Model.ValueObjects;
/// <summary>Identifies an affiliated store from another bounded context.</summary>
/// <remarks>Author: Candy Milagros Vizcarra Mamani.</remarks>
public sealed record StoreIdentifier
{
    /// <summary>Gets the store identifier value.</summary>
    public Guid Value { get; private set; }
    private StoreIdentifier() { }
    /// <summary>Creates a store identifier.</summary>
    public StoreIdentifier(Guid value) => Value = value != Guid.Empty ? value : throw new ArgumentException("AffiliateStoreId cannot be empty.", nameof(value));
    /// <summary>Returns the identifier as a string.</summary>
    public override string ToString() => Value.ToString();
}
