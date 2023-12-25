using BuildingBlocks;
using MassTransit;

namespace eShoppo.Orders;

public static class ServiceBusExtensions
{
    public static IBusRegistrationConfigurator RegisterOrderConsumers(
        this IBusRegistrationConfigurator busRegistrationConfigurator)
    {
        var consumerTypes = ConsumerTypeFinder.FindConsumerTypesInAssembly(typeof(OrdersTypeReference).Assembly);
        busRegistrationConfigurator.AddConsumers(consumerTypes);
        return busRegistrationConfigurator;
    }
}