using Microsoft.EntityFrameworkCore;
using MiniPizzaria.Application.Repositorios;
using MiniPizzaria.Application.Servicos;
using MiniPizzaria.Infrastructure.Repositorios;
using MiniPizzaria.Infrastructure.Persistencia;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
//builder.Services.AddSingleton<IPizzaRepositorio, PizzaRepositorioEmMemoria>();
builder.Services.AddScoped<IPizzaRepositorio, PizzaRepositorioSql>();
builder.Services.AddScoped<IPizzaService, PizzaService>();
builder.Services.AddDbContext<PizzariaDbContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
