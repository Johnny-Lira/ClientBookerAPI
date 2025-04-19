using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Domain.Interfaces;
using Application.Services;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence.Context;
using DotNetEnv;
using System.Text.RegularExpressions;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables from .env file
Env.Load();

var rawConnection = builder.Configuration.GetSection("ConnectionStrings")["PostgresConnection"];

string ExpandEnvVariables(string input)
{
    return Regex.Replace(input, @"\$\{(.*?)\}", match =>
    {
        var varName = match.Groups[1].Value;
        return Environment.GetEnvironmentVariable(varName) ?? match.Value;
    });
}

if (rawConnection == null)
{
    throw new InvalidOperationException("Connection string not found in configuration.");
}

var connectionString = ExpandEnvVariables(rawConnection);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString)
    .EnableSensitiveDataLogging()
    );

builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<AppointmentService>();

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

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
