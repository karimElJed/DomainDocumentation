using DomainDrivenDesign.DiagramGenerators.UseCases;
using FluentAssertions;
using NUnit.Framework;

namespace DomainDrivenDesign.DiagramGenerators.Test;

[TestFixture]
public class ActorTests
{
    [Test]
    public void ToPlantUml_ReturnsValidUml()
    {
        var sut = new Actor("PremiumUser");

        var uml = sut.ToPlantUml();

        uml.Should().Be("actor \"Premium User\" as PremiumUser");
    }
}