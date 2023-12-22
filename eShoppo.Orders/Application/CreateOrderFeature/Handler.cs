using BuildingBlocks;
using eShoppo.Catalog.Contracts;
using eShoppo.Orders.Domain.Order;
using MassTransit;
using MediatR;

namespace eShoppo.Orders.Application.CreateOrderFeature;

internal class Handler : IRequestHandler<CreateOrder, string>
{
    private readonly Repository<Order> _repository;
    private readonly IRequestClient<FindProductRequest> _client;

    public Handler(Repository<Order> repository, IRequestClient<FindProductRequest> client)
    {
        _repository = repository;
        _client = client;
    }

    public async Task<string> Handle(CreateOrder request, CancellationToken cancellationToken)
    {
        var order = Order.NewOrder(request.CustomerId);
        
        foreach (var item in request.Items)
        {
            var product = await _client.GetResponse<ProductDto>(new FindProductRequest(item.ProductId), cancellationToken);
            order.AddOrderLine(item, product.Message.Price);
        }
        
        order.Create();
        
        await _repository.Save(order);

        return order.Id;
    }
}