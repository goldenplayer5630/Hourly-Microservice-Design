using Hourly.Gateway;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

var corsOptions = builder.Configuration
    .GetSection("CORS")
    .Get<CorsSettings>();

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

// Load ocelot config
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// Add Ocelot
builder.Services.AddOcelot();

var app = builder.Build();

app.UseCors("CORS");
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

// Enable Ocelot
await app.UseOcelot();

app.Run();
