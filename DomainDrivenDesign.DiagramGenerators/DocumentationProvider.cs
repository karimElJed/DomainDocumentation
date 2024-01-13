using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;

namespace DomainDrivenDesign.DiagramGenerators;

// Contains code copied from
// https://learn.microsoft.com/en-us/archive/msdn-magazine/2019/october/csharp-accessing-xml-documentation-via-reflection

public class DocumentationProvider
{
    private readonly Dictionary<string, string> _xmlDocumentation = new();
    
    public DocumentationProvider(string xmlDocumentationPath)
    {
        if (!Path.Exists(xmlDocumentationPath))
        {
            throw new InvalidOperationException(
                "Put the xml documentation file in same folder and with same name as assembly.");
        }

        var xmlDocumentation  = File.ReadAllText(xmlDocumentationPath);
        
        LoadXmlDocumentation(xmlDocumentation);
    }

    public static DocumentationProvider ForAssembly(Assembly assembly)
    {
        var xmlDocumentationPath = Path.ChangeExtension(assembly.Location, "xml");

        return new DocumentationProvider(xmlDocumentationPath);
    }
    
    public string? GetDocumentation(Type type)
    {
        string key = "T:" + XmlDocumentationKeyHelper(type.FullName, null);
        
        _xmlDocumentation.TryGetValue(key, out string? documentation);
        
        return documentation;
    }
    
    public string? GetDocumentation(PropertyInfo propertyInfo)
    {
        string key = "P:" + XmlDocumentationKeyHelper(propertyInfo.DeclaringType.FullName, 
            propertyInfo.Name);
        
        _xmlDocumentation.TryGetValue(key, out string? documentation);
        
        return documentation;
    }
    
    private void LoadXmlDocumentation(string xmlDocumentation)
    {
        using XmlReader xmlReader = XmlReader.Create(new StringReader(xmlDocumentation));

        while (xmlReader.Read())
        {
            if (xmlReader is { NodeType: XmlNodeType.Element, Name: "member" })
            {
                string name = xmlReader["name"]!;
                _xmlDocumentation[name] = xmlReader.ReadInnerXml();
            }
        }
    }
    
    // Helper method to format the key strings
    private static string XmlDocumentationKeyHelper(string typeFullNameString, string memberNameString)
    {
        string key = Regex.Replace(typeFullNameString, @"\[.*\]", string.Empty)
                         .Replace('+', '.')
                     + "." + memberNameString;
        
        return key;
    }
}