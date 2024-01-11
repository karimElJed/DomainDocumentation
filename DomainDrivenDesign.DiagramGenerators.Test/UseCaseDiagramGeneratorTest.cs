using DomainDrivenDesign.DiagramGenerators.UseCases;
using DomainDrivenDesign.SampleDomain;
using FluentAssertions;
using NUnit.Framework;

namespace DomainDrivenDesign.DiagramGenerators.Test;

[TestFixture]
public class UseCaseDiagramGeneratorTest
{
    [Test]
    public void GenerateDiagram_GivenTypeHasNoUseCaseAttribute_ThrowsNotSupportedException()
    {
        var sut = new UseCaseDiagramGenerator(GetType());
        
        Assert.Throws<NotSupportedException>(() =>
        {
            sut.CreateDiagram();
        });
    }
    
    [Test]
    public void CreateDiagram_UseCaseWithoutActor_GeneratesCorrectDiagram()
    {
        var sut = new UseCaseDiagramGenerator(typeof(UseCaseWithoutActor));

        var result = sut.CreateDiagram();

        result.UseCases.Count.Should().Be(1);
        result.UseCases[0].Identifier.Should().Be("UseCaseWithoutActor");
        result.UseCases[0].Title.Should().Be("Use Case Without Actor");

        result.Actors.Count.Should().Be(0);
    }

    [Test]
    public void CreateDiagram_UseCaseWithOneActor_GeneratesCorrectDiagram()
    {
        var sut = new UseCaseDiagramGenerator(typeof(UseCaseWithOneActor));

        var result = sut.CreateDiagram();

        result.UseCases.Count.Should().Be(1);
        result.Actors.Count.Should().Be(1);
        result.Relations.Count.Should().Be(1);
    }
    
    [Test]
    public void CreateDiagram_UseCaseWithMultipleActors_GeneratesCorrectDiagram()
    {
        var sut = new UseCaseDiagramGenerator(typeof(UseCaseWithMultipleActors));

        var result = sut.CreateDiagram();

        result.UseCases.Count.Should().Be(1);
        result.Actors.Count.Should().Be(2);
        result.Relations.Count.Should().Be(2);

        result.Relations.Any(relation =>
            relation.From.Identifier == "Admin" && relation.To.Identifier == "UseCaseWithMultipleActors")
            .Should().BeTrue();
        
        result.Relations.Any(relation =>
                relation.From.Identifier == "PremiumUser" && relation.To.Identifier == "UseCaseWithMultipleActors")
            .Should().BeTrue();
    }     
}