using System.Reflection;
using DomainDocumentation.Attributes;

namespace DomainDocumentation.Diagrams.UseCaseDiagrams;

public class UseCaseDiagramGenerator
{
    private readonly Assembly _assembly;
    private readonly IDocumentationProvider _documentationProvider;

    public UseCaseDiagramGenerator(Assembly assembly, IDocumentationProvider documentationProvider)
    {
        _assembly = assembly;
        _documentationProvider = documentationProvider;
    }

    public UseCaseDiagram CreateDiagramForAllUseCases()
    {
        var diagram = new UseCaseDiagram(_documentationProvider);

        var useCaseTypes = _assembly.GetTypes().Where(t => t.IsDefined(typeof(UseCaseAttribute)) && !t.IsAbstract);

        foreach (var useCaseType in useCaseTypes)
        {   
            diagram.AddUseCase(useCaseType);
        }

        return diagram;
    }
}