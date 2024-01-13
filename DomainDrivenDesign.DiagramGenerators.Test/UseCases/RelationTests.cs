using DomainDrivenDesign.DiagramGenerators.Diagrams.UseCases;
using FluentAssertions;
using NUnit.Framework;

namespace DomainDrivenDesign.DiagramGenerators.Test.UseCases;

[TestFixture]
public class RelationTests
{
    [Test]
    public void ToPlantUml_ReturnsValidUml()
    {
        var sut = new Relation(new Actor("PremiumUser"), new UseCase("DeleteShoppingCart"));

        var uml = sut.ToPlantUml();

        uml.Should().Be("PremiumUser --> (DeleteShoppingCart)");
    }
}