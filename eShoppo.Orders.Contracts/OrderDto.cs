namespace eShoppo.Orders.Contracts;

public record OrderDto(string OrderId, string CustomerId, int TotalItems, IEnumerable<OrderItemDto> OrderItems);

public record OrderItemDto(string ProductId, int Quantity);