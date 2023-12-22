using BuildingBlocks;
using eShoppo.Payments.Contracts;

namespace eShoppo.Payments.Domain;

public class PaymentPromise(string id) : AggregateRoot(id)
{
    public DateTime ExpiredAt { get; init; }
    public PaymentPromiseStatus Status { get; private set; } = PaymentPromiseStatus.WaitingForPayment;

    public bool WaitingForPayment => Status == PaymentPromiseStatus.WaitingForPayment;
    public void MarkAsPaid()
    {
        Status = PaymentPromiseStatus.Paid;
        RaiseEvent(new PaymentSuccessful(id));
    }
    
    public void MarkAsExpired()
    {
        Status = PaymentPromiseStatus.Expired;
        RaiseEvent(new PaymentExpired(id));
    }
}

public enum PaymentPromiseStatus
{
    Paid,
    WaitingForPayment,
    Expired
}