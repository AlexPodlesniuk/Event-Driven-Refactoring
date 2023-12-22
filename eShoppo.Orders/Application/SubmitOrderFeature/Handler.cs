using BuildingBlocks;
using eShoppo.Orders.Domain.Order;
using MediatR;

namespace eShoppo.Orders.Application.SubmitOrderFeature;

internal class Handler: IRequestHandler<SubmitOrder, Order>
{
    private readonly Repository<Order> _repository;

    public Handler(Repository<Order> repository)
    {
        _repository = repository;
    }

    public async Task<Order> Handle(SubmitOrder request, CancellationToken cancellationToken)
    {
        var order = await _repository.GetById(request.OrderId) ?? throw new OrderNotFoundException(request.OrderId);
        order.Submit();
        
        await _repository.Save(order);

        return order;
    }
}