namespace Blog.SeedWork;

public interface IDomainEventBus
{
    Task PublishAsync<T>(T @event) where T : DomainEvent;
    
    Task DispatchEventPublishAsync<TId>(AggregateRoot<TId> aggregate) where TId :  ValueObject<TId>;
}