using BuildingBlocks;
using MassTransit;

namespace eShoppo.Payments;

public static class ServiceBusExtensions
{
    public static IBusRegistrationConfigurator RegisterPaymentConsumers(
        this IBusRegistrationConfigurator busRegistrationConfigurator)
    {
        var consumerTypes = ConsumerTypeFinder.FindConsumerTypesInAssembly(typeof(PaymentsTypeReference).Assembly);
        busRegistrationConfigurator.AddConsumers(consumerTypes);
        return busRegistrationConfigurator;
    }
}