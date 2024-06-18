using LangLearner.Database;
using LangLearner.Database.Seeders;
using Microsoft.EntityFrameworkCore;
using NLog.Extensions.Logging;
using System;
using NLog.Web;
using LangLearner.Services;
using LangLearner.Database.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Host.UseNLog();
builder.Services.AddScoped<ILanguagesService, LanguagesService>();
builder.Services.AddScoped<ILanguageRepository, LanguageRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

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
