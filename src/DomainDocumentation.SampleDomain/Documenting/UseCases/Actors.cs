using DomainDocumentation.Attributes;

namespace DomainDocumentation.SampleDomain.Documenting.UseCases;

public static class Actors
{
    /// <summary>
    /// A domain expert has deep knowledge about one or multiple
    /// business topics of the domain.
    /// </summary>
    [Actor]
    public static class DomainExpert
    {
    }
    
    /// <summary>
    /// A developer implements the software that is documented.
    /// </summary>
    [Actor]
    public static class Developer {}
    
    /// <summary>
    /// The Documentation Generator is generating the documentation
    /// according to some predefined rules.
    /// </summary>
    [Actor(Stereotype = "Application")]
    public static class DocumentationGenerator {}
}