using System.Reflection;
using System.Text.RegularExpressions;
using DomainDrivenDesign.Core.Attributes;

namespace DomainDrivenDesign.DiagramGenerators.UseCases;

public class UseCaseDiagramGenerator
{
    private readonly Assembly _assembly;

    public UseCaseDiagramGenerator(Assembly assembly)
    {
        _assembly = assembly;
    }

    public UseCaseDiagram CreateDiagram()
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