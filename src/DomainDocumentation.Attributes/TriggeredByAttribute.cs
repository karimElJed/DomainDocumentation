namespace DomainDocumentation.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class TriggeredByAttribute : Attribute
{
    public TriggeredByAttribute(Type actorType)
    {
        ActorType = actorType;
    }
    
    public Type ActorType { get; }

    public string Reason { get; init; }
}