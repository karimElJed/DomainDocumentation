using DomainDocumentation.SampleDomain.ImportantPart.UseCases;
using DomainDrivenDesign.DiagramGenerators.Diagrams.UseCases;
using FluentAssertions;
using NUnit.Framework;

namespace DomainDocumentation.UnitTest.UseCases;

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