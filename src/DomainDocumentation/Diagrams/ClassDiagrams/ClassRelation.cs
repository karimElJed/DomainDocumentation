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
        switch (_relationType)
        {
            case RelationType.OneToOne:
                sb.AppendLine($"{From.Name} ---> {To.Name} : {RelationName}");
                break;
            case RelationType.OneToMany:
                var relationFrom =
                sb.AppendLine($"{From.Name} \"1\" ---> \"*\" {To.Name} : {RelationName}");
                break;
            case RelationType.Extends:
                sb.AppendLine($"{To.Name} <|-- {From.Name}");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

public enum RelationType
{
    OneToOne,
    OneToMany,
    Extends
}