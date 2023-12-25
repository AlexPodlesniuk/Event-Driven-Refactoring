using BuildingBlocks;

namespace eShoppo.Payments.Contracts;

public record OrderPaid(string OrderId, DateTime SubmittedOn) : IDomainEvent;