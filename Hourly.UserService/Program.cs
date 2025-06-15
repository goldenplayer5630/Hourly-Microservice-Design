using Hourly.Application.Services;
using Hourly.Data.Repositories;
using Hourly.UserService;
using Hourly.UserService.Abstractions.Repositories;
using Hourly.UserService.Abstractions.Services;
using Hourly.UserService.Application.Publishers;
using Hourly.UserService.Infrastructure.Persistence;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

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

var massTransitOptions = builder.Configuration
    .GetSection("RabbitMQ");

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

// Add Mass Transit config
builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();
    x.AddConsumers(Assembly.GetExecutingAssembly());

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq", "/", h =>
        {
            h.Username(massTransitOptions["username"]);
            h.Password(massTransitOptions["password"]);
        });

        cfg.ConfigureEndpoints(context);
    });
});

// Add publishers
builder.Services.AddScoped<IUserEventPublisher, UserEventPublisher>();

// Register repositories
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserContractRepository, UserContractRepository>();

// Register services
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserContractService, UserContractService>();

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
