using DomainDrivenDesign.DiagramGenerators.UseCases;
using FluentAssertions;
using NUnit.Framework;

namespace DomainDrivenDesign.DiagramGenerators.Test;

[TestFixture]
public class UseCaseTests
{
    [Test]
    public void ToPlantUml_ReturnsValidUml()
    {
        var sut = new UseCase("AddItemToShoppingCart");

        var uml = sut.ToPlantUml();

        uml.Should().Be("\"Add Item To Shopping Cart\" as (AddItemToShoppingCart)");
    }
}