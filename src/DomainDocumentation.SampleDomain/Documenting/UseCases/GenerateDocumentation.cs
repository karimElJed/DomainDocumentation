using DomainDocumentation.Attributes;

namespace DomainDocumentation.SampleDomain.Documenting.UseCases;

/// <summary>
/// The documentation can be generated during the build process or manually by a developer.
/// </summary>
[UseCase]
[TriggeredBy(typeof(Actors.DocumentationGenerator), Reason = @"To make domain knowledge accessible for team members and stakeholders.")]
public class GenerateDocumentation
{
}