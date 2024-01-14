// See https://aka.ms/new-console-template for more information

using DomainDrivenDesign.DiagramGenerators;
using DomainDrivenDesign.SampleDomain;

var assembly = typeof(UseCaseWithMultipleActors).Assembly;
var provider = DocumentationProvider.FromAssembly(assembly);
var formatter = DocumentationFormatter.ForDefaultMarkdown();
var generator = new DocumentationGenerator(provider, formatter);

var documentation = generator.DocumentUseCase(typeof(UseCaseWithMultipleActors));
File.WriteAllText("documentation.md", documentation);
Console.WriteLine(documentation);

var documentation2 = generator.DocumentUseCase(typeof(UseCaseWithOneActor));
File.WriteAllText("documentation2.md", documentation2);
Console.WriteLine(documentation2);

//var generator = new UseCaseDiagramGenerator(assembly, true);
//var diagram = generator.CreateDiagramForAllUseCases();
//var uml = diagram.ToPlantUml();
//Console.WriteLine(uml);