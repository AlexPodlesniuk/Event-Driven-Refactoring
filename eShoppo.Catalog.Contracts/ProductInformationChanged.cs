using BuildingBlocks;

namespace eShoppo.Catalog.Contracts;

public record ProductInformationChanged(string ProductId, string Name, string Sku, decimal Price, int MaxPaymentTime) : IIntegrationEvent;