using DomainDrivenDesign.Core.Attributes;

namespace DomainDrivenDesign.DiagramGenerators.UseCases;

public class Relation : DiagramObject
{
    public Relation(Actor from, UseCase to) : base($"{from.Identifier}_{to.Identifier}")
    {
        From = from;
        To = to;
    }
    
    public Relation(UseCase from, UseCase to) : base($"{from.Identifier}_{to.Identifier}")
    {
        From = from;
        To = to;
    }
    
    public DiagramObject From { get; }
    public DiagramObject To { get; }

    public override string ToPlantUml()
    {
        return $"{From} --> {To}";
    }
}