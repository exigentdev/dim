using ExigentDev.DIM.Api.Data;
using ExigentDev.DIM.Api.Interfaces;
using ExigentDev.DIM.Api.Repositories;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

DotNetEnv.Env.Load();

var dbEnv = Environment.GetEnvironmentVariable("DB_ENV");
var dbProdString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
var dbDevString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING_DEV");
var connectionString = dbEnv == "dev" ? dbDevString : dbProdString;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ApplicationDBContext>(options =>
  options.UseNpgsql(
    builder.Configuration.GetConnectionString("DefaultConnection") ?? connectionString
  )
);

builder.Services.AddScoped<IStockRepository, StockRepository>();

builder.Services.AddHealthChecks();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
  var db = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
  db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
  app.MapScalarApiReference(options =>
  {
    options
      .WithTitle("Weather Forecast API")
      .WithTheme(ScalarTheme.Saturn)
      .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
  });
}

app.UseHttpsRedirection();

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
