using Blazor.Server.Components;
using Microsoft.AspNetCore.ResponseCompression;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var host = builder.Host;
var web = builder.WebHost;

services.AddRazorComponents()
	.AddInteractiveServerComponents();

services.AddResponseCompression(options =>
{
	options.EnableForHttps = true;
	options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(["application/octet-stream"]);
});

services.AddServerSideBlazor()
	.AddCircuitOptions(options =>
	{
		options.DetailedErrors = true;
		options.JSInteropDefaultCallTimeout = TimeSpan.FromSeconds(30);
	});

services.AddControllersWithViews();
services.AddRazorPages();

services.AddHttpClient();

services.AddLogging(logging =>
{
	logging.ClearProviders();
	logging.AddSerilog();
});
host.UseSerilog((context, options) =>
	options.ReadFrom.Configuration(context.Configuration));

web.ConfigureKestrel((context, options) =>
	options.Configure(context.Configuration));

services.AddAntiforgery(options =>
{
	options.Cookie.Name = "XSRF-TOKEN";
	options.Cookie.SecurePolicy = CookieSecurePolicy.None;
	options.Cookie.SameSite = SameSiteMode.Strict;
	options.HeaderName = "XSRF-TOKEN";
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	app.UseHsts();
	app.UseSerilogRequestLogging();
}

app.UseAntiforgery();
app.UseResponseCompression();
app.UseStaticFiles();
app.MapStaticAssets();
app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

await app.RunAsync();