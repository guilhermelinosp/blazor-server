using Blazor.Application;
using Blazor.Server.Configurations;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, options) =>
	options.ReadFrom.Configuration(context.Configuration));

await builder.Services.AddServicesInjections(builder.Configuration);
await builder.Services.AddApplicationInjection(builder.Configuration);

var app = builder.Build();

await app.AddMiddlewareInjection();

await app.RunAsync();