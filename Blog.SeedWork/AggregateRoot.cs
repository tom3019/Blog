namespace Blog.SeedWork;

/// <summary>
/// Aggregate Root
/// </summary>
/// <typeparam name="TId"></typeparam>
public abstract class AggregateRoot<TId> : IInternalEventHandler
{
    public TId Id { get; protected set; }
    public int Version { get; private set; } = -1;

    private readonly IList<object> _changes;
    protected AggregateRoot() => _changes = new List<object>();
    protected abstract void When(object @event);
    protected abstract void EnsureValidState();
    protected void Apply(object @event)
    {
        When(@event);
        EnsureValidState();
        _changes.Add(@event);
    }

    public IEnumerable<object> GetChanges() => _changes.AsEnumerable();
    public void ClearChanges() => _changes.Clear();
    protected void ApplyToEntity(IInternalEventHandler entity, object @event) => entity?.Handle(@event);
    void IInternalEventHandler.Handle(object @event) => When(@event);

    public void Load(IEnumerable<object> history)
    {
        foreach (var e in history)
        {
            When(e);
            Version++;
        }
    }
}