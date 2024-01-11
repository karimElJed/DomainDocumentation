using DomainDrivenDesign.DiagramGenerators.UseCases;
using FluentAssertions;
using NUnit.Framework;

namespace DomainDrivenDesign.DiagramGenerators.Test;

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