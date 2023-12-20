using BuildingBlocks;
using eShoppo.Orders.Domain;
using MediatR;

namespace eShoppo.Orders.Application;

internal class SubmitOrderHandler: IRequestHandler<SubmitOrder, Order>
{
    private readonly Repository _repository;

    public SubmitOrderHandler(Repository repository)
    {
        _repository = repository;
    }

    public Task<Order> Handle(SubmitOrder request, CancellationToken cancellationToken)
    {
        var order = new Order(request.OrderId);
        order.Submit();
        
        _repository.Save(order);
        
        return Task.FromResult(order);
    }
}