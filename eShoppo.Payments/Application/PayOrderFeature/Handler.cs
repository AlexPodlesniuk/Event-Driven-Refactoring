using BuildingBlocks;
using eShoppo.Payments.Domain;
using MediatR;

namespace eShoppo.Payments.Application.PayOrderFeature;

internal class Handler : IRequestHandler<PayOrder, Unit>
{
    private readonly Repository<PaymentPromise> _repository;

    public Handler(Repository<PaymentPromise> repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(PayOrder request, CancellationToken cancellationToken)
    {
        var existedPromise = await _repository.GetById(request.OrderId) ?? throw new ArgumentNullException(nameof(PaymentPromise));
        existedPromise.MarkAsPaid();
        
        await _repository.Save(existedPromise);
        return Unit.Value;
    }
}