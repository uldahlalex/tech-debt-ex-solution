using System.Text.Json.Serialization;
using API;
using Infra;
using LinqToDB;
using Service;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Cookbook")
                       ?? throw new InvalidOperationException("Missing connection string 'Cookbook'");
var dbOptions = new DataOptions<MyDatabaseConnection>(new DataOptions().UseSQLite(connectionString));

builder.Services.AddScoped(_ => new MyDatabaseConnection(dbOptions));
builder.Services.AddScoped<CookbookService>();
builder.Services.AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
    .ConfigureApiBehaviorOptions(o => o.InvalidModelStateResponseFactory = ProblemResponses.FromInvalidModelState);
builder.Services.AddOpenApiDocument(o =>
    o.SchemaSettings.SchemaProcessors.Add(new RequireNonNullablePropertiesSchemaProcessor()));
builder.Services.AddProblemDetails();
builder.Services.AddCors();
builder.Services.AddExceptionHandler<MyExceptionHandler>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    DatabaseSeeder.CreateSchemaAndSeed(scope.ServiceProvider.GetRequiredService<MyDatabaseConnection>());
}

app.UseExceptionHandler();
app.UseCors(config => config.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.MapControllers();
app.UseOpenApi();
app.UseSwaggerUi();
app.Run();
