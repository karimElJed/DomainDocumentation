using System.Text;

namespace DomainDocumentation.Diagrams.ClassDiagrams;

public class ClassDiagram : DiagramBase
{
    private readonly List<Class> _classes = new();
    private readonly List<ClassRelation> _relations = new();

    public void AddClass(Type classType)
    {
        var classObject = new Class(classType);
        _classes.Add(classObject);

        foreach (var property in classObject.Properties)
        {
            if (!property.PropertyType.Assembly.FullName!.StartsWith("System."))
            {
                var relatedClass = new Class(property.PropertyType);
                _classes.Add(relatedClass);

                var relation = new ClassRelation(
                    classObject,
                    relatedClass,
                    property.PropertyType.Name,
                    relatedClass.IsArray ? RelationType.OneToMany : RelationType.OneToOne);
                
                _relations.Add(relation);
            }
        }
    }

    public override string ToPlantUml()
    {
        var sb = new StringBuilder();
        sb.AppendLine("@startuml");

        foreach (var classDefinition in _classes)
        {
           sb.AppendLine(classDefinition.ToPlantUml());
        }

        sb.AppendLine();

        foreach (var relation in _relations)
        {
            relation.ToPlantUml(sb);
        }
        
        return sb.ToString();
    }
}