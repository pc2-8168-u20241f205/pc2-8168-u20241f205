namespace PC28168.U20241f205.Openpay.Platform.Settlements.Application.Errors;
/// <summary>Represents a known error during disbursement order creation.</summary>
/// <remarks>Author: Candy Milagros Vizcarra Mamani.</remarks>
public sealed record CreateDisbursementOrderError(string Code, string Message);
