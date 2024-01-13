using DomainDrivenDesign.DiagramGenerators.Diagrams.UseCases;
using DomainDrivenDesign.SampleDomain;
using FluentAssertions;
using NUnit.Framework;

namespace DomainDrivenDesign.DiagramGenerators.Test.UseCases;

[TestFixture]
public class UseCaseDiagramTest
{
    private UseCaseDiagram _sut;
    
    [SetUp]
    public void SetUp()
    {
        _sut = new UseCaseDiagram();   
    }
    
    [Test]
    public void AddUseCase_GivenTypeHasNoUseCaseAttribute_ThrowsNotSupportedException()
    {
        var act = () => _sut.AddUseCase(GetType());
        
        act.Should().Throw<NotSupportedException>();
    }
    
    [Test]
    public void AddUseCase_UseCaseWithoutActor_GeneratesCorrectDiagram()
    {
        _sut.AddUseCase(typeof(UseCaseWithoutActor));

        _sut.UseCases.Count.Should().Be(1);
        _sut.UseCases[0].Identifier.Should().Be("UseCaseWithoutActor");
        _sut.UseCases[0].Title.Should().Be("Use Case Without Actor");

        _sut.Actors.Count.Should().Be(0);
    }

    [Test]
    public void AddUseCase_UseCaseWithOneActor_GeneratesCorrectDiagram()
    {
        _sut.AddUseCase(typeof(UseCaseWithOneActor));

        _sut.UseCases.Count.Should().Be(1);
        _sut.Actors.Count.Should().Be(1);
        _sut.Relations.Count.Should().Be(1);
    }
    
    [Test]
    public void CreateDiagram_UseCaseWithMultipleActors_AddsMultipleActors()
    {
        _sut.AddUseCase(typeof(UseCaseWithMultipleActors));

        _sut.UseCases.Count.Should().Be(1);
        _sut.Actors.Count.Should().Be(2);
        _sut.Relations.Count.Should().Be(2);

        _sut.Relations.Any(relation =>
            relation.From.Identifier == "Admin" && relation.To.Identifier == "UseCaseWithMultipleActors")
            .Should().BeTrue();
        
        _sut.Relations.Any(relation =>
                relation.From.Identifier == "PremiumUser" && relation.To.Identifier == "UseCaseWithMultipleActors")
            .Should().BeTrue();
    }
    
    [Test]
    public void CreateDiagram_UseCaseTriggeredByUseCase_AddsRelation()
    {
        _sut.AddUseCase(typeof(UseCaseTriggeredByAnotherUseCase));

        _sut.UseCases.Count.Should().Be(2);
        _sut.Actors.Count.Should().Be(1);
        _sut.Relations.Count.Should().Be(2);

        _sut.Relations.Any(relation =>
                relation.From.Identifier == "UseCaseWithOneActor" && relation.To.Identifier == "UseCaseTriggeredByAnotherUseCase")
            .Should().BeTrue();
    }
    
    [Test]
    public void CreateDiagram_USeCaseWithAlreadyAddedActor_DoesNotAddActorAgain()
    {
        _sut.AddUseCase(typeof(UseCaseWithOneActor));
        
        _sut.AddUseCase(typeof(UseCaseWithMultipleActors));
        
        _sut.Actors.Count.Should().Be(2);
    }

    [Test]
    public void ToPlantUml_EmptyDiagram_ReturnsValidUml()
    {
        var uml = _sut.ToPlantUml();

        var trimmedUml = uml.Replace(Environment.NewLine, "");
        trimmedUml.Should().Be(@"@startuml@enduml");
        trimmedUml.Should().NotBeEquivalentTo(uml);
    }
}