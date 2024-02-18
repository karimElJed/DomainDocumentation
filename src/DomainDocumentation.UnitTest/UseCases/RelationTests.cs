using DomainDocumentation.Diagrams.UseCaseDiagrams;
using DomainDocumentation.SampleDomain.ImportantPart.UseCases;
using FluentAssertions;
using NUnit.Framework;

namespace DomainDocumentation.UnitTest.UseCases;

[TestFixture]
public class RelationTests
{
    [Test]
    public void ToPlantUml_ReturnsValidUml()
    {
        var sut = new UseCaseRelation(Actor.Create(typeof(Actors.PremiumUser)), UseCase.Create(typeof(UseCaseWithOneActor)));

        var uml = sut.ToPlantUml();

        uml.Should().Be("PremiumUser --> (UseCaseWithOneActor)");
    }
}