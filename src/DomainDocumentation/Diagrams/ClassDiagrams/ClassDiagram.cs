using System.Collections;
using System.Text;

namespace DomainDocumentation.Diagrams.ClassDiagrams;

public class ClassDiagram : DiagramBase
{
    private readonly List<Class> _classes = new();
    private readonly List<ClassRelation> _relations = new();
    private readonly Class _root;

    public ClassDiagram(Type rootClass)
    {
        _root = new Class(rootClass);

        Traverse(_root);
    }

    private void Traverse(Class current)
    {
        AddClass(current);
        foreach (var property in current.Properties)
        {
            if (property.HasRelation)
            {
                Traverse(property.Relation.To);
            }
        }
    }

    private void AddClass(Class classToAdd)
    {
        if (!_classes.Exists(c => c.ImplementingType == classToAdd.ImplementingType))
        {
            _classes.Add(classToAdd);
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

        foreach (var classDefinition in _classes)
        {
            foreach (var property in classDefinition.Properties)
            {
                if (property.HasRelation)
                {
                    property.Relation.ToPlantUml(sb);
                }
            }
        }
        
        return sb.ToString();
    }
}