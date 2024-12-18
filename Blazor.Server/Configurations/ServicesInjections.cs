using System.Threading.RateLimiting;
using Blazor.Application;
using Microsoft.AspNetCore.ResponseCompression;
using Serilog;

namespace Blazor.Server.Configurations;

public static class ServicesInjections
{
	public static async Task AddServicesInjections(this IServiceCollection services, IConfiguration configuration)
	{ 
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
		
		services.AddAntiforgery(options =>
		{
			options.Cookie.Name = "XSRF-TOKEN";
			options.Cookie.SecurePolicy = CookieSecurePolicy.None;
			options.Cookie.SameSite = SameSiteMode.Strict;
			options.HeaderName = "XSRF-TOKEN";
		});
		
		services.AddHealthChecks();

		services.AddLocalization(options => options.ResourcesPath = "Resources");
		
		services.AddMemoryCache();
		services.AddDistributedMemoryCache(); 

		services.AddRateLimiter(options =>
		{
			options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(
				context => RateLimitPartition.GetFixedWindowLimiter(
					partitionKey: context.Request.Path.ToString(),
					factory: _ => new FixedWindowRateLimiterOptions
					{
						PermitLimit = 100,
						Window = TimeSpan.FromMinutes(1),
						QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
						QueueLimit = 10
					}));
		});
        
		await Task.CompletedTask;
	}
}