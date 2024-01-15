using System.Xml;

namespace DomainDrivenDesign.DiagramGenerators.Diagrams.UseCases;

public class UseCase : DiagramObject
{
    private UseCase(Type implementingType) 
        : base(implementingType)
    {
    }

    public static UseCase Create(Type implementingType, IDocumentationProvider documentationProvider)
    {
        var useCase = new UseCase(implementingType)
            { Documentation = documentationProvider.GetDocumentation(implementingType) };

        return useCase;
    }
    
    public static UseCase Create(Type implementingType)
    {
        return Create(implementingType, new NoDocumentationProvider());
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