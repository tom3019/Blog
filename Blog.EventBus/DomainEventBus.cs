using Blog.SeedWork;

namespace Blog.EventBus;

public class DomainEventBus : IDomainEventBus
{
    public Task PublishAsync<T>(T @event) where T : DomainEvent
    {
        //EventBus.Publish(domainEvent);
        throw new NotImplementedException();
    }

    public Task DispatchEventPublishAsync<TId>(AggregateRoot<TId> aggregate) where TId : ValueObject<TId>
    {
        var domainEvents = aggregate.GetChanges();
        foreach (var domainEvent in domainEvents)
        {
            //EventBus.Publish(domainEvent);
        }
        aggregate.ClearChanges();

        throw new NotImplementedException();
    }
}