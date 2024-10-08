using Clase03.Mediadores;
using Clase03.Persistencia;
using Clase03.Servicios;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var loggerConfig = new LoggerConfiguration()
    .ReadFrom.Configuration(new ConfigurationBuilder().AddJsonFile("./Recursos/serilog-config.json").Build())
    .Enrich.FromLogContext().CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(loggerConfig);

builder.Services.AddStackExchangeRedisCache(option =>
{
    string redisConnection = Environment.GetEnvironmentVariable("RedisConnection") ?? string.Empty;
    option.Configuration = redisConnection;
});

builder.Services.AddSingleton<ICachePersistencia, CachePersistencia>();
builder.Services.AddSingleton<ICategoriaPersistencia, CategoriaPersistencia>();
builder.Services.AddSingleton<ICategoriaServicio, CategoriaServicio>();

builder.Services.RegistrarMediador();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
