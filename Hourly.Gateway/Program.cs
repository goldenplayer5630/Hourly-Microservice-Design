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

var allowedOrigins = corsOptions.AllowedOrigins;
var allowedMethods = string.Join(", ", corsOptions.AllowedMethods);
var allowedHeaders = string.Join(", ", corsOptions.AllowedHeaders);
var exposedHeaders = string.Join(", ", corsOptions.ExposedHeaders);

app.Use(async (context, next) =>
{
    var logger = app.Services.GetRequiredService<ILoggerFactory>().CreateLogger("CorsDebugMiddleware");
    var origin = context.Request.Headers["Origin"].ToString();

    logger.LogDebug("Incoming request: {Method} {Path}", context.Request.Method, context.Request.Path);
    logger.LogDebug("Request Origin header: {Origin}", origin);

    context.Response.Headers["Access-Control-Allow-Origin"] = origin;
    logger.LogDebug("Setting Access-Control-Allow-Origin header to: {Origin}", origin);
    //if (!string.IsNullOrEmpty(origin) && allowedOrigins.Contains(origin))
    //{
    //    context.Response.Headers["Access-Control-Allow-Origin"] = origin;
    //    context.Response.Headers["Vary"] = "Origin";
    //    logger.LogDebug("Origin '{Origin}' is allowed. Setting Access-Control-Allow-Origin and Vary headers.", origin);
    //}
    //else
    //{
    //    logger.LogDebug("Origin '{Origin}' is not allowed or not present. Skipping Access-Control-Allow-Origin.", origin);
    //}

    context.Response.Headers["Access-Control-Allow-Methods"] = allowedMethods;
    context.Response.Headers["Access-Control-Allow-Headers"] = allowedHeaders;
    context.Response.Headers["Access-Control-Expose-Headers"] = exposedHeaders;

    logger.LogDebug("Set Access-Control-Allow-Methods: {Methods}", allowedMethods);
    logger.LogDebug("Set Access-Control-Allow-Headers: {Headers}", allowedHeaders);
    logger.LogDebug("Set Access-Control-Expose-Headers: {Headers}", exposedHeaders);

    if (corsOptions.AllowCredentials)
    {
        context.Response.Headers["Access-Control-Allow-Credentials"] = "true";
        logger.LogDebug("AllowCredentials is enabled. Set Access-Control-Allow-Credentials: true");
    }
    else
    {
        logger.LogDebug("AllowCredentials is disabled. Not setting Access-Control-Allow-Credentials.");
    }

    if (context.Request.Method == HttpMethod.Options.Method)
    {
        logger.LogDebug("OPTIONS preflight request detected. Returning 204 No Content.");
        context.Response.StatusCode = 204;
        await context.Response.CompleteAsync();
        return;
    }

    logger.LogDebug("Proceeding to next middleware.");
    await next();
});

await app.UseOcelot();
app.Run();

