using System.Reflection;
using System.Text.RegularExpressions;
using DomainDrivenDesign.Core.Attributes;

namespace DomainDrivenDesign.DiagramGenerators.UseCases;

public class UseCaseDiagramGenerator
{
    private readonly Type _useCaseType;

    public UseCaseDiagramGenerator(Type useCaseType)
    {
        _useCaseType = useCaseType;
    }

    public UseCaseDiagram CreateDiagram()
    {
        var diagram = new UseCaseDiagram();

        diagram.AddUseCase(_useCaseType);

        return diagram;
    }
}