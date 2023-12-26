using BuildingBlocks;
using eShoppo.Catalog.Contracts;
using eShoppo.Inventory.Domain;
using MassTransit;

namespace eShoppo.Inventory.Application.ProductInformationChangedFeature;

internal class Handler : IConsumer<ProductInformationChanged>
{
    private readonly Repository<InventoryProduct> _repository;

    public Handler(Repository<InventoryProduct> repository)
    {
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<ProductInformationChanged> context)
    {
        await _repository.Save(new InventoryProduct(context.Message.ProductId, context.Message.Sku));
    }
}