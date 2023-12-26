using BuildingBlocks;

namespace eShoppo.Payments.Contracts;

public record PaymentFailed(string OrderId, DateTime ProcessedAt) : IIntegrationEvent;