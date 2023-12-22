using BuildingBlocks;

namespace eShoppo.Orders.Contracts;

public record OrderCancelled(string OrderId, DateTime SubmittedOn) : IDomainEvent;