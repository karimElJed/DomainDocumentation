using System.Reflection;
using System.Text;

namespace DomainDocumentation.Diagrams.ClassDiagrams;

public class Property
{
    private readonly PropertyInfo _info;

    public Property(PropertyInfo propertyInfo)
    {
        _info = propertyInfo;
        
        PropertyName = _info.Name;
        PropertyType = _info.PropertyType;
    }

    public Type PropertyType { get; }

    public string PropertyName { get; }

    public void ToPlantUml(StringBuilder sb)
    {
        // todo: do we need the property type? Pro vs. con?
        sb.AppendLine($"\t{PropertyName} : {PropertyType.Name}");
    }
}