using BuildingBlocks;

namespace eShoppo.Orders.Contracts;

public record OrderPaid(string OrderId, DateTime SubmittedOn) : IDomainEvent;