using DomainDocumentation.Diagrams.UseCaseDiagrams;
using DomainDocumentation.Attributes;
using FluentAssertions;
using NUnit.Framework;

namespace DomainDocumentation.UnitTest.UseCases;

[TestFixture]
public class RelationTests
{
    [Actor]
    private class PremiumUser {}
    
    [UseCase]
    private class UseCaseWithOneActor {}
    
    [Test]
    public void ToPlantUml_ReturnsValidUml()
    {
        var sut = new UseCaseRelation(
            Actor.Create(typeof(PremiumUser)), 
            UseCase.Create(typeof(UseCaseWithOneActor)));

        var uml = sut.ToPlantUml();

        uml.Should().Be("PremiumUser --> (UseCaseWithOneActor)");
    }
}