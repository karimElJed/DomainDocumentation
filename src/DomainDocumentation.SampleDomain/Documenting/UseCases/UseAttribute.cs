using DomainDocumentation.Attributes;

namespace DomainDocumentation.SampleDomain.Documenting.UseCases;

/// <summary>
/// This is a special use case for the admin only.
/// </summary>
[UseCase]
[TriggeredBy(typeof(Actors.Developer), Reason = "Extend source code with domain knowledge.")]
public class UseAttribute
{
}