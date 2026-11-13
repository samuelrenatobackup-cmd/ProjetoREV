
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Plot.WebApi.Context;
using Plot.WebApi.Interfaces;
using Plot.WebApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException(
        "A string de conexão 'DefaultConnection' não foi configurada.");
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IGeneroRepository, GeneroRepository>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Plot - WebApi",
        Version = "v1",
        Description = "API de filmes, gêneros e usuários"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Plot - WebApi v1");
        options.RoutePrefix = "swagger";
    });
}

app.MapControllers();

app.Run();
