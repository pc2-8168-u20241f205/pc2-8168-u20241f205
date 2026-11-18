using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using PC28168.U20241f205.Openpay.Platform.Shared.Domain.Repositories;
using PC28168.U20241f205.Openpay.Platform.Shared.Infrastructure.Persistence.EFC.Configuration;
using PC28168.U20241f205.Openpay.Platform.Shared.Infrastructure.Persistence.EFC.Repositories;
using PC28168.U20241f205.Openpay.Platform.Settlements.Application.Internal.CommandServices;
using PC28168.U20241f205.Openpay.Platform.Settlements.Application.Services;
using PC28168.U20241f205.Openpay.Platform.Settlements.Domain.Repositories;
using PC28168.U20241f205.Openpay.Platform.Settlements.Infrastructure.Persistence.EFC.Repositories;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
.ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var spanish = CultureInfo.CurrentUICulture.Name.StartsWith("es", StringComparison.OrdinalIgnoreCase);
        var details = new ValidationProblemDetails(context.ModelState)
        {
            Status = StatusCodes.Status400BadRequest,
            Title = spanish ? "Error de validación" : "Validation error",
            Detail = spanish ? "Uno o más campos de la solicitud no son válidos." : "One or more request fields are invalid."
        };
        return new BadRequestObjectResult(details);
    };
});
builder.Services.AddLocalization();
builder.Services.AddProblemDetails();
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info.Title = "Openpay Settlements API";
        document.Info.Version = "v1";
        document.Info.Description = "REST API for creating merchant disbursement orders.";
        return Task.CompletedTask;
    });
});
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connection = builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Database connection string is not configured.");
    options.UseMySQL(connection);
});
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDisbursementOrderRepository, DisbursementOrderRepository>();
builder.Services.AddScoped<IDisbursementOrderCommandService, DisbursementOrderCommandService>();

var app = builder.Build();
// Create the configured MySQL database and schema on first startup.
using (var scope = app.Services.CreateScope())
{
    var database = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    database.Database.EnsureCreated();
}

app.UseExceptionHandler(errorApp => errorApp.Run(async context =>
{
    var culture = CultureInfo.CurrentUICulture.Name.StartsWith("es", StringComparison.OrdinalIgnoreCase);
    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
    await Results.Problem(statusCode: 500,
        title: culture ? "Error interno del servidor" : "Internal server error",
        detail: culture ? "Ocurrió un error inesperado al procesar la solicitud." : "An unexpected error occurred while processing the request.")
        .ExecuteAsync(context);
}));
string[] supportedCultures = ["en", "en-US", "es", "es-PE"];
var localization = new RequestLocalizationOptions()
    .SetDefaultCulture("en")
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);
localization.ApplyCurrentCultureToResponseHeaders = true;
app.UseRequestLocalization(localization);
app.UseHttpsRedirection();
app.MapOpenApi();
app.MapScalarApiReference();
app.MapControllers();
app.Run();
