using eShoppo.Orders.Domain.Order;
using MediatR;

namespace eShoppo.Orders.Application.FindOrderByIdFeature;

public record FindOrderById(string OrderId) : IRequest<Order?>;