namespace DomainDocumentation.SampleDomain.ImportantPart;

public class FancyAggregate
{
    public int FancyId { get; set; }

    public FancyEntity Child { get; set; }
}