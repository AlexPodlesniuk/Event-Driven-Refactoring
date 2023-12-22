namespace BuildingBlocks;

public class AggregateNotFoundException : Exception
{
    public string AggregateId { get; }
    public Type AggregateType { get; }

    public AggregateNotFoundException(string id, Type type)
        : base($"Aggregate of type '{type.Name}' with ID '{id}' was not found.")
    {
        AggregateId = id;
        AggregateType = type;
    }
    
    public override string ToString()
    {
        return $"AggregateNotFoundException: {Message}, Aggregate ID: {AggregateId}, Aggregate Type: {AggregateType.FullName}";
    }
}