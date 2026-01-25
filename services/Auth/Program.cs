using Auth.Core.App.Services;
using Auth.Infrastructure;
using NSwag;
using NSwag.Generation.AspNetCore;
using NSwag.Generation.Processors.Security;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddControllers();
builder.Services.AddServices();
builder.Services.AddDataAccess();

builder.Services.Configure<AuthSettings>
(
    builder.Configuration.GetSection("AuthSettings")
);

builder.Services.AddAuth(builder.Configuration);

builder.Services.AddOpenApiDocument(options =>
{
    options.AddSecurity("Bearer", new OpenApiSecurityScheme
    {
        Description = "Bearer token auth header",
        Type = OpenApiSecuritySchemeType.Http,
        In = OpenApiSecurityApiKeyLocation.Header,
        Name = "Authorization",
        Scheme = "Bearer"
    });

    options.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("Bearer"));
});

var app = builder.Build();

// app.UseOpenApi(); // Это Middleware для генерации OpenAPI документа
// app.UseSwaggerUi(); // UI для NSwag (опционально для сервиса)

// ИЛИ более явно:
app.UseOpenApi(config =>
{
    config.PostProcess = (document, request) =>
    {
        document.Info.Title = "Auth Service API";
        document.Info.Version = "v1";
    };
    
    // Убедитесь, что путь правильный
    config.Path = "/swagger/v1/swagger.json";
});

// Если нужен UI
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUi(config =>
    {
        config.Path = "/swagger"; // UI будет доступен по /swagger
        config.DocumentPath = "/swagger/v1/swagger.json"; // Путь к документу
    });
}

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.MapOpenApi();
//     app.UseSwaggerUi(options =>
//     {
//         options.DocumentPath = "/openapi/v1.json";
//     });
// }

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();