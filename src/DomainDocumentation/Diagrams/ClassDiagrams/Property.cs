using System.Reflection;
using System.Text;

namespace DomainDocumentation.Diagrams.ClassDiagrams;

public class Property
{
    private readonly PropertyInfo _info;
    private readonly ClassRelation? _classRelation;

    public Property(PropertyInfo propertyInfo, ClassRelation classRelation)
    {
        _info = propertyInfo;
        _classRelation = classRelation;

        PropertyName = _info.Name;
        PropertyType = _info.PropertyType;
    }

    public Property(PropertyInfo propertyInfo) 
    {
        _info = propertyInfo;
        _classRelation = null;

        PropertyName = _info.Name;
        PropertyType = _info.PropertyType;
    }

    public Type PropertyType { get; }

    public string PropertyName { get; }
    public bool HasRelation => _classRelation != null;
    public ClassRelation Relation => _classRelation!;

    public void ToPlantUml(StringBuilder sb)
    {
        // todo: do we need the property type? Pro vs. con?
        if (_classRelation == null)
        {
            sb.AppendLine($"\t{PropertyName} : {PropertyType.Name}");
        }
    }
}