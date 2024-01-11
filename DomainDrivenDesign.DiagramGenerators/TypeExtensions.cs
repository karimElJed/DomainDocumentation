using DomainDrivenDesign.Core.Attributes;

namespace DomainDrivenDesign.DiagramGenerators;

public static class TypeExtensions
{
    public static bool IsActor(this Type type)
    {
        return type.IsDefined(typeof(ActorAttribute), true);
    }
    
    public static bool IsUseCase(this Type type)
    {
        return type.IsDefined(typeof(UseCaseAttribute), true);
    }
}