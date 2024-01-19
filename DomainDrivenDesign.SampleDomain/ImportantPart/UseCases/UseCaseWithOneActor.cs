using DomainDrivenDesign.Core.Attributes;

namespace DomainDrivenDesign.SampleDomain.ImportantPart.UseCases;

/// <summary>
/// This is a special use case for the admin only.
/// </summary>
[UseCase]
[TriggeredBy(typeof(Actors.Admin))]
public class UseCaseWithOneActor
{
    
}