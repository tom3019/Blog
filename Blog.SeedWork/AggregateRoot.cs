namespace Blog.SeedWork;

/// <summary>
/// Aggregate Root
/// </summary>
/// <typeparam name="TId"></typeparam>
public abstract class AggregateRoot<TId> : IInternalEventHandler where TId : ValueObject<TId>
{
    public TId Id { get; protected set; }
    public int Version { get; private set; } = -1;

    private readonly IList<DomainEvent> _changes;
    protected AggregateRoot() => _changes = new List<DomainEvent>();
    protected abstract void When(object @event);
    protected abstract void EnsureValidState();
    protected void Apply(DomainEvent @event)
    {
        When(@event);
        EnsureValidState();
        _changes.Add(@event);
    }

    public IEnumerable<DomainEvent> GetChanges() => _changes.AsEnumerable();
    public void ClearChanges() => _changes.Clear();
    protected void ApplyToEntity(IInternalEventHandler entity, DomainEvent @event) => entity?.Handle(@event);
    void IInternalEventHandler.Handle(DomainEvent @event) => When(@event);

    public void Load(IEnumerable<object> history)
    {
        foreach (var e in history)
        {
            When(e);
            Version++;
        }
    }
}