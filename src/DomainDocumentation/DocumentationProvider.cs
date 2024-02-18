using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;

namespace DomainDocumentation;

public class DocumentationProvider : IDocumentationProvider
{
    private readonly XmlDocument _xmlDocumentation;

    public DocumentationProvider(string xmlDocumentationPath)
    {
        if (!Path.Exists(xmlDocumentationPath))
        {
            throw new InvalidOperationException(
                "Put the xml documentation file in same folder and with same name as assembly.");
        }

        _xmlDocumentation = new XmlDocument();
        _xmlDocumentation.Load(xmlDocumentationPath);
    }

    public static DocumentationProvider FromAssembly(Assembly assembly)
    {
        var xmlDocumentationPath = Path.ChangeExtension(assembly.Location, "xml");

        return new DocumentationProvider(xmlDocumentationPath);
    }
    
    /// <inheritdoc />
    public XmlNode? GetDocumentation(Type type)
    {
        var key = "T:" + CreateKey(type.FullName!, null);
        var xmlNode = _xmlDocumentation.SelectSingleNode($"/doc/members/member[@name=\"{key}\"]");

        return xmlNode;
    }
    
    public XmlNode? GetDocumentation(FieldInfo fieldInfo)
    {
        var fullName = fieldInfo.FieldType.FullName + "." + fieldInfo.Name;
        var key = "F:" + CreateKey(fullName, null);
        var xmlNode = _xmlDocumentation.SelectSingleNode($"/doc/members/member[@name=\"{key}\"]");

        return xmlNode;
    }
    
    // Helper method to format the key strings
    private static string CreateKey(string typeFullNameString, string? memberNameString)
    {
        string key = Regex.Replace(typeFullNameString, @"\[.*\]", string.Empty)
            .Replace('+', '.');

        if (memberNameString != null)
        {
            key += "." + memberNameString;
        }
        
        return key;
    }
}