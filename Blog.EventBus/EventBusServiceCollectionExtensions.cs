using Blog.SeedWork;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.EventBus;

public static class EventBusServiceCollectionExtensions
{
    public static IServiceCollection AddEventBusModule(this IServiceCollection services)
    {
        services.AddSingleton<IDomainEventBus, DomainEventBus>();
        return services;
    }
}