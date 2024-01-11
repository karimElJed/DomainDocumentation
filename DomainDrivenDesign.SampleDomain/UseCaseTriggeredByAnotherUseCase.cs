using DomainDrivenDesign.Core.Attributes;

namespace DomainDrivenDesign.SampleDomain;

[UseCase]
[TriggeredBy(typeof(UseCaseWithOneActor))]
public class UseCaseTriggeredByAnotherUseCase
{
    
}