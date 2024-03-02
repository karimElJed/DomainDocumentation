using System.Collections;
using System.Text;

namespace DomainDocumentation.Diagrams.ClassDiagrams;

public class ClassDiagram : DiagramBase
{
    private readonly List<Class> _classes = new();
    private readonly Class _root;

    public ClassDiagram(Type rootClass)
    {
        _root = new Class(rootClass);

        Traverse(_root);
    }

    public IReadOnlyList<Class> Classes => _classes.AsReadOnly();
    public Class RootClass => _root;

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

            foreach (var relation in classToAdd.Relations)
            {
                AddClass(relation.To);
            }
        }
    }

    public override string ToPlantUml()
    {
        var sb = new StringBuilder();
        sb.AppendLine("@startuml");

        foreach (var @class in _classes)
        {
           sb.AppendLine(@class.ToPlantUml());
        }

        sb.AppendLine();

        foreach (var @class in _classes)
        {
            foreach (var relation in @class.Relations)
            {
                relation.ToPlantUml(sb);
            }
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