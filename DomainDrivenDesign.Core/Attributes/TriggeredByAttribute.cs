namespace DomainDrivenDesign.Core.Attributes;

public class TriggeredByAttribute : Attribute
{
    public TriggeredByAttribute(Type actorType)
    {
        ActorType = actorType;
    }
    
    public Type ActorType { get; }
}