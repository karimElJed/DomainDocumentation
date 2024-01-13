using System.Reflection;
using DomainDrivenDesign.Core.Attributes;

namespace DomainDrivenDesign.DiagramGenerators.Diagrams.UseCases;

public class UseCaseDiagramGenerator
{
    private readonly Assembly _assembly;     

    public UseCaseDiagramGenerator(Assembly assembly, bool withDocumentation = false)
    {
        _assembly = assembly;
    }

    public UseCaseDiagram CreateDiagramForAllUseCases()
    {
        var diagram = new UseCaseDiagram();

        var useCaseTypes = _assembly.GetTypes().Where(t => t.IsDefined(typeof(UseCaseAttribute)) && !t.IsAbstract);

        foreach (var useCaseType in useCaseTypes)
        {   
            diagram.AddUseCase(useCaseType);
        }

        return diagram;
    }
}