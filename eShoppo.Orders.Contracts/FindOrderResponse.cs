namespace eShoppo.Orders.Contracts;

public record FindOrderResponse(string OrderId, string OrderNumber, string CustomerId, decimal TotalPrice, IEnumerable<OrderItemDto> OrderItems);

public record OrderItemDto(string ProductId, int Quantity);