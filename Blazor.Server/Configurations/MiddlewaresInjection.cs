using Blazor.Server.Components;
using Serilog;

namespace Blazor.Server.Configurations;

public static class MiddlewaresInjection
{
	public static async Task AddMiddlewareInjection(this WebApplication app)
	{
		if (!app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/error", true);
			app.UseHsts();
			app.UseSerilogRequestLogging();
		}

		app.MapRazorPages();

		app.UseResponseCompression();
		app.UseStaticFiles();

		app.UseRouting();

		app.UseCors(builder =>
			builder.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader());

		app.MapHealthChecks("/health");

		app.UseRateLimiter();

		app.UseRequestLocalization();

		app.UseAuthentication();
		app.UseAuthorization();

		app.UseAntiforgery();

		app.MapStaticAssets();

		app.MapRazorComponents<App>()
			.AddInteractiveServerRenderMode();

		await Task.CompletedTask;
	}
}