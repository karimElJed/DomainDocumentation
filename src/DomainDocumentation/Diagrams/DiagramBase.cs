using System.Text;

namespace DomainDocumentation.Diagrams;

public abstract class DiagramBase
{
    public abstract string ToPlantUml();
    
    protected void RenderAsPlantUml(StringBuilder sb, int indent, IEnumerable<DiagramObject> objects)
    {
        var indentation = new string('\t', indent);
        
        foreach (var @object in objects)
        {
            sb.Append(indentation);
            sb.AppendLine( @object.ToPlantUml());
        }
        
        sb.AppendLine();
    }
}