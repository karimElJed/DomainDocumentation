using DomainDrivenDesign.DiagramGenerators.UseCases;
using FluentAssertions;
using NUnit.Framework;

namespace DomainDrivenDesign.DiagramGenerators.Test.UseCases;

[TestFixture]
public class ActorTests
{
    [Test]
    public void ToPlantUml_WithoutStereotype_ReturnsCorrectUml()
    {
        var sut = new Actor("PremiumUser");

        var uml = sut.ToPlantUml();

        uml.Should().Be("actor \"Premium User\" as PremiumUser");
    }
    
    [Test]
    public void ToPlantUml_WithStereotype_ReturnsCorrectUml()
    {
        var sut = new Actor("ServiceAPI") { Stereotype = "System"};

        var uml = sut.ToPlantUml();

        uml.Should().Be("actor \"Service API\"<<System>> as ServiceAPI");
    }
}