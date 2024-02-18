using System.Xml;

namespace DomainDocumentation;

public class NoDocumentationProvider : IDocumentationProvider
{
    public XmlNode? GetDocumentation(Type type)
    {
        return null;
    }
}