namespace PC28168.U20241f205.Openpay.Platform.Shared.Domain.Model;

/// <summary>Defines audit timestamps for aggregate roots.</summary>
/// <remarks>Author: Candy Milagros Vizcarra Mamani.</remarks>
public interface IAuditableEntity
{
    /// <summary>Gets or sets the creation timestamp in UTC.</summary>
    DateTimeOffset? CreatedAt { get; set; }
    /// <summary>Gets or sets the last update timestamp in UTC.</summary>
    DateTimeOffset? UpdatedAt { get; set; }
}
