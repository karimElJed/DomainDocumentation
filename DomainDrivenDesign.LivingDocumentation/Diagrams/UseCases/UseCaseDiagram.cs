using System.Reflection;
using System.Text;
using DomainDrivenDesign.Core.Attributes;

namespace DomainDrivenDesign.DiagramGenerators.Diagrams.UseCases;

public class UseCaseDiagram
{
    private readonly IDocumentationProvider _documentationProvider;
    private readonly List<UseCase> _useCases = new();
    private readonly List<Actor> _actors = new();
    private readonly List<Relation> _relations = new();
    
    public IReadOnlyList<UseCase> UseCases => _useCases.AsReadOnly();
    public IReadOnlyList<Actor> Actors => _actors.AsReadOnly();
    public IReadOnlyList<Relation> Relations => _relations.AsReadOnly();

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

                var relation = new Relation(actor, useCase) { Motivation = triggeredByAttribute.Reason };

                _relations.Add(relation);
            }
            else if (actorType.IsUseCase())
            {
                var actorUseCase = UseCase.Create(actorType, _documentationProvider);
                Add(actorUseCase);
                
                var relation = new Relation(actorUseCase, useCase) { Motivation = triggeredByAttribute.Reason };

                _relations.Add(relation);
            }
        }
    }

    public string ToPlantUml()
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

    private void RenderAsPlantUml(StringBuilder sb, int indent, IEnumerable<DiagramObject> objects)
    {
        var indentation = new string('\t', indent);
        
        foreach (var @object in objects)
        {
            sb.Append(indentation);
            sb.AppendLine( @object.ToPlantUml());
        }
        
        sb.AppendLine();
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