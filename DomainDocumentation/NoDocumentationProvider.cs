using System.Xml;

namespace DomainDrivenDesign.DiagramGenerators;

public class NoDocumentationProvider : IDocumentationProvider
{
    public XmlNode? GetDocumentation(Type type)
    {
        return null;
    }
}