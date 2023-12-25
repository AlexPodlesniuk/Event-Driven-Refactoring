using BuildingBlocks;
using MassTransit;

namespace eShoppo.Inventory;

public static class ServiceBusExtensions
{
    public static IBusRegistrationConfigurator RegisterInventoryConsumers(
        this IBusRegistrationConfigurator busRegistrationConfigurator)
    {
        var consumerTypes = ConsumerTypeFinder.FindConsumerTypesInAssembly(typeof(InventoryTypeReference).Assembly);
        busRegistrationConfigurator.AddConsumers(consumerTypes);
        return busRegistrationConfigurator;
    }
}