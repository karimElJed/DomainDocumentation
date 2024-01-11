using DomainDrivenDesign.Core.Attributes;

namespace DomainDrivenDesign.SampleDomain;

[UseCase]
[TriggeredBy(typeof(UseCaseWithOneActor))]
[TriggeredBy(typeof(Actors.CMS))]
public class UseCaseTriggeredByAnotherUseCase
{
    
}