using System.Text.RegularExpressions;

namespace DomainDrivenDesign.DiagramGenerators;

public abstract class DiagramObject
{
    protected DiagramObject(string identifier)
    {
        Identifier = identifier;
        Title = IdentifierToTitle(identifier);
    }
    
    public string Identifier { get; }

    public string Title { get; }
    
    protected string IdentifierToTitle(string identifier)
    {
        return Regex.Replace(identifier, "(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])", " $1");
    }
}