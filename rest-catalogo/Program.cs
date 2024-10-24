using Prometheus;
using rest_biblioteca.Dependencias;
using rest_biblioteca.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.UseHttpClientMetrics();
builder.Logging.AgregarLogging();
builder.Services.AgregarReddisCache();
builder.Services.AgregarJwtTokenValidacion();

builder.Services.AddSingleton<ICachePersistencia, CachePersistencia>();
//builder.Services.AddSingleton<ICategoriaPersistencia, CategoriaPersistencia>();
//builder.Services.AddSingleton<ICategoriaServicio, CategoriaServicio>();

//builder.Services.RegistrarMediador<ListarCategoriaHandler>();

var app = builder.Build();

app.UseMetricServer();
app.UseHttpMetrics();

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
