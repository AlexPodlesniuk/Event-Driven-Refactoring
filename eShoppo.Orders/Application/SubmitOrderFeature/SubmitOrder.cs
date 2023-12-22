using eShoppo.Orders.Domain.Order;
using MediatR;

namespace eShoppo.Orders.Application.SubmitOrderFeature;

public record SubmitOrder(string OrderId) : IRequest<Order>;