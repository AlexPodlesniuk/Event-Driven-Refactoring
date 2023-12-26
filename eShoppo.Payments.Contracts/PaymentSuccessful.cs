using BuildingBlocks;

namespace eShoppo.Payments.Contracts;

public record PaymentSuccessful(string OrderId, DateTime ProcessedAt) : IIntegrationEvent;