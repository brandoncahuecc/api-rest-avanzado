using Clase03.Mediadores;
using Clase03.Persistencia;
using Clase03.Servicios;
using rest_biblioteca.Dependencias;
using rest_biblioteca.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.AgregarLogging();
builder.Services.AgregarReddisCache();
builder.Services.AgregarJwtTokenValidacion();

builder.Services.AddSingleton<ICachePersistencia, CachePersistencia>();
builder.Services.AddSingleton<ICategoriaPersistencia, CategoriaPersistencia>();
builder.Services.AddSingleton<ICategoriaServicio, CategoriaServicio>();

builder.Services.RegistrarMediador<ListarCategoriaHandler>();

var app = builder.Build();

app.UseMiddleware<CustomeMiddleware>();

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
