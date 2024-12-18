using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blazor.Application;

public static class ApplicationInjection
{
    public static async Task AddApplicationInjection(this IServiceCollection services, IConfiguration configuration)
    {
        await Task.CompletedTask;
    }
}
