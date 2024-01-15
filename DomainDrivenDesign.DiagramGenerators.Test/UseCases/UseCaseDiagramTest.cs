using DomainDrivenDesign.Core.Attributes;
using DomainDrivenDesign.DiagramGenerators.Diagrams.UseCases;
using DomainDrivenDesign.SampleDomain;
using FluentAssertions;
using NUnit.Framework;

namespace DomainDrivenDesign.DiagramGenerators.Test.UseCases;

[TestFixture]
public class UseCaseDiagramTest
{
    private UseCaseDiagram _sut;
    private IDocumentationProvider _documentationProvider;
    
    [SetUp]
    public void SetUp()
    {
        _documentationProvider = new NoDocumentationProvider();
        _sut = new UseCaseDiagram(_documentationProvider);   
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
        _sut.AddUseCase(typeof(UserAndAdminUseCase));

        _sut.UseCases.Count.Should().Be(1);
        _sut.Actors.Count.Should().Be(2);
        _sut.Relations.Count.Should().Be(2);

        _sut.Relations.Any(relation =>
            relation.From.Identifier == "Admin" && relation.To.Identifier == "UserAndAdminUseCase")
            .Should().BeTrue();
        
        _sut.Relations.Any(relation =>
                relation.From.Identifier == "User" && relation.To.Identifier == "UserAndAdminUseCase")
            .Should().BeTrue();
    }
    
    [Test]
    public void CreateDiagram_UseCaseTriggeredByUseCase_AddsRelation()
    {
        _sut.AddUseCase(typeof(UserAndUseCaseUseCase));

        _sut.UseCases.Count.Should().Be(2);
        _sut.Actors.Count.Should().Be(1);
        _sut.Relations.Count.Should().Be(2);

        _sut.Relations.Any(relation =>
                relation.From.Identifier == "UserUseCase" && relation.To.Identifier == "UserAndUseCaseUseCase")
            .Should().BeTrue();
    }
    
    [Test]
    public void CreateDiagram_USeCaseWithAlreadyAddedActor_DoesNotAddActorAgain()
    {
        _sut.AddUseCase(typeof(UserUseCase));
        
        _sut.AddUseCase(typeof(UserAndAdminUseCase));
        
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
    
    [Actor]
    private class User {}
    
    [Actor]
    private class Admin {}
    
    [UseCase]
    [TriggeredBy(typeof(User))]
    private class UserUseCase {}
    
    [UseCase]
    [TriggeredBy(typeof(User))]
    [TriggeredBy(typeof(Admin))]
    private class UserAndAdminUseCase {}
    
    [UseCase]
    [TriggeredBy(typeof(User))]
    [TriggeredBy(typeof(UserUseCase))]
    private class UserAndUseCaseUseCase {}
}
