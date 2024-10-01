using Clase02.Mediadores;
using Clase02.Persistencia;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string cadenaConexion = Environment.GetEnvironmentVariable("CadenaConexion") ?? string.Empty;

Console.WriteLine("Cadena: " + cadenaConexion);

builder.Services.AddDbContext<ClaseDbContext>(options =>
{
    options.UseSqlServer(cadenaConexion);
});

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
