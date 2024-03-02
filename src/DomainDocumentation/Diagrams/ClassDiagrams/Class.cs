using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace DomainDocumentation.Diagrams.ClassDiagrams;

[DebuggerDisplay("{Name}")]
public class Class : DiagramObject
{
    private readonly List<Property> _properties = new();
    private readonly List<ClassRelation> _relations = new();
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

        if (!_type.BaseType!.Namespace!.StartsWith("System"))
        {
            var extendedClass = new Class(_type.BaseType);
            var relation = new ClassRelation(this, extendedClass, "extends", RelationType.Extends);
            _relations.Add(relation);
        }
        
        InitializeProperties();
    }

    public bool IsArray { get; }

    public string Name { get; }
    public IEnumerable<Property> Properties => _properties.AsReadOnly();
    public IEnumerable<ClassRelation> Relations => _relations.AsReadOnly();


    public override string ToPlantUml()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"class {Name} {{");
        
        foreach (var property in _properties.OrderBy(p => p.PropertyName))
        {
            property.ToPlantUml(sb);
        }

        sb.AppendLine("}");

        return sb.ToString();
    }
    
    
    private void InitializeProperties()
    {
        var properties = _type.GetProperties(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance);
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