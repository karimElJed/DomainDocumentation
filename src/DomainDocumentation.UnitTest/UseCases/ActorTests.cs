using DomainDocumentation.Attributes;
using DomainDocumentation.Diagrams.UseCaseDiagrams;
using FluentAssertions;
using NUnit.Framework;

namespace DomainDocumentation.UnitTest.UseCases;

[TestFixture]
public class ActorTests
{
    [Actor]
    private class PremiumUser {}
    
    [Test]
    public void ToPlantUml_WithoutStereotype_ReturnsCorrectUml()
    {
        var sut = new Actor(typeof(PremiumUser));

        var uml = sut.ToPlantUml();

        uml.Should().Be("actor \"Premium User\" as PremiumUser");
    }
    
    [Test]
    public void ToPlantUml_WithStereotype_ReturnsCorrectUml()
    {
        var sut = new Actor(typeof(PremiumUser)) { Stereotype = "Human"};

        var uml = sut.ToPlantUml();

        uml.Should().Be("actor \"Premium User\" <<Human>> as PremiumUser");
    }
}