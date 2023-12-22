using BuildingBlocks;

namespace eShoppo.Payments.Contracts;

public record PaymentSuccessful(string OrderId) : IDomainEvent;