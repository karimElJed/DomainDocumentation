using DomainDrivenDesign.Core.Attributes;

namespace DomainDrivenDesign.SampleDomain;

[UseCase]
[TriggeredBy(typeof(Actors.Admin))]
public class CreateUser
{
    
}