using System.IO.Compression;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Diagnostics.HealthChecks;

public static class ServicesInjections
{
    public static async Task AddServicesInjections(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
            options.Providers.Clear();
            options.Providers.Add<GzipCompressionProvider>();
            options.Providers.Add<BrotliCompressionProvider>();
            options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
            {
                "application/octet-stream",
                "application/xml",
                "application/zip",
                "application/x-7z-compressed",
                "text/css",
                "text/html",
                "text/plain",
                "image/svg+xml",
                "image/png",
                "image/jpeg"
            });
        });

        services.Configure<GzipCompressionProviderOptions>(options =>
        {
            options.Level = CompressionLevel.Optimal;
        });

        services.Configure<BrotliCompressionProviderOptions>(options =>
        {
            options.Level = CompressionLevel.Optimal;
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

        services.AddAntiforgery(options =>
        {
            options.Cookie.Name = "XSRF-TOKEN";
            options.Cookie.SecurePolicy = configuration.GetValue<bool>("IsProduction") ? CookieSecurePolicy.Always : CookieSecurePolicy.None;
            options.Cookie.SameSite = SameSiteMode.Strict;
            options.HeaderName = "XSRF-TOKEN";
        });

        services.AddHealthChecks();

        services.AddLocalization(options => options.ResourcesPath = "Resources");

        services.AddMemoryCache(options =>
        {
            options.SizeLimit = 1024;
        });
        
        services.AddRateLimiter(options =>
        {
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(
                context => RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: $"{context.Request.Path}:{context.Request.Method}",
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 100,
                        Window = TimeSpan.FromMinutes(1),
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        QueueLimit = 10
                    }));
        });

        services.AddRazorComponents()
            .AddInteractiveServerComponents();

        await Task.CompletedTask;
    }
}
