namespace DomainDrivenDesign.DiagramGenerators.UseCases;

public class UseCase : DiagramObject
{
    public UseCase(string identifier) 
        : base(identifier)
    {
    }

    public string ToPlantUml()
    {
        return $"\"{Title}\" as ({Identifier})";
    }

    public override string ToString()
    {
        return $"({Identifier})";
    }
}