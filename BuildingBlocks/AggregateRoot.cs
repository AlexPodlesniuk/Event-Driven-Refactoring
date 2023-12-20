namespace BuildingBlocks;

public abstract class AggregateRoot
{
    private readonly List<IDomainEvent> _domainEvents = new();
    public string Id { get; init; }

    protected AggregateRoot(string id)
    {
        Id = id;
    }
    
    protected void RaiseEvent(IDomainEvent @event) => _domainEvents.Add(@event);
    public void ClearEvents() => _domainEvents.Clear();
    public IReadOnlyCollection<IDomainEvent> GetEvents() => _domainEvents.AsReadOnly();
}