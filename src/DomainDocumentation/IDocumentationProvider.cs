using System.Xml;

namespace DomainDocumentation;

public interface IDocumentationProvider
{
    XmlNode? GetDocumentation(Type type);
}