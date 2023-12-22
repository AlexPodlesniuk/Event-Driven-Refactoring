using BuildingBlocks;
using eShoppo.Orders.Domain.Order;

namespace eShoppo.Orders.Domain.OrderHistory;

public class OrderHistory(string id) : AggregateRoot(id)
{
    private readonly List<HistoryItem> _history = new();

    public IReadOnlyList<HistoryItem> History => _history.AsReadOnly();

    public void Append(HistoryItem historyItem) => _history.Add(historyItem);
}

public record HistoryItem(OrderStatus Status, DateTime ChangedAt);