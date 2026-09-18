using Microsoft.EntityFrameworkCore;
using R5.A._08_TD3.Models;
using R5.A._08_TD3.Models.Repository.Implementations;
using R5.A._08_TD3.Repositories;
using System.Text.Json.Serialization;

using ProductRepositoryInterface =
    R5.A._08_TD3.Models.Repository.Interfaces.IProduitRepository;

var builder = WebApplication.CreateBuilder(args);

// Entity Framework connection to PostgreSQL
builder.Services.AddDbContext<R508Context>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddScoped<ProductRepositoryInterface, ProductRepository>();
builder.Services.AddScoped<IMarqueRepository, MarqueRepository>();
builder.Services.AddScoped<ITypeProduitRepository, TypeProduitRepository>();

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// Learn more about configuring Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
