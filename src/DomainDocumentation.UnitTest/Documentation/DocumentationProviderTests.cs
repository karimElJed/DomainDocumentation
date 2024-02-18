using DomainDocumentation.SampleDomain.ImportantPart.UseCases;
using FluentAssertions;
using NUnit.Framework;

namespace DomainDocumentation.UnitTest.Documentation;

[TestFixture]
public class DocumentationProviderTests
{
    private DocumentationProvider _sut = null!;

    [SetUp]
    public void SetUp()
    {
        _sut = DocumentationProvider.FromAssembly(typeof(UseCaseWithoutActor).Assembly);
    }

    [Test]
    public void GetDocumentation_ForType_ReturnsSummaryOfClass()
    {
        var result = _sut.GetDocumentation(typeof(UseCaseWithMultipleActors));

        result.InnerText.Should().NotBeEmpty();
    }
}