using BuildingBlocks;

namespace eShoppo.Payments.Contracts;

public record PaymentExpired(string OrderId) : IDomainEvent;