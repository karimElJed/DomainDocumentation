using System.Text.RegularExpressions;
using System.Xml;
using DomainDrivenDesign.DiagramGenerators.Utils;

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
    
    public XmlNode? Documentation { get; init; }

    public bool HasDocumentation => Documentation != null;

    public abstract string ToPlantUml();
    
    private static string IdentifierToTitle(string identifier)
    {
        return TextUtils.ReplaceDotsWithWhitespaces(identifier);
    }
}