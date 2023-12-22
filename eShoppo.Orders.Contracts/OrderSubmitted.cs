using BuildingBlocks;

namespace eShoppo.Orders.Contracts;

public record OrderSubmitted(string OrderId, DateTime SubmittedOn) : IDomainEvent;