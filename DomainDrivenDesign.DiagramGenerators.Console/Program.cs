using System.Reflection;
using System.Text;
using DomainDrivenDesign.Core.Attributes;
using DomainDrivenDesign.DiagramGenerators;
using DomainDrivenDesign.DiagramGenerators.Diagrams.UseCases;
using DomainDrivenDesign.SampleDomain;
using PlantUML.TextEncoder;

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
File.WriteAllText("../../../../docs/all_usecases.puml", uml);

var imgParameter = PlantUmlTextEncoder.Encode(uml);

var markdown = new StringBuilder();
markdown.AppendLine("# All Use Cases");
markdown.AppendLine($"<img src=\"https://www.plantuml.com/plantuml/svg/{imgParameter}\"/>");
File.WriteAllText("../../../../docs/all_usecases.md", markdown.ToString());
Console.WriteLine(uml);