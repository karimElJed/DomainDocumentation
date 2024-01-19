namespace DomainDrivenDesign.DiagramGenerators.Diagrams.StateTransitions;

public class State : DiagramObject
{
    public State(Type implementingType) : base(implementingType)
    {
    }

    public State(string identifier) : base(identifier)
    {
    }

    public override string ToPlantUml()
    {
        return "todo";
    }
}