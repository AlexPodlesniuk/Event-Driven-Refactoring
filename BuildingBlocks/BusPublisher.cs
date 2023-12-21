using MassTransit;

namespace BuildingBlocks;

public class BusPublisher : IEventPublisher
{
    private readonly IBus _bus;

    public BusPublisher(IBus bus)
    {
        _bus = bus;
    }

    public Task Publish(IDomainEvent domainEvent)
    {
        return _bus.Publish(domainEvent, domainEvent.GetType());
    }
}