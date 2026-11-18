using System.Globalization;
using Microsoft.AspNetCore.Mvc;
namespace PC28168.U20241f205.Openpay.Platform.Settlements.Interfaces.REST;
/// <summary>Resolves API error messages for supported request cultures.</summary>
/// <remarks>Author: Candy Milagros Vizcarra Mamani.</remarks>
public static class LocalizedErrors
{
    public static (int Status, string Title, string Detail) Resolve(string code, string original,
        IServiceProvider services)
    {
        var spanish = CultureInfo.CurrentUICulture.Name.StartsWith("es", StringComparison.OrdinalIgnoreCase);
        var conflict = code is "duplicate_order_tracking_code" or "persistence_conflict";
        var title = spanish ? (conflict ? "Conflicto al crear la orden" : "Error de validación")
                            : (conflict ? "Order creation conflict" : "Validation error");
        var detail = code switch
        {
            "duplicate_order_tracking_code" => spanish ? "Ya existe una orden con este código de seguimiento." : "A disbursement order with this tracking code already exists.",
            "persistence_conflict" => spanish ? "No se pudo guardar la orden debido a un conflicto de unicidad." : "The order could not be stored because a unique constraint was violated.",
            _ => spanish ? TranslateValidation(original) : original
        };
        return (conflict ? StatusCodes.Status409Conflict : StatusCodes.Status400BadRequest, title, detail);
    }
    private static string TranslateValidation(string message)
    {
        if (message.Contains("ScheduledTo", StringComparison.OrdinalIgnoreCase)) return "ScheduledTo debe ser posterior a ScheduledFrom.";
        if (message.Contains("Amount", StringComparison.OrdinalIgnoreCase)) return "El monto debe ser mayor o igual que cero.";
        if (message.Contains("CurrencyCode", StringComparison.OrdinalIgnoreCase)) return "El código de moneda es obligatorio y no puede estar vacío.";
        if (message.Contains("User", StringComparison.OrdinalIgnoreCase)) return "El usuario es obligatorio y no debe exceder los 20 caracteres.";
        if (message.Contains("Remarks", StringComparison.OrdinalIgnoreCase)) return "Las observaciones no deben exceder los 85 caracteres.";
        return "La solicitud contiene datos no válidos.";
    }
}
