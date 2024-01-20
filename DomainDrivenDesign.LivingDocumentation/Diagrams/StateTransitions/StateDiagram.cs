using System.Reflection;
using System.Text;
using DomainDrivenDesign.Core.Attributes;

namespace DomainDrivenDesign.DiagramGenerators.Diagrams.StateTransitions;

public class StateDiagram : DiagramBase
{
    private List<State> _states;
    private List<StateTransition> _transitions;
    private readonly DocumentationProvider _documentationProvider;

    public StateDiagram(DocumentationProvider documentationProvider)
    {
        _documentationProvider = documentationProvider;
        _states = new List<State>();
        _transitions = new List<StateTransition>();
    }
    
    public void AddStates(Type enumType)
    {
        var stateAttribute = enumType.GetCustomAttribute<StateAttribute>();
        
        if (stateAttribute == null || !enumType.IsEnum)
        {
            throw new NotSupportedException("Type must have a StateAttribute.");
        }

        var enumValues = enumType.GetFields().Where(field => field.FieldType == enumType).ToList();

        foreach (var enumValue in enumValues)
        {
            var state = new State(enumValue.Name)
            {
                Documentation = _documentationProvider.GetDocumentation(enumValue)
            };
           
            _states.Add(state);
        }

        foreach (var state in _states)
        {
            var enumValue = enumValues.First(field => field.Name == state.Identifier);
            var customAttributes = enumValue.GetCustomAttributes();

            foreach (var customAttribute in customAttributes)
            {
                var attributeType = customAttribute.GetType();
                var genericType = customAttribute.GetType().GetGenericTypeDefinition();
              
                if (genericType == typeof(TransformsIntoAttribute<>))
                {
                    var nextStateValue = attributeType.GetProperty("NextState")!.GetValue(customAttribute);
                    var trigger = attributeType.GetProperty("Trigger")!.GetValue(customAttribute);

                    var nextState = _states.First(field => field.Identifier == nextStateValue!.ToString());
                    var transition = new StateTransition(state, nextState, trigger?.ToString());
                    _transitions.Add(transition);
                }
            }
        }
        
        // add start and end state
        var startEndState = new State("[*]");
        var firstState = _states.FirstOrDefault(s => _transitions.All(t => t.To != s));
        var lastState = _states.FirstOrDefault(s => _transitions.All(t => t.From != s));
        
        if (firstState != null)
        {
            var transition = new StateTransition(startEndState, firstState, null);
            _transitions.Add(transition);
        }
        
        if (lastState != null)
        {
            var transition = new StateTransition(lastState, startEndState, null);
            _transitions.Add(transition);
        }
    }

    public override string ToPlantUml()
    {
        var sb = new StringBuilder();

        sb.AppendLine("@startuml");
        sb.AppendLine();
        //sb.AppendLine("left to right direction"); // todo make configurable
        sb.AppendLine("top to bottom direction"); 
        sb.AppendLine("hide empty description");

        RenderAsPlantUml(sb, 0, _states);
        RenderAsPlantUml(sb, 0, _transitions);

        sb.Append("@enduml");
        
        return sb.ToString();
    }
}