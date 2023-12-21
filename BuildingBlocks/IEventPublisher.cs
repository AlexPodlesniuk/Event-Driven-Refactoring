namespace BuildingBlocks;

public interface IEventPublisher
{
    Task Publish(IDomainEvent domainEvent);
}