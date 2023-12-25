using System.Reflection;
using MassTransit;

namespace BuildingBlocks;

public class ConsumerTypeFinder
{
    public static Type[] FindConsumerTypesInAssembly(Assembly assembly)
    {
        var consumerTypes = assembly.GetTypes()
            .Where(type => type.GetInterfaces().Any(i => 
                i.IsGenericType && 
                i.GetGenericTypeDefinition() == typeof(IConsumer<>)))
            .ToArray();

        return consumerTypes;
    }
}