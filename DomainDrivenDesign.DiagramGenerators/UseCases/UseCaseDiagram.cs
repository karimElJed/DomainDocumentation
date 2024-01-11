using System.Reflection;
using System.Text;
using DomainDrivenDesign.Core.Attributes;

namespace DomainDrivenDesign.DiagramGenerators.UseCases;

public class UseCaseDiagram
{
    private readonly List<UseCase> _useCases = new();
    private readonly List<Actor> _actors = new();
    private readonly List<Relation> _relations = new();
    
    public IReadOnlyList<UseCase> UseCases => _useCases.AsReadOnly();
    public IReadOnlyList<Actor> Actors => _actors.AsReadOnly();
    public IReadOnlyList<Relation> Relations => _relations.AsReadOnly();

    public void AddUseCase(Type useCaseType)
    {
        var useCaseAttribute = useCaseType.GetCustomAttribute<UseCaseAttribute>()!;

        if (useCaseAttribute == null)
        {
            throw new NotSupportedException("Type must have a UseCaseAttribute.");
        }
        
        var useCase = new UseCase(useCaseType.Name);
        
        _useCases.Add(useCase);

        var triggeredByAttributes = useCaseType.GetCustomAttributes<TriggeredByAttribute>();

        foreach (var triggeredByAttribute in triggeredByAttributes)
        {
            var actorType = triggeredByAttribute.ActorType;
            var actor = new Actor(actorType.Name);
            Add(actor);

            _relations.Add(new Relation(actor, useCase));
        }
    }

    private void Add(Actor actor)
    {
        if (_actors.All(a => a.Identifier != actor.Identifier))
        {
            _actors.Add(actor);
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
}