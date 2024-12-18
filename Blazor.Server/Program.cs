using Blazor.Application;
using Blazor.Infrastructure;
using Blazor.Server.Configurations;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, options) =>
    options.ReadFrom.Configuration(context.Configuration));

builder.WebHost.ConfigureKestrel((context, options) =>
    options.Configure(context.Configuration));

await builder.Services.AddServicesInjections(builder.Configuration);
await builder.Services.AddApplicationInjection(builder.Configuration);
await builder.Services.AddInfrastructureInjection(builder.Configuration);

var app = builder.Build();

await app.AddMiddlewareInjection();

await app.RunAsync();