using System.Collections;
using System.Text;

namespace DomainDocumentation.Diagrams.ClassDiagrams;

public class Class : DiagramObject
{
    private readonly List<Property> _properties = new();
    private readonly Type _type;

    public Class(Type classType) : base(classType)
    {
        _type = classType;
        Name = _type.Name;
        
        if (classType.IsArray)
        {
            _type = IsArray ? classType.GetElementType()! : classType;
            IsArray = true;
        }
        else if (classType.IsGenericType)
        {
            if (typeof(IEnumerable).IsAssignableFrom(classType))
            {
                _type = classType.GenericTypeArguments[0];
                IsArray = true;
            }
        }

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
        foreach (var property in properties)
        {
            bool isOneToManyRelation = false;

            var typeOfReference = property.PropertyType;
            if (property.PropertyType.IsGenericType)
            {
                typeOfReference = property.PropertyType.GenericTypeArguments[0];

                if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
                {
                    isOneToManyRelation = true;
                }
            }

            if (typeOfReference.Assembly.FullName!.StartsWith("System."))
            {
                // No need to add system types to diagram
                _properties.Add(new Property(property));
                continue;
            }

            var relatedClass = new Class(typeOfReference);
            var relation = new ClassRelation(
                this,
                relatedClass,
                property.Name,
                relatedClass.IsArray || isOneToManyRelation ? RelationType.OneToMany : RelationType.OneToOne);

            _properties.Add(new Property(property, relation));
        }
    }
}