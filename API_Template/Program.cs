using API_Template.Middleware;
using API_Template.Profiles;
using App.Repo;
using Data.Entities.DatabaseDB;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog for logging
builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day);
});

// Configure CORS to allow all origins, methods, and headers
// This is useful for development purposes, but should be restricted in production.
// For very general use.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Register AutoMapper
builder.Services.AddAutoMapper(typeof(MainProfile));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Register DbContext with MariaDB server version
var serverVersion = new MariaDbServerVersion(new Version(11, 4, 2));
//TODO: Setup connection string in appsettings.Development.json
builder.Services.AddDbContext<ExampleDbContext>(options => options.UseMySql(builder.Configuration.GetConnectionString("ExampleContext"), serverVersion));
// Register the generic repository
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));



var app = builder.Build();

// Add middleware for exception handling
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseMiddleware<ApiKeyAuthenticationMiddleware>();
    app.UseExceptionHandler("/error");
}

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
