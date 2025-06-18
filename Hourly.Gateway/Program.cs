using Hourly.Gateway;
using Microsoft.Extensions.Configuration;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Configuration.File;
using Ocelot.Configuration.Repository;

var builder = WebApplication.CreateBuilder(args);

// -------------------------
// Load CORS Settings
// -------------------------
var corsOptions = builder.Configuration
    .GetSection("CORS")
    .Get<CorsSettings>();

if (corsOptions == null)
{
    throw new InvalidOperationException("CORS settings are not configured properly.");
}

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

// -------------------------
// Build Ocelot Configuration from appsettings.json
// -------------------------
var ocelotConfigPath = "ocelot.generated.json";

var config = builder.Configuration.GetSection("OCELOT");
string scheme = config["HTTP_TRANSPORT_SCHEME"] ?? "http";

// Construct configuration
var fileConfig = new FileConfiguration
{
    Routes = new List<FileRoute>
        {
            new()
            {
                DownstreamPathTemplate = "/api/user/{everything}",
                DownstreamScheme = scheme,
                DownstreamHostAndPorts = new List<FileHostAndPort>
                {
                    new() { Host = config["USER_SERVICE_HOST"], Port = int.Parse(config["USER_SERVICE_PORT"]!) }
                },
                UpstreamPathTemplate = "/api/user/{everything}",
                UpstreamHttpMethod = new() { "GET", "POST", "PATCH", "PUT", "DELETE", "OPTIONS" }
            },
            new()
            {
                DownstreamPathTemplate = "/api/usercontract/{everything}",
                DownstreamScheme = scheme,
                DownstreamHostAndPorts = new List<FileHostAndPort>
                {
                    new() { Host = config["USER_SERVICE_HOST"], Port = int.Parse(config["USER_SERVICE_PORT"]!) }
                },
                UpstreamPathTemplate = "/api/usercontract/{everything}",
                UpstreamHttpMethod = new() { "GET", "POST", "PATCH", "PUT", "DELETE", "OPTIONS" }
            },
            new()
            {
                DownstreamPathTemplate = "/api/department/{everything}",
                DownstreamScheme = scheme,
                DownstreamHostAndPorts = new List<FileHostAndPort>
                {
                    new() { Host = config["USER_SERVICE_HOST"], Port = int.Parse(config["USER_SERVICE_PORT"]!) }
                },
                UpstreamPathTemplate = "/api/department/{everything}",
                UpstreamHttpMethod = new() { "GET", "POST", "PATCH", "PUT", "DELETE", "OPTIONS" }
            },
            new()
            {
                DownstreamPathTemplate = "/api/worksession/{everything}",
                DownstreamScheme = scheme,
                DownstreamHostAndPorts = new List<FileHostAndPort>
                {
                    new() { Host = config["TIMETRACKING_SERVICE_HOST"], Port = int.Parse(config["TIMETRACKING_SERVICE_PORT"]!) }
                },
                UpstreamPathTemplate = "/api/worksession/{everything}",
                UpstreamHttpMethod = new() { "GET", "POST", "PATCH", "PUT", "DELETE", "OPTIONS" }
            },
            new()
            {
                DownstreamPathTemplate = "/api/gitcommit/{everything}",
                DownstreamScheme = scheme,
                DownstreamHostAndPorts = new List<FileHostAndPort>
                {
                    new() { Host = config["GIT_SERVICE_HOST"], Port = int.Parse(config["GIT_SERVICE_PORT"]!) }
                },
                UpstreamPathTemplate = "/api/gitcommit/{everything}",
                UpstreamHttpMethod = new() { "GET", "POST", "PATCH", "PUT", "DELETE", "OPTIONS" }
            },
            new()
            {
                DownstreamPathTemplate = "/api/gitrepository/{everything}",
                DownstreamScheme = scheme,
                DownstreamHostAndPorts = new List<FileHostAndPort>
                {
                    new() { Host = config["GIT_SERVICE_HOST"], Port = int.Parse(config["GIT_SERVICE_PORT"]!) }
                },
                UpstreamPathTemplate = "/api/gitrepository/{everything}",
                UpstreamHttpMethod = new() { "GET", "POST", "PATCH", "PUT", "DELETE", "OPTIONS" }
            }
        },
    GlobalConfiguration = new FileGlobalConfiguration
    {
        BaseUrl = "https://localhost:9000"
    }
};

// Write config to disk
var json = System.Text.Json.JsonSerializer.Serialize(fileConfig, new System.Text.Json.JsonSerializerOptions
{
    PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase,
    WriteIndented = true
});
await File.WriteAllTextAsync(ocelotConfigPath, json);

// Register the config path
builder.Configuration.AddJsonFile(ocelotConfigPath, optional: false, reloadOnChange: true);
builder.Services.AddOcelot();

var app = builder.Build();

app.UseRouting();

app.UseCors("CORS");

await app.UseOcelot();
app.Run();
