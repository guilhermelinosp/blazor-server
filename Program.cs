using System.Text.Json;
using Blazor.Components;
using Serilog;

var builder = WebApplication.CreateSlimBuilder(args);
var services = builder.Services;
var host = builder.Host;
var web = builder.WebHost;

services.AddRazorComponents()
	.AddInteractiveServerComponents();

host.UseSerilog((context, options) =>
	options.ReadFrom.Configuration(context.Configuration));

web.ConfigureKestrel((context, options) =>
	options.Configure(context.Configuration));

services.AddHealthChecks();

services.AddAntiforgery(options =>
{
	options.Cookie.Name = "XSRF-TOKEN";
	options.Cookie.SecurePolicy = CookieSecurePolicy.None;
	options.Cookie.SameSite = SameSiteMode.Strict;
	options.HeaderName = "X-XSRF-TOKEN";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	app.UseHsts();
}

app.UseAntiforgery();
app.UseHttpsRedirection();
app.UseHealthChecks("/health");
app.UseSerilogRequestLogging();
app.MapStaticAssets();
app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

await app.RunAsync();