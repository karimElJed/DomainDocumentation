using System.Reflection;
using DomainDrivenDesign.Core.Attributes;
using DomainDrivenDesign.DiagramGenerators;
using DomainDrivenDesign.DiagramGenerators.Diagrams.UseCases;
using DomainDrivenDesign.SampleDomain;

var assembly = typeof(UseCaseWithMultipleActors).Assembly;
var provider = DocumentationProvider.FromAssembly(assembly);
var formatter = DocumentationFormatter.ForDefaultMarkdown();
var generator = new DocumentationGenerator(provider, formatter);

var useCases = assembly.GetTypes()
    .Where(t => t.GetCustomAttribute(typeof(UseCaseAttribute), true) != null)
    .ToList();

foreach (var useCaseType in useCases)
{
    var documentation = generator.DocumentUseCase(useCaseType);
    File.WriteAllText($"../../../../docs/{useCaseType.Name}.md", documentation);
    Console.WriteLine(documentation);
}

var diagramGenerator = new UseCaseDiagramGenerator(assembly, provider);
var diagram = diagramGenerator.CreateDiagramForAllUseCases();
var uml = diagram.ToPlantUml();
File.WriteAllText("all_usecases.puml", uml);
Console.WriteLine(uml);