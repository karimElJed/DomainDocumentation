using DomainDrivenDesign.DiagramGenerators.Diagrams.UseCases;
using DomainDrivenDesign.SampleDomain;
using DomainDrivenDesign.SampleDomain.ImportantPart.UseCases;
using FluentAssertions;
using NUnit.Framework;

namespace DomainDrivenDesign.DiagramGenerators.Test.UseCases;

[TestFixture]
public class ActorTests
{
    [Test]
    public void ToPlantUml_WithoutStereotype_ReturnsCorrectUml()
    {
        var sut = new Actor(typeof(Actors.PremiumUser));

        var uml = sut.ToPlantUml();

        uml.Should().Be("actor \"Premium User\" as PremiumUser");
    }
    
    [Test]
    public void ToPlantUml_WithStereotype_ReturnsCorrectUml()
    {
        var sut = new Actor(typeof(Actors.PremiumUser)) { Stereotype = "Human"};

        var uml = sut.ToPlantUml();

        uml.Should().Be("actor \"Premium User\" <<Human>> as PremiumUser");
    }
}