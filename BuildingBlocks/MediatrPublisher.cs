using MediatR;

namespace BuildingBlocks;

public class MediatrPublisher : IEventPublisher
{
    private readonly IPublisher _publisher;

    public MediatrPublisher(IPublisher publisher) =>  _publisher = publisher;

    public void Publish(IDomainEvent domainEvent)
    {
        _publisher.Publish(domainEvent);
    }
}

public interface IEventPublisher
{
    void Publish(IDomainEvent domainEvent);
}