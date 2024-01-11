using System.Reflection;
using DomainDrivenDesign.Core.Attributes;

namespace DomainDrivenDesign.DiagramGenerators;

public static class TypeExtensions
{
    public static bool IsActor(this Type type, out ActorAttribute? actorAttribute)
    {
        actorAttribute = type.GetCustomAttribute<ActorAttribute>(true);

        return actorAttribute != null;
    }
    
    public static bool IsUseCase(this Type type)
    {
        return type.IsDefined(typeof(UseCaseAttribute), true);
    }
}