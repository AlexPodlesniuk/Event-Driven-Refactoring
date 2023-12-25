using BuildingBlocks;
using eShoppo.Catalog.Contracts;
using eShoppo.Catalog.Domain;
using MassTransit;

namespace eShoppo.Catalog.Application.FindProductFromRequest;

public class Handler : IConsumer<FindProductRequest>
{
    private readonly Repository<Product> _repository;

    public Handler(Repository<Product> repository)
    {
        _repository = repository;
    }
    
    public async Task Consume(ConsumeContext<FindProductRequest> context)
    {
        var product = await _repository.GetById(context.Message.ProductId);
        await context.RespondAsync(new FindProductResponse(product.Id, product.Name, product.Price, product.MaxPaymentTime, product.Sku));
    }
}