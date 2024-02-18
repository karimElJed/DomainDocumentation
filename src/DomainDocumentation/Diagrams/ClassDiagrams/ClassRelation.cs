using System.Text;

namespace DomainDocumentation.Diagrams.ClassDiagrams;

public class ClassRelation
{
    private readonly RelationType _relationType;

    public ClassRelation(Class fromClass, Class toClass, string relationName, RelationType relationType)
    {
        _relationType = relationType;
        RelationName = relationName;
        From = fromClass;
        To = toClass;
    }
    
    public string RelationName { get; }

    public Class To { get; set; }

    public Class From { get; set; }

    public void ToPlantUml(StringBuilder sb)
    {
        var relationRight = _relationType == RelationType.OneToMany ? "\"many\" " : "";
        sb.AppendLine($"{From.Name} --> {relationRight}{To.Name} : {RelationName}");
        
    }
}

public enum RelationType
{
    OneToOne,
    OneToMany,
    Extends
}