using System.Text;

namespace DomainDocumentation.Diagrams.StateDiagrams;

public class State : DiagramObject
{
    public State(string identifier) : base(identifier)
    {
    }

    public override string ToPlantUml()
    {
        if (!HasDocumentation)
        {
            return string.Empty;
        }

        var text = Documentation!.InnerText;
        if (text.EndsWith(Environment.NewLine))
        {
            text = text.Substring(text.Length - Environment.NewLine.Length);
        }
        if (text.EndsWith(Environment.NewLine))
        {
            text = text.Substring(0, text.Length - Environment.NewLine.Length);
        }

        var lines = text.Trim().Split(Environment.NewLine);
        var sb = new StringBuilder();
        
        foreach (var line in lines)
        {
            var identifier = Identifier == Title ? Identifier : $"state \"{Title}\" as {Identifier}";
            sb.AppendLine($"{identifier} : {line.Trim()}");
        }

        return sb.ToString();
    }
}