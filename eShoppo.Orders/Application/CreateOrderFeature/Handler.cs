using BuildingBlocks;
using eShoppo.Orders.Domain.Order;
using MediatR;

namespace eShoppo.Orders.Application.CreateOrderFeature;

internal class Handler : IRequestHandler<CreateOrder, string>
{
    private readonly Repository<Order> _repository;

    public Handler(Repository<Order> repository)
    {
        _repository = repository;
    }

    public async Task<string> Handle(CreateOrder request, CancellationToken cancellationToken)
    {
        var order = Order.NewOrder(request.CustomerId, request.Items);
        order.Create();
        
        await _repository.Save(order);

        return order.Id;
    }
}