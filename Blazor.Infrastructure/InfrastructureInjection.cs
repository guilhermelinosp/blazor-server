using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blazor.Infrastructure;

public static class InfrastructureInjection
{
    public static async Task AddInfrastructureInjection(this IServiceCollection services, IConfiguration configuration)
    {
        
        await Task.CompletedTask;
    }
}
