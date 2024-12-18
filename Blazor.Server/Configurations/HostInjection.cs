using Serilog;

namespace Blazor.Server.Configurations;

public static class HostInjection
{
	public static async Task AddHostInjection(this IHostBuilder host)
	{
		host.UseSerilog((context, options) =>
			options.ReadFrom.Configuration(context.Configuration));
        
		await Task.CompletedTask;
	}
}