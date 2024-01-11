using DomainDrivenDesign.Core.Attributes;

namespace DomainDrivenDesign.SampleDomain;

[UseCase]
[TriggeredBy(typeof(Actors.Admin))]
public class UseCaseWithOneActor
{
    
}

[UseCase]
[TriggeredBy(typeof(Actors.Admin))]
[TriggeredBy(typeof(Actors.PremiumUser))]
public class UseCaseWithMultipleActors
{
    
}