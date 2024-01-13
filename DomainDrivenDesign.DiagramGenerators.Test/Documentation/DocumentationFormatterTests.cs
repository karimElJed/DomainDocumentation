using DomainDrivenDesign.SampleDomain;
using FluentAssertions;
using NUnit.Framework;

namespace DomainDrivenDesign.DiagramGenerators.Test.Documentation;

[TestFixture]
public class DocumentationFormatterTests
{
    private DocumentationFormatter _sut = null!;

    [SetUp]
    public void SetUp()
    {
        var currentDirectory = Path.GetDirectoryName(GetType().Assembly.Location)!;
        _sut = new DocumentationFormatter(Path.Combine(currentDirectory, "markdown.xslt"));
    }
        
    [Test]
    public void Foo()
    {
        var provider = DocumentationProvider.FromAssembly(typeof(UseCaseWithoutActor).Assembly);
        var documentation = provider.GetDocumentation(typeof(UseCaseWithMultipleActors))!;
        var markdown = _sut.Format(documentation);

        markdown.Should().NotBeEmpty();
    }
}