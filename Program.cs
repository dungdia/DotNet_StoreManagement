using System.Reflection;
using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.SharedKernel.configuration;
using DotNet_StoreManagement.SharedKernel.exception;
using DotNet_StoreManagement.SharedKernel.persistence;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Serilog;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();

TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowCors", builder =>
    {
        builder
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddAnnotation(Assembly.GetExecutingAssembly());
builder.Services.AddAutoMapper(cfg =>
{
}, typeof(Program).Assembly);
// builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly(), typeof(MappingConfig).Assembly);
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddOpenApi();

var config = builder.Configuration;
builder.Services.ControllerConfigExtension(config);
builder.Services.DatabaseConfigExtension(config);
builder.Services.SecurityConfigExtension(config);
builder.Services.VNConfig(config);

//var cloudinaryConfig = builder.Configuration.GetSection("Cloudinary");
//Account account = new Account(
//    cloudinaryConfig["CloudName"],
//    cloudinaryConfig["ApiKey"],
//    cloudinaryConfig["ApiSecret"]
//);
//Cloudinary cloudinary = new Cloudinary(account);
//builder.Services.AddSingleton(cloudinary);
//builder.Services.AddControllers();

var app = builder.Build();

app.UseGlobalExceptionMiddleware();

app.UseCors("AllowCors");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapGet("/api/health/db", async (IConfiguration config) =>
{
    try
    {
        var connectionString = config.GetConnectionString("DefaultConnection")!;
        
        var options = new DbContextOptionsBuilder<BaseContext>()
            .UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString)
            )
            .Options;

        await using var context = new AppDbContext(options, config);
        await context.Database.CanConnectAsync();
        
        return Results.Ok(new 
        {
            status = "OK",
            database = "Connected",
            timestamp = DateTime.UtcNow
        });
    }
    catch (Exception ex)
    {
        return Results.Problem(
            title: "Database connection failed",
            detail: $"Connection string: {config.GetConnectionString("DefaultConnection")}\nError: {ex.Message}",
            statusCode: StatusCodes.Status503ServiceUnavailable
        );
    }
});

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
