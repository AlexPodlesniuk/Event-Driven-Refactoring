namespace eShoppo.Catalog.Contracts;

public record FindProductResponse(string ProductId, string Name, decimal Price, int MaxPaymentTime, string Sku);