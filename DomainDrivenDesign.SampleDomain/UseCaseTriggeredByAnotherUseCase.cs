using DomainDrivenDesign.Core.Attributes;

namespace DomainDrivenDesign.SampleDomain;

[UseCase]
[TriggeredBy(typeof(UseCaseWithOneActor), Reason = "All new customers will be subscribed to the newsletter automatically.")]
[TriggeredBy(typeof(Actors.CMS), Reason = "A new newsletter was created by the marketing team.")]
public class UseCaseTriggeredByAnotherUseCase
{
    
}