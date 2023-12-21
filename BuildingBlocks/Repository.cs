using Microsoft.Extensions.Logging;

namespace BuildingBlocks;

public class Repository
{
    private readonly IEventPublisher _eventPublisher;
    private readonly ILogger<Repository> _logger;


    public Repository(IEventPublisher eventPublisher, ILogger<Repository> logger)
    {
        _eventPublisher = eventPublisher;
        _logger = logger;
    }

    public async Task Save(AggregateRoot aggregateRoot)
    {
        foreach (var @event in aggregateRoot.GetEvents())
        {
            await _eventPublisher.Publish(@event);
        }
        
        aggregateRoot.ClearEvents();
        
        _logger.LogInformation($"Saved aggregate {aggregateRoot.GetType().Name} {aggregateRoot.Id}");
    }
}