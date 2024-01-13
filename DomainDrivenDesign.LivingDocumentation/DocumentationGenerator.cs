using System.Text;
using DomainDrivenDesign.DiagramGenerators.Diagrams.UseCases;

namespace DomainDrivenDesign.DiagramGenerators;

public class DocumentationGenerator
{
    private readonly DocumentationProvider _documentationProvider;
    private readonly DocumentationFormatter _documentationFormatter;

    public DocumentationGenerator(
        DocumentationProvider documentationProvider,
        DocumentationFormatter documentationFormatter)
    {
        _documentationProvider = documentationProvider;
        _documentationFormatter = documentationFormatter;
    }

    public string DocumentUseCase(Type useCaseType)
    {
        // todo: make dynamic
        var diagram = new UseCaseDiagram();
        diagram.AddUseCase(useCaseType);
        
        var sb = new StringBuilder();
        sb.AppendLine("# " + diagram.UseCases.First().Title);
        
        // todo: note that this document is generated and should not be changed!

        string? description = GetDescription(useCaseType);
        if (description != null)
        {
            sb.AppendLine(description);
        }

        string plantUmlDiagram = diagram.ToPlantUml();

        sb.AppendLine("## Use Case Diagram");
        sb.AppendLine(plantUmlDiagram);

        if (diagram.Actors.Count > 0)
        {
            sb.AppendLine("## Actors");
            foreach (var actor in diagram.Actors)
            {
                sb.AppendLine("### " + actor.Title);
                // todo: Store type or store documentation?
                //description = GetDescription(actor.Type);
            }
        }

        return sb.ToString();
    }

    private string? GetDescription(Type useCaseType)
    {
        string? description = null;
        
        var summaryNode = _documentationProvider.GetDocumentation(useCaseType);
        if (summaryNode != null)
        {
            description = _documentationFormatter.Format(summaryNode);
        }

        return description;
    }
}