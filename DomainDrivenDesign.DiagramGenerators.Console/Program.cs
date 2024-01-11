// See https://aka.ms/new-console-template for more information

using DomainDrivenDesign.DiagramGenerators.UseCases;
using DomainDrivenDesign.SampleDomain;

var generator = new UseCaseDiagramGenerator(typeof(UseCaseWithMultipleActors));
var diagram = generator.CreateDiagram();

var uml = diagram.ToPlantUml();
Console.WriteLine(uml);