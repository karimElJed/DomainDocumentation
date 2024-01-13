namespace DomainDrivenDesign.DiagramGenerators.Diagrams.UseCases;

public class UseCase : DiagramObject
{
    public UseCase(string identifier) 
        : base(identifier)
    {
    }

    public override string ToPlantUml()
    {
        return $"\"{Title}\" as ({Identifier})";
    }

    public override string ToString()
    {
        return $"({Identifier})";
    }
}