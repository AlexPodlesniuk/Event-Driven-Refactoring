using MediatR;

namespace eShoppo.Catalog.Application.CreateProductFeature;

public record CreateProduct(string Name, decimal Price, int MaxPaymentTime, string Sku) : IRequest<string>;