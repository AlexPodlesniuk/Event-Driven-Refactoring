using eShoppo.Orders.Domain.OrderHistory;
using MediatR;

namespace eShoppo.Orders.Application.FindOrderHistoryByIdFeature;

public record FindOrderHistoryById(string OrderId) : IRequest<OrderHistory>;