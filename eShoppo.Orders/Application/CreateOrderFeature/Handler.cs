using BuildingBlocks;
using eShoppo.Orders.Domain.Order;
using eShoppo.Orders.Domain.Product;
using MediatR;

namespace eShoppo.Orders.Application.CreateOrderFeature;

internal class Handler : IRequestHandler<CreateOrder, string>
{
    private readonly Repository<Order> _ordersRepository;
    private readonly Repository<OrderProduct> _productRepository;

    public Handler(Repository<Order> ordersRepository, Repository<OrderProduct> productRepository)
    {
        _ordersRepository = ordersRepository;
        _productRepository = productRepository;
    }


    public async Task<string> Handle(CreateOrder request, CancellationToken cancellationToken)
    {
        var order = Order.NewOrder(request.CustomerId);
        
        foreach (var item in request.Items)
        {
            var product = await _productRepository.GetById(item.ProductId);
            order.AddOrderLine(item, product.Price);
        }
        
        order.Create();
        
        await _ordersRepository.Save(order);

        return order.Id;
    }
}