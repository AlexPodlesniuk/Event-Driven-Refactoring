using BuildingBlocks;
using eShoppo.Orders.Domain.Order;
using MediatR;

namespace eShoppo.Orders.Application.FindOrderByIdFeature;

internal class Handler : IRequestHandler<FindOrderById, Order?>
{
    private readonly Repository<Order> _repository;

    public Handler(Repository<Order> repository)
    {
        _repository = repository;
    }

    public Task<Order?> Handle(FindOrderById request, CancellationToken cancellationToken)
    {
        return _repository.GetById(request.OrderId);
    }
}