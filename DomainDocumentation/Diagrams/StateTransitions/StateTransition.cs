namespace DomainDrivenDesign.DiagramGenerators.Diagrams.StateTransitions;

internal class StateTransition : DiagramObject
{
    public StateTransition(State from, State to, string? trigger) : base("")
    {
        From = from;
        To = to;
        Trigger = trigger;
    }

    public State From { get; }
    public State To { get; }
    public string? Trigger { get; }
    
    public override string ToPlantUml()
    {
        var trigger = string.IsNullOrWhiteSpace(Trigger) ? "" : $" : {Trigger}";
        return $"{From.Identifier} --> {To.Identifier}{trigger}";
    }
}