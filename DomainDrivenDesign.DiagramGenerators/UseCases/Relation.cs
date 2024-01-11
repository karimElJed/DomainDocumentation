namespace DomainDrivenDesign.DiagramGenerators.UseCases;

public class Relation
{
    public Relation(DiagramObject from, DiagramObject to)
    {
        From = from;
        To = to;
    }
    
    public DiagramObject From { get; }
    public DiagramObject To { get; }
}