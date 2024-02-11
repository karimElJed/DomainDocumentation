using System.Xml;

namespace DomainDrivenDesign.DiagramGenerators;

public interface IDocumentationProvider
{
    XmlNode? GetDocumentation(Type type);
}