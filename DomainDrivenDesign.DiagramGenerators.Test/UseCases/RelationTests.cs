using DomainDrivenDesign.DiagramGenerators.Diagrams.UseCases;
using DomainDrivenDesign.SampleDomain;
using DomainDrivenDesign.SampleDomain.ImportantPart.UseCases;
using FluentAssertions;
using NUnit.Framework;

namespace DomainDrivenDesign.DiagramGenerators.Test.UseCases;

[TestFixture]
public class RelationTests
{
    [Test]
    public void ToPlantUml_ReturnsValidUml()
    {
        var sut = new Relation(Actor.Create(typeof(Actors.PremiumUser)), UseCase.Create(typeof(UseCaseWithOneActor)));

        var uml = sut.ToPlantUml();

        uml.Should().Be("PremiumUser --> (UseCaseWithOneActor)");
    }
}