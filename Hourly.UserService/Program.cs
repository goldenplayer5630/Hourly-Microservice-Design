using Hourly.Application.Services;
using Hourly.Data.Repositories;
using Hourly.UserService;
using Hourly.UserService.Application.Publishers;
using Hourly.UserService.Application.Services;
using Hourly.UserService.Infrastructure.Messaging.GitCommitConsumers;
using Hourly.UserService.Infrastructure.Persistence;
using Hourly.UserService.Infrastructure.Repositories;
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

        cfg.ReceiveEndpoint("userservice-gitcommit-created", e =>
        {
            e.ConfigureConsumer<GitCommitCreatedConsumer>(context);
        });
        cfg.ReceiveEndpoint("userservice-gitcommit-deleted", e =>
        {
            e.ConfigureConsumer<GitCommitDeletedConsumer>(context);
        });
    });
});

builder.Services.AddMassTransitTextWriterLogger(); 


// Add publishers
builder.Services.AddScoped<IUserEventPublisher, UserEventPublisher>();
builder.Services.AddScoped<IUserContractEventPublisher, UserContractEventPublisher>();

// Register repositories
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserContractRepository, UserContractRepository>();

// Register services
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserContractService, UserContractService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();


app.Run();
