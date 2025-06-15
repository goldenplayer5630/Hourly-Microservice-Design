using Hourly.GitService;
using Hourly.GitService.Application.Publishers;
using Hourly.GitService.Application.Services;
using Hourly.GitService.Infrastructure.Queries;
using Hourly.GitService.Infrastructure.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

var corsOptions = builder.Configuration
    .GetSection("CORS")
    .Get<CorsSettings>();

var massTransitOptions = builder.Configuration
    .GetSection("RabbitMQ");

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORS", policy =>
    {
        policy.WithOrigins(corsOptions.AllowedOrigins)
        .WithMethods(corsOptions.AllowedMethods)
        .WithHeaders(corsOptions.AllowedHeaders)
              .WithExposedHeaders(corsOptions.ExposedHeaders)
              .SetPreflightMaxAge(TimeSpan.FromSeconds(corsOptions.MaxAge));

        if (corsOptions.AllowCredentials)
            policy.AllowCredentials();
        else
            policy.DisallowCredentials();
    });
});

// Add Mass Transit config
builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();
    x.AddConsumers(Assembly.GetExecutingAssembly());

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(
            massTransitOptions["Host"],
            ushort.Parse(massTransitOptions["Port"]),
            "/",
            h =>
            {
                h.Username(massTransitOptions["Username"]);
                h.Password(massTransitOptions["Password"]);
            });

        cfg.ConfigureEndpoints(context);
    });
});

// Register publishers
builder.Services.AddScoped<IGitCommitEventPublisher, GitCommitEventPublisher>();

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

app.UseCors("CORS");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
