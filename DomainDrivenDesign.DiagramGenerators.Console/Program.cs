// See https://aka.ms/new-console-template for more information

using DomainDrivenDesign.DiagramGenerators.UseCases;
using DomainDrivenDesign.SampleDomain;

var generator = new UseCaseDiagramGenerator(typeof(UseCaseWithMultipleActors).Assembly, true);
var diagram = generator.CreateDiagramForAllUseCases();

var uml = diagram.ToPlantUml();
Console.WriteLine(uml);