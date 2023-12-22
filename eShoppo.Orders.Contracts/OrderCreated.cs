using BuildingBlocks;

namespace eShoppo.Orders.Contracts;

public record OrderCreated(string Id, DateTime CreatedOn) : IDomainEvent;