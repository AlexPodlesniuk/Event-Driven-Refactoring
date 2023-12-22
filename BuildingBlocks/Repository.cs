using Microsoft.Extensions.Logging;

namespace BuildingBlocks;

public class Repository<T> where T: AggregateRoot
{
    private readonly IEventPublisher _eventPublisher;
    private readonly ILogger<Repository<T>> _logger;
    private readonly Dictionary<string, T> _storage = new();
    public Repository(IEventPublisher eventPublisher, ILogger<Repository<T>> logger)
    {
        _eventPublisher = eventPublisher;
        _logger = logger;
    }

    public async Task Save(T aggregateRoot)
    {
        foreach (var @event in aggregateRoot.GetEvents())
        {
            await _eventPublisher.Publish(@event);
        }
        
        aggregateRoot.ClearEvents();

        Upsert(aggregateRoot.Id, aggregateRoot);
        _logger.LogInformation($"Saved aggregate {aggregateRoot.GetType().Name} {aggregateRoot.Id}");
    }
    
    public async Task<T?> GetById(string id)
    {
        return _storage.TryGetValue(id, out var existingValue) ? existingValue : null;
    }
    
    public async Task<T[]> GetAll()
    {
        return _storage.Values.ToArray();
    }
    
    public async Task Remove(string id)
    {
        _storage.Remove(id);
    }

    private void Upsert(string id, T aggregateRoot)
    {
        if (_storage.TryGetValue(id, out var existingValue))
        {
            _storage[id] = aggregateRoot;
        }
        else
        {
            _storage.Add(id, aggregateRoot);
        }
    }
    
}