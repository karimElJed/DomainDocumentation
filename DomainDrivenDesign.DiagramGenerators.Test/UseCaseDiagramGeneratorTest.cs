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

    // Todo: UseCase with one actor => Relations!
     
}