using Hourly.GitService;
using Hourly.GitService.Application.Publishers;
using Hourly.GitService.Application.Services;
using Hourly.GitService.Infrastructure.Messaging.Consumers;
using Hourly.GitService.Infrastructure.Queries;
using Hourly.GitService.Infrastructure.Repositories;
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

        cfg.ReceiveEndpoint("gitservice-user-created", e =>
        {
            e.ConfigureConsumer<UserCreatedConsumer>(context);
        });
        cfg.ReceiveEndpoint("gitservice-user-updated", e =>
        {
            e.ConfigureConsumer<UserUpdatedConsumer>(context);
        });
        cfg.ReceiveEndpoint("gitservice-user-deleted", e =>
        {
            e.ConfigureConsumer<UserDeletedConsumer>(context);
        });
    });
});

builder.Services.AddMassTransitTextWriterLogger();

// Register publishers
builder.Services.AddScoped<IGitRepositoryEventPublisher, GitRepositoryEventPublisher>();
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

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();
app.UseCors("CORS");

app.UseAuthorization();

app.MapControllers();


app.Run();

