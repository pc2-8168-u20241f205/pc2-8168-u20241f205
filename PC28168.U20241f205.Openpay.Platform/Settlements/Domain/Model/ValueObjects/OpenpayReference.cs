namespace PC28168.U20241f205.Openpay.Platform.Settlements.Domain.Model.ValueObjects;
/// <summary>Identifies a disbursement order across bounded contexts.</summary>
/// <remarks>Author: Candy Milagros Vizcarra Mamani.</remarks>
public sealed record OpenpayReference
{
    /// <summary>Gets the unique reference value.</summary>
    public Guid Value { get; private set; }
    private OpenpayReference() { }
    /// <summary>Creates an Openpay reference.</summary>
    public OpenpayReference(Guid value) => Value = value != Guid.Empty ? value : throw new ArgumentException("OpenpayReference cannot be empty.", nameof(value));
    /// <summary>Returns the reference as a string.</summary>
    public override string ToString() => Value.ToString();
}
