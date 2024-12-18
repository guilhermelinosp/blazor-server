using Blazor.Application;
using Blazor.Server.Configurations;

var builder = WebApplication.CreateBuilder(args);

await builder.Host.AddHostInjection();

await builder.Services.AddServicesInjections(builder.Configuration);
await builder.Services.AddApplicationInjection(builder.Configuration);

var app = builder.Build();

await app.AddMiddlewareInjection();

await app.RunAsync();