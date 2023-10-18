using Application.Services;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyResolverService;

public static class DependencyResolverService
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddHttpClient<IDadJokeClient, DadJokeClient>(options => {
            options.BaseAddress = new Uri("https://icanhazdadjoke.com/");
            options.DefaultRequestHeaders.Add("Accept", "application/json");
            options.DefaultRequestHeaders.Add("User-Agent", "My Library (https://github.com/username/reposetlater)");
        });
    }
}