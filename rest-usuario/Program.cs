using rest_biblioteca.Dependencias;
using rest_biblioteca.Middlewares;
using rest_usuario.Mediadores;
using rest_usuario.Persistencia;
using rest_usuario.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.AgregarLogging();

builder.Services.AddSingleton<IGeneradorTokenJwt, GeneradorTokenJwt>();
builder.Services.AddSingleton<IUsuarioPersistencia, UsuarioPersistencia>();
builder.Services.AddSingleton<IUsuarioServicio, UsuarioServicio>();

builder.Services.RegistrarMediador<IniciarSesionHandler>();

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
