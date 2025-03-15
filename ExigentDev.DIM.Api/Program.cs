using ExigentDev.DIM.Api.Data;
using ExigentDev.DIM.Api.Interfaces;
using ExigentDev.DIM.Api.Models;
using ExigentDev.DIM.Api.Repositories;
using ExigentDev.DIM.Api.Services;
using ExigentDev.DIM.Api.Transformers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

DotNetEnv.Env.Load();

var dbEnv = Environment.GetEnvironmentVariable("DB_ENV");
var dbProdString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
var dbDevString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING_DEV");
var authIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
var authAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");
var authSigningKey = Environment.GetEnvironmentVariable("JWT_SIGNINGKEY");
var connectionString = dbEnv == "dev" ? dbDevString : dbDevString;

var builder = WebApplication.CreateBuilder(args);

builder
  .Services.AddControllers()
  .AddNewtonsoftJson(options =>
  {
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
  });

builder.Services.AddOpenApi(
  "v1",
  options =>
  {
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
  }
);

builder.Services.AddDbContext<ApplicationDBContext>(options =>
  options.UseNpgsql(
    builder.Configuration.GetConnectionString("DefaultConnection") ?? connectionString
  )
);

builder
  .Services.AddIdentity<AppUser, IdentityRole>(options =>
  {
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 12;

    options.User.AllowedUserNameCharacters =
      "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    options.User.RequireUniqueEmail = true;
  })
  .AddEntityFrameworkStores<ApplicationDBContext>()
  .AddDefaultTokenProviders();

builder
  .Services.AddAuthentication(options =>
  {
    options.DefaultAuthenticateScheme =
      options.DefaultChallengeScheme =
      options.DefaultForbidScheme =
      options.DefaultScheme =
      options.DefaultSignInScheme =
      options.DefaultSignOutScheme =
        JwtBearerDefaults.AuthenticationScheme;
  })
  .AddJwtBearer(options =>
  {
    options.TokenValidationParameters = new TokenValidationParameters
    {
      ValidateIssuer = true,
      ValidIssuer = authIssuer,
      ValidateAudience = true,
      ValidAudience = authAudience,
      ValidateIssuerSigningKey = true,
      IssuerSigningKey = new SymmetricSecurityKey(
        System.Text.Encoding.UTF8.GetBytes(authSigningKey!)
      ),
    };
  });

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IDogRepository, DogRepository>();
builder.Services.AddScoped<IDogImageRepository, DogImageRepository>();

builder.Services.AddHealthChecks();

var app = builder.Build();

// perform all migrations before running
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
      .WithTheme(ScalarTheme.Saturn)
      .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
  });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
