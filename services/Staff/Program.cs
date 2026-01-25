using Staff.Infrastructure;
using NSwag.Generation.Processors.Security;
using NSwag;
using Staff.Core.App.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddServices();
builder.Services.AddDataAccess();

builder.Services.Configure<AuthSettings>
(
    builder.Configuration.GetSection("AuthSettings")
);

builder.Services.AddAuth(builder.Configuration);

builder.Services.AddOpenApiDocument();

var app = builder.Build();

app.UseOpenApi(config =>
{
    config.PostProcess = (document, request) =>
    {
        document.Info.Title = "Staff Service API";
        document.Info.Version = "v1";
    };
    
    // Убедитесь, что путь правильный
    config.Path = "/swagger/v1/swagger.json";
});

// Настройка конвейера HTTP запросов
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUi(config =>
    {
        config.Path = "/swagger"; // UI будет доступен по /swagger
        config.DocumentPath = "/swagger/v1/swagger.json"; // Путь к документу
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();