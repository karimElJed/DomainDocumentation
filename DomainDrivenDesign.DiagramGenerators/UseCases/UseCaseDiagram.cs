using System.Reflection;
using DomainDrivenDesign.Core.Attributes;

namespace DomainDrivenDesign.DiagramGenerators.UseCases;

public class UseCaseDiagram
{
    private List<UseCase> _useCases = new();
    private List<Actor> _actors = new();

    public void AddUseCase(Type useCaseType)
    {
        var useCaseAttribute = useCaseType.GetCustomAttribute(typeof(UseCaseAttribute))!;

        if (useCaseAttribute == null)
        {
            throw new NotSupportedException("Type must have a UseCaseAttribute.");
        }
        
        string identifier = useCaseType.Name;
        var useCase = new UseCase(identifier);
        
        _useCases.Add(useCase);
    }

    public void AddActor(Type actorType)
    {
        
    }

    public IReadOnlyList<UseCase> UseCases => _useCases.AsReadOnly();
    public IReadOnlyList<Actor> Actors => _actors.AsReadOnly();
}