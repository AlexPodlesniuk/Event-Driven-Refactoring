using BuildingBlocks;
using eShoppo.Catalog.Contracts;
using eShoppo.Payments.Domain;
using MassTransit;

namespace eShoppo.Payments.Application.ProductInformationChangedFeature;

internal class Handler : IConsumer<ProductInformationChanged>
{
    private readonly Repository<PaymentProduct> _productRepository;

    public Handler(Repository<PaymentProduct> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Consume(ConsumeContext<ProductInformationChanged> context)
    {
        await _productRepository.Save(new PaymentProduct(context.Message.ProductId, context.Message.MaxPaymentTime));
    }
}