using Clase03.Mediadores;
using Clase03.Persistencia;
using Clase03.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddStackExchangeRedisCache(option =>
{
    string redisConnection = Environment.GetEnvironmentVariable("RedisConnection") ?? string.Empty;
    option.Configuration = redisConnection;
});

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
