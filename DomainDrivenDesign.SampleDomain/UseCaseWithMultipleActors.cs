using DomainDrivenDesign.Core.Attributes;

namespace DomainDrivenDesign.SampleDomain;

/// <summary>
/// This use case is relevant for two different kind of users.
/// Also this description is some kind of meaningless,
/// but in a real use case you would explain some more details about the business process.
/// </summary>
[UseCase]
[TriggeredBy(typeof(Actors.Admin))]
[TriggeredBy(typeof(Actors.PremiumUser))]
public class UseCaseWithMultipleActors
{
    
}