namespace DomainDrivenDesign.DiagramGenerators.Diagrams.UseCases;

public class Actor : DiagramObject
{
    public Actor(Type implementingType) : base(implementingType)
    {
    }
    
    public static Actor Create(Type implementingType, string? stereotype,
        IDocumentationProvider documentationProvider)
    {
        var actor = new Actor(implementingType)
        {
            Stereotype = stereotype,
            Documentation = documentationProvider.GetDocumentation(implementingType)
        };

        return actor;
    }
    
    public static Actor Create(Type implementingType)
    {
        return Create(implementingType, null, new NoDocumentationProvider());
    }

    public string? Stereotype { get; set; }

    public override string ToPlantUml()
    {
        var stereotype = string.IsNullOrWhiteSpace(Stereotype) ? string.Empty : $" <<{Stereotype}>>";
        return $"actor \"{Title}\"{stereotype} as {Identifier}";
    }

    public override string ToString()
    {
        return Identifier;
    }
}