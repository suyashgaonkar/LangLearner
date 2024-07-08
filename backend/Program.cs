using LangLearner.Database;
using LangLearner.Database.Seeders;
using Microsoft.EntityFrameworkCore;
using NLog.Extensions.Logging;
using System;
using NLog.Web;
using LangLearner.Services;
using LangLearner.Database.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using LangLearner.Models.Entities;
using LangLearner.Middlewares;
using LangLearner.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(x =>
{
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = "https://LangLearner.com",
        ValidAudience = "https://LangLearner.com",
        IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes("secret4w78efhc2783gd671872e2@!WDX!@#!~!@$!@E@!1wd12")),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorization();
builder.Services.AddControllers(options =>
{
    //options.Filters.Add(new ValidateModelFilter());
    options.Filters.Add<ValidateModelFilter>(int.MinValue);
});

builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Host.UseNLog();
// Services
builder.Services.AddScoped<ILanguagesService, LanguagesService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IIdentityService, IdentityService>();

// Repositories
builder.Services.AddScoped<ILanguageRepository, LanguageRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Middlewares
builder.Services.AddScoped<ErrorHandlingMiddleware>();

builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Seed data
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<AppDbContext>();
var logger = services.GetService<ILogger>();

try
{
    await context.Database.MigrateAsync();
    await DatabaseInitializer.SeedDataAsync(context);
}
catch (Exception ex)
{
    app.Logger.LogError(ex, " and error occured during migration or seeding");
}

app.Run();
