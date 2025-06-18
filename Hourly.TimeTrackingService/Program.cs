using Hourly.TimeTrackingService;
using Hourly.TimeTrackingService.Application.Publishers;
using Hourly.TimeTrackingService.Application.Services;
using Hourly.TimeTrackingService.Infrastructure.Messaging.Consumers.GitCommitConsumers;
using Hourly.TimeTrackingService.Infrastructure.Messaging.Consumers.UserConsumers;
using Hourly.TimeTrackingService.Infrastructure.Persistence;
using Hourly.TimeTrackingService.Infrastructure.Queries;
using Hourly.TimeTrackingService.Infrastructure.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

var massTransitOptions = builder.Configuration
    .GetSection("RabbitMQ")
    .Get<RabbitMQSettings>();

if (massTransitOptions == null)
    throw new InvalidOperationException("RabbitMQ settings are not configured in appsettings.json");

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORS", policy =>
    {
        policy
            .SetIsOriginAllowed(_ => true) // Allow any origin
            .AllowAnyMethod()              // Allow all HTTP methods
            .AllowAnyHeader()              // Allow all headers
            .AllowCredentials();           // Allow credentials (cookies, auth headers)
    });
});

// Add Mass Transit config
builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();
    x.AddConsumers(Assembly.GetExecutingAssembly());

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(new Uri($"rabbitmq://{massTransitOptions.Host}:{massTransitOptions.Port}"), h =>
        {
            h.Username(massTransitOptions.UserName);
            h.Password(massTransitOptions.Password);
        });

        cfg.ConfigureEndpoints(context);

        cfg.ReceiveEndpoint("timetrackingservice-user-created", e =>
        {
            e.ConfigureConsumer<UserCreatedConsumer>(context);
        });
        cfg.ReceiveEndpoint("timetrackingservice-user-updated", e =>
        {
            e.ConfigureConsumer<UserUpdatedConsumer>(context);
        });
        cfg.ReceiveEndpoint("timetrackingservice-user-deleted", e =>
        {
            e.ConfigureConsumer<UserDeletedConsumer>(context);
        });
        cfg.ReceiveEndpoint("timetrackingservice-gitcommit-created", e =>
        {
            e.ConfigureConsumer<GitCommitCreatedConsumer>(context);
        });
        cfg.ReceiveEndpoint("timetrackingservice-gitcommit-deleted", e =>
        {
            e.ConfigureConsumer<GitCommitDeletedConsumer>(context);
        });
    });
});

builder.Services.AddMassTransitTextWriterLogger();

// Add publishers
builder.Services.AddScoped<IWorkSessionEventPublisher, WorkSessionEventPublisher>();

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

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();
app.UseCors("CORS");

app.UseAuthorization();

app.MapControllers();


app.Run();

