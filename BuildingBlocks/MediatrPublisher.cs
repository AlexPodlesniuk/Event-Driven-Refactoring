using MediatR;

namespace BuildingBlocks;

public class MediatrPublisher : IEventPublisher
{
    private readonly IPublisher _publisher;

    public MediatrPublisher(IPublisher publisher) =>  _publisher = publisher;

    public Task Publish(IDomainEvent domainEvent)
    {
        return _publisher.Publish(domainEvent);
    }
}