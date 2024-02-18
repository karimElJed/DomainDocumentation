using System.Reflection;
using System.Text;
using DomainDocumentation.Attributes;

namespace DomainDocumentation.Diagrams.UseCaseDiagrams;

public class UseCaseDiagram : DiagramBase
{
    private readonly IDocumentationProvider _documentationProvider;
    private readonly List<UseCase> _useCases = new();
    private readonly List<Actor> _actors = new();
    private readonly List<UseCaseRelation> _relations = new();
    
    public IReadOnlyList<UseCase> UseCases => _useCases.AsReadOnly();
    public IReadOnlyList<Actor> Actors => _actors.AsReadOnly();
    public IReadOnlyList<UseCaseRelation> Relations => _relations.AsReadOnly();

    public UseCaseDiagram(IDocumentationProvider documentationProvider)
    {
        _documentationProvider = documentationProvider;
    }
    
    public void AddUseCase(Type useCaseType)
    {
        var useCaseAttribute = useCaseType.GetCustomAttribute<UseCaseAttribute>()!;

        if (useCaseAttribute == null)
        {
            throw new NotSupportedException("Type must have a UseCaseAttribute.");
        }
        
        var useCase = UseCase.Create(useCaseType, _documentationProvider);
        
        _useCases.Add(useCase);

        var triggeredByAttributes = useCaseType.GetCustomAttributes<TriggeredByAttribute>();

        foreach (var triggeredByAttribute in triggeredByAttributes)
        {
            var actorType = triggeredByAttribute.ActorType;

            if (actorType.IsActor(out var actorAttribute))
            {
                var actor = Actor.Create(actorType, actorAttribute?.Stereotype, _documentationProvider);
                Add(actor);

                var relation = new UseCaseRelation(actor, useCase) { Motivation = triggeredByAttribute.Reason };

                _relations.Add(relation);
            }
            else if (actorType.IsUseCase())
            {
                var actorUseCase = UseCase.Create(actorType, _documentationProvider);
                Add(actorUseCase);
                
                var relation = new UseCaseRelation(actorUseCase, useCase) { Motivation = triggeredByAttribute.Reason };

                _relations.Add(relation);
            }
        }
    }

    public override string ToPlantUml()
    {
        var sb = new StringBuilder();

        sb.AppendLine("@startuml");
        sb.AppendLine();

        RenderAsPlantUml(sb, 0, _actors);
        RenderAsPlantUml(sb, 0, _useCases);
        RenderAsPlantUml(sb, 0, _relations);

        sb.Append("@enduml");
        
        return sb.ToString();
    }
    
    private void Add(Actor actor)
    {
        if (_actors.All(a => a.Identifier != actor.Identifier))
        {
            _actors.Add(actor);
        }
    }
    
    private void Add(UseCase useCase)
    {
        if (_useCases.All(uc => uc.Identifier != useCase.Identifier))
        {
            _useCases.Add(useCase);
        }
    }
}