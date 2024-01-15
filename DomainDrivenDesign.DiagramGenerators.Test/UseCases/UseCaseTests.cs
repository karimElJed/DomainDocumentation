using DomainDrivenDesign.DiagramGenerators.Diagrams.UseCases;
using DomainDrivenDesign.SampleDomain;
using FluentAssertions;
using NUnit.Framework;

namespace DomainDrivenDesign.DiagramGenerators.Test.UseCases;

[TestFixture]
public class UseCaseTests
{
    [Test]
    public void ToPlantUml_ReturnsValidUml()
    {
        var sut = UseCase.Create(typeof(UseCaseWithOneActor));

        var uml = sut.ToPlantUml();

        uml.Should().Be("\"Use Case With One Actor\" as (UseCaseWithOneActor)");
    }
}