using Microsoft.SemanticKernel;
using SalesIA.Plugins;
using SalesIA.Repositories;
using SalesIA.Services;

namespace SalesIA.Extensions;

public static class ApplicationBuilderServicesExtensions
{   
    private static void InjectPlugins(this IServiceCollection services, Type[] plugins)
    {
        foreach (var plugin in plugins)
        {
            services.AddScoped(plugin);
        }

        services.AddScoped((sp) =>
        {
            var pluginCollection = new KernelPluginCollection();

            foreach (var plugin in plugins)
            {
                var instance = sp.GetRequiredService(plugin);
                pluginCollection.AddFromObject(instance);
            }

            return new Kernel(sp, pluginCollection);
        });
    }
    
    public static void InjectDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOpenAIChatCompletion("gpt-4o-mini", configuration["Tokens:OpenAI"]!);

        services.AddSingleton(typeof(HistoryService));
        services.AddScoped(typeof(ChatService));

        services.AddScoped(typeof(CustomerRepository));
        services.AddScoped(typeof(ProductRepository));
        services.AddScoped(typeof(BuyedProductRepository));

        var plugins = new Type[] {
            typeof(CustomersPlugin),
            typeof(BuyedProductPlugin)
        };

        services.InjectPlugins(plugins);
    }
}
