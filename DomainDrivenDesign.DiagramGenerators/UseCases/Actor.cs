namespace DomainDrivenDesign.DiagramGenerators.UseCases;

public class Actor : DiagramObject
{
    public Actor(string identifier) : base(identifier)
    {
    }

    public override string ToPlantUml()
    {
        return $"actor \"{Title}\" as {Identifier}";
    }

    public override string ToString()
    {
        return Identifier;
    }
}