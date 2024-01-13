using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using Microsoft.VisualBasic.CompilerServices;

namespace DomainDrivenDesign.DiagramGenerators;

public class DocumentationFormatter
{
    private readonly XslCompiledTransform _transform;
    
    public DocumentationFormatter(string xsltPath)
    {
        if (!Path.Exists(xsltPath))
        {
            throw new ArgumentException("Provided xslt path does not exist.");
        }
        
        _transform = new XslCompiledTransform();
        _transform.Load(xsltPath);
    }

    public static DocumentationFormatter ForDefaultMarkdown()
    {
        var assemblyPath = typeof(DocumentationFormatter).Assembly.Location;
        var folderPath = Path.GetDirectoryName(assemblyPath);
        var xsltPath = Path.Combine(folderPath, "markdown.xslt");

        return new DocumentationFormatter(xsltPath);
    }
    
    public string? Format(XmlNode documentationNode)
    {
        var memoryStream = new MemoryStream();
        _transform.Transform(new XPathDocument(new XmlNodeReader(documentationNode)), null, memoryStream);
        
        memoryStream.Position = 0;
        StreamReader streamReader = new StreamReader(memoryStream);
        string? output = streamReader.ReadToEnd();

        return output;
    }
}