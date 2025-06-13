using Hourly.TimeTrackingService;
using Hourly.TimeTrackingService.Abstractions.Queries;
using Hourly.TimeTrackingService.Abstractions.Repositories;
using Hourly.TimeTrackingService.Abstractions.Services;
using Hourly.TimeTrackingService.Application.Services;
using Hourly.TimeTrackingService.Infrastructure.Persistence;
using Hourly.TimeTrackingService.Infrastructure.Queries;
using Hourly.TimeTrackingService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

var reactCorsOptions = builder.Configuration
    .GetSection("React-CORS")
    .Get<CorsSettings>();

var gatewayCorsOptions = builder.Configuration
    .GetSection("Gateway-CORS")
    .Get<CorsSettings>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins(reactCorsOptions.AllowedOrigins)
        .WithMethods(reactCorsOptions.AllowedMethods)
        .WithHeaders(reactCorsOptions.AllowedHeaders)
              .WithExposedHeaders(reactCorsOptions.ExposedHeaders)
              .SetPreflightMaxAge(TimeSpan.FromSeconds(reactCorsOptions.MaxAge));

        if (reactCorsOptions.AllowCredentials)
            policy.AllowCredentials();
        else
            policy.DisallowCredentials();
    });

    options.AddPolicy("AllowGateway", policy =>
    {
        policy.WithOrigins(gatewayCorsOptions.AllowedOrigins)
        .WithMethods(gatewayCorsOptions.AllowedMethods)
        .WithHeaders(gatewayCorsOptions.AllowedHeaders)
              .WithExposedHeaders(gatewayCorsOptions.ExposedHeaders)
              .SetPreflightMaxAge(TimeSpan.FromSeconds(gatewayCorsOptions.MaxAge));

        if (gatewayCorsOptions.AllowCredentials)
            policy.AllowCredentials();
        else
            policy.DisallowCredentials();
    });
});

// Register repositories
builder.Services.AddScoped<IWorkSessionRepository, WorkSessionRepository>();

// Register queries
builder.Services.AddScoped<IGitCommitQuery, GitCommitQuery>();
builder.Services.AddScoped<IUserContractQuery, UserContractQuery>();

// Register services
builder.Services.AddScoped<IWorkSessionService, WorkSessionService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable CORS before endpoints ??
app.UseCors("AllowReactApp");
app.UseCors("AllowGateway");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
