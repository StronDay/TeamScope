using MMLib.SwaggerForOcelot;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Загружаем конфигурацию Ocelot
builder.Configuration
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

// Добавляем сервисы контроллеров
builder.Services.AddControllers();

// Добавляем Swagger для самого API Gateway
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Добавляем Ocelot
builder.Services.AddOcelot(builder.Configuration);

// Добавляем Swagger для Ocelot с настройками
builder.Services.AddSwaggerForOcelot(builder.Configuration, options =>
{
    options.GenerateDocsForGatewayItSelf = true; // Включаем документацию для самого шлюза
});

var app = builder.Build();

// Включаем Swagger
app.UseSwagger();

// Настраиваем Swagger UI для Ocelot
app.UseSwaggerForOcelotUI(options =>
{
    options.PathToSwaggerGenerator = "/swagger/docs";
    
    // Если хотите видеть отдельные сервисы
    // options.DocumentTitle = "API Gateway - All Services";
    
    // Опционально: можно добавить группировку
    options.ReConfigureUpstreamSwaggerJson = AlterUpstreamSwaggerJson;
});

// Используем Ocelot middleware
await app.UseOcelot();

app.MapControllers();

app.Run();

static string AlterUpstreamSwaggerJson(HttpContext context, string swaggerJson)
{
    try
    {
        using var document = System.Text.Json.JsonDocument.Parse(swaggerJson);
        var root = document.RootElement;
        
        // Здесь можно модифицировать swaggerJson при необходимости
        // Например, исправить серверы, добавить префиксы и т.д.
        
        return swaggerJson;
    }
    catch
    {
        return swaggerJson;
    }
}