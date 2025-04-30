using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
var builder = WebApplication.CreateBuilder(args);

// 🔹 Configurar controladores
builder.Services.AddControllers();


builder.Logging.AddConsole();

// 🔹 Configurar CORS para permitir el frontend en localhost:5173
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") // Dirección del frontend
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials(); // Permitir autenticación con cookies o tokens
        });
});

// 🔹 Configurar autenticación con JWT
var key = Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secret"] ?? "ClavePorDefecto");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

builder.Services.AddAuthorization();

// 🔹 Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Sav API",
        Version = "v1",
    });
});

var app = builder.Build();

// 🔹 Habilitar Swagger al iniciar el proyecto
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SGE API v1");
    c.RoutePrefix = string.Empty; // Acceso directo a Swagger al iniciar el proyecto
});

// 🔹 Habilitar CORS antes de autenticación y autorización
app.UseCors(MyAllowSpecificOrigins);

// 🔹 Habilitar autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

// 🔹 Habilitar los controladores
app.MapControllers();

// 🔹 Iniciar la API
app.Run();
