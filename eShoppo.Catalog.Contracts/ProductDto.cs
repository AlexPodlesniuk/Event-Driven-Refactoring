namespace eShoppo.Catalog.Contracts;

public record ProductDto(string ProductId, string Name, decimal Price, int MaxPaymentTime, string Sku);