using BuildingBlocks;
using eShoppo.Orders.Domain.OrderHistory;
using MediatR;

namespace eShoppo.Orders.Application.FindOrderHistoryByIdFeature;

internal class Handler : IRequestHandler<FindOrderHistoryById, OrderHistory?>
{
    private readonly Repository<OrderHistory> _repository;

    public Handler(Repository<OrderHistory> repository)
    {
        _repository = repository;
    }

    public async Task<OrderHistory?> Handle(FindOrderHistoryById request, CancellationToken cancellationToken)
    {
        return await _repository.GetById(request.OrderId);
    }
}