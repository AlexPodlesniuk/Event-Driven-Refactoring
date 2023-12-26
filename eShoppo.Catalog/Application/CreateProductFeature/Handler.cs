using BuildingBlocks;
using eShoppo.Catalog.Domain;
using MediatR;

namespace eShoppo.Catalog.Application.CreateProductFeature;

internal class Handler : IRequestHandler<CreateProduct, string>
{
    private readonly Repository<Product> _repository;

    public Handler(Repository<Product> repository)
    {
        _repository = repository;
    }

    public async Task<string> Handle(CreateProduct request, CancellationToken cancellationToken)
    {
        var product = new Product(Guid.NewGuid().ToString())
        {
            Name = request.Name,
            Price = request.Price,
            Sku = request.Sku,
            MaxPaymentTime = request.MaxPaymentTime
        };
        
        product.MarkAsInformationChanged();
        
        await _repository.Save(product);

        return product.Id;
    }
}