using BuildingBlocks;

namespace eShoppo.Orders.Contracts;

public record OrderCreated(string OrderId, DateTime CreatedOn) : IDomainEvent;