using Blog.SeedWork;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.EventBus;

public static class EventBusServiceCollectionExtensions
{
    public static void AddEventBusModule(this IServiceCollection services)
    {
        services.AddSingleton<IDomainEventBus, DomainEventBus>();
    }
}