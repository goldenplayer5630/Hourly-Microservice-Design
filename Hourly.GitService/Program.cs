using Hourly.GitService;
using Hourly.GitService.Abstractions.Repositories;
using Hourly.GitService.Abstractions.Services;
using Hourly.GitService.Application.Services;
using Hourly.GitService.Infrastructure.Queries;
using Hourly.GitService.Infrastructure.Repositories;
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
builder.Services.AddScoped<IGitCommitRepository, GitCommitRepository>();
builder.Services.AddScoped<IGitRepositoryRepository, GitRepositoryRepository>();

// Register queries
builder.Services.AddScoped<IUserQuery, UserQuery>();


// Register services
builder.Services.AddScoped<IGitCommitService, GitCommitService>();
builder.Services.AddScoped<IGitRepositoryService, GitRepositoryService>();

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
