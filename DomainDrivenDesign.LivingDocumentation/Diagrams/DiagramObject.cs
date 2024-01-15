using System.Text.RegularExpressions;
using System.Xml;

namespace DomainDrivenDesign.DiagramGenerators.Diagrams;

public abstract class DiagramObject
{
    protected DiagramObject(Type implementingType) 
        : this(implementingType.Name)
    {
        ImplementingType = implementingType;
    }

    protected DiagramObject(string identifier)
    {
        Identifier = identifier;
        Title = IdentifierToTitle(identifier);
    }
    
    public string Identifier { get; }

    public string Title { get; }
    
    public Type ImplementingType { get; }
    
    public XmlNode? Documentation { get; protected set; }

    public bool HasDocumentation => Documentation != null;

    public abstract string ToPlantUml();
    
    protected string IdentifierToTitle(string identifier)
    {
        return Regex.Replace(identifier, "(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])", " $1");
    }
}