using System.ComponentModel;

namespace DomainDrivenDesign.Core.Attributes;

public class StateAttribute : Attribute
{
    
}

public class StartAttribute : Attribute
{
    
}

[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
public class TransformsIntoAttribute<TState> : Attribute
{
    public TransformsIntoAttribute(TState nextState) 
    {
        NextState = nextState;
    }
    
    public TState NextState { get; }
    
    public string Trigger { get; init; }
}