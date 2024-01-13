using System.Text.RegularExpressions;

namespace DomainDrivenDesign.DiagramGenerators.Diagrams;

public abstract class DiagramObject
{
    protected DiagramObject(string identifier)
    {
        Identifier = identifier;
        Title = IdentifierToTitle(identifier);
    }
    
    public string Identifier { get; }

    public string Title { get; }

    public abstract string ToPlantUml();
    
    protected string IdentifierToTitle(string identifier)
    {
        return Regex.Replace(identifier, "(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])", " $1");
    }
}