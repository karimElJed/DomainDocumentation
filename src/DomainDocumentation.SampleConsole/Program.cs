using System.Reflection;
using System.Text;
using DomainDocumentation.Attributes;
using DomainDocumentation.SampleDomain.ImportantPart;
using DomainDocumentation.SampleDomain.ImportantPart.UseCases;
using DomainDrivenDesign.DiagramGenerators;
using DomainDrivenDesign.DiagramGenerators.Diagrams.StateTransitions;
using DomainDrivenDesign.DiagramGenerators.Diagrams.UseCases;
using DomainDrivenDesign.DiagramGenerators.Utils;
using PlantUML.TextEncoder;

var assembly = typeof(UseCaseWithMultipleActors).Assembly;
var provider = DocumentationProvider.FromAssembly(assembly);
var formatter = DocumentationFormatter.ForDefaultMarkdown();
var generator = new DocumentationGenerator(provider, formatter);

var useCaseGroups = assembly.GetTypes()
    .Where(t => t.GetCustomAttribute(typeof(UseCaseAttribute), true) != null)
    .GroupBy(t => t.Namespace);

var documentationPath = Path.Combine("..", "..", "..", "..", "docs");
var rootNamespace = "DomainDrivenDesign";

foreach (var useCaseGroup in useCaseGroups)
{
    var links = new List<string>();
    var namespacePath = useCaseGroup.Key.Replace(rootNamespace, "").Split('.');
    var topic = namespacePath.Last() ?? "";
    var savePath = documentationPath;
    
    foreach (var path in namespacePath)
    {
        savePath = Path.Combine(savePath, path);
    }
    
    if (!Directory.Exists(savePath))
    {
        Directory.CreateDirectory(savePath);
    }
    
    foreach (var useCaseType in useCaseGroup)
    {
        var documentation = generator.DocumentUseCase(useCaseType);
        File.WriteAllText(Path.Combine(savePath, $"{useCaseType.Name}.md"), documentation);
        links.Add(useCaseType.Name);
    }
    
    var diagramGenerator = new UseCaseDiagramGenerator(assembly, provider);
    var diagram = diagramGenerator.CreateDiagramForAllUseCases();
    var uml = diagram.ToPlantUml();
    var imgParameter = PlantUmlTextEncoder.Encode(uml);

    var markdown = new StringBuilder();
    markdown.AppendLine($"# {TextUtils.ReplaceDotsWithWhitespaces(topic)} Use Cases");
    markdown.AppendLine($"<img src=\"https://www.plantuml.com/plantuml/svg/{imgParameter}\"/>");
    markdown.AppendLine();
        
    markdown.AppendLine("## Details");
    foreach (var link in links)
    {
        markdown.AppendLine($"[{TextUtils.ReplaceDotsWithWhitespaces(link)}]({link}.md)");
        markdown.AppendLine();
    }
    
    File.WriteAllText(Path.Combine(savePath, "_Index.md"), markdown.ToString());
    Console.WriteLine(uml);
    
    var stateDiagram = new StateDiagram(provider);
    stateDiagram.AddStates(typeof(ActivationState));
    uml = stateDiagram.ToPlantUml();
    
    File.WriteAllText(Path.Combine(savePath, $"{nameof(ActivationState)}.puml"), uml);
    Console.Write(uml);
}