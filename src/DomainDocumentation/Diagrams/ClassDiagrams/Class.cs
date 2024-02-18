using System.Text;

namespace DomainDocumentation.Diagrams.ClassDiagrams;

public class Class : DiagramObject
{
    private readonly List<Property> _properties = new();
    private readonly Type _type;

    public Class(Type classType) : base(classType)
    {
        IsArray = classType.IsArray;
        _type = IsArray ? classType.GetElementType()! : classType;
        
        Name = _type.Name;

        InitializeProperties();
    }

    public bool IsArray { get; }

    public string Name { get; }
    public IEnumerable<Property> Properties => _properties.AsReadOnly();


    public override string ToPlantUml()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"class {Name} {{");
        
        foreach (var property in _properties)
        {
            property.ToPlantUml(sb);
        }

        sb.AppendLine("}");

        return sb.ToString();
    }
    
    
    private void InitializeProperties()
    {
        var properties = _type.GetProperties();
        foreach (var propertyInfo in properties)
        {
            _properties.Add(new Property(propertyInfo));
        }
    }
}