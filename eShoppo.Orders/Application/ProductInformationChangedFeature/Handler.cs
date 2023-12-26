using BuildingBlocks;
using eShoppo.Catalog.Contracts;
using eShoppo.Orders.Domain.Product;
using MassTransit;

namespace eShoppo.Orders.Application.ProductInformationChangedFeature;

internal class Handler : IConsumer<ProductInformationChanged>
{
    private readonly Repository<OrderProduct> _repository;

    public Handler(Repository<OrderProduct> repository)
    {
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<ProductInformationChanged> context)
    {
        await _repository.Save(new OrderProduct(context.Message.ProductId, context.Message.Price));
    }
}