namespace DomainDrivenDesign.DiagramGenerators.Diagrams.States;

internal class StateTransition : DiagramObject
{
    public StateTransition(Type implementingType) : base(implementingType)
    {
    }

    public StateTransition(string identifier) : base(identifier)
    {
    }

    public override string ToPlantUml()
    {
        return "todo";
    }
}