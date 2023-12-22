using MediatR;

namespace eShoppo.Payments.Application.PayOrderFeature;

public record PayOrder(string OrderId) : IRequest<Unit>;