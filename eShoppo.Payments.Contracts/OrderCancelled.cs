using BuildingBlocks;

namespace eShoppo.Payments.Contracts;

public record OrderCancelled(string OrderId, DateTime SubmittedOn) : IDomainEvent;