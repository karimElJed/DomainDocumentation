namespace DomainDrivenDesign.DiagramGenerators.Diagrams.UseCases;

public class Actor : DiagramObject
{
    public Actor(string identifier) : base(identifier)
    {
    }

    public string? Stereotype { get; set; }

    public override string ToPlantUml()
    {
        var stereotype = string.IsNullOrWhiteSpace(Stereotype) ? string.Empty : $"<<{Stereotype}>>";
        return $"actor \"{Title}\"{stereotype} as {Identifier}";
    }

    public override string ToString()
    {
        return Identifier;
    }
}