using DomainDrivenDesign.Core.Attributes;
// ReSharper disable InconsistentNaming

namespace DomainDrivenDesign.SampleDomain;

public static class Actors
{
    [Actor]
    public static class Admin
    {
    }

    [Actor]
    public static class PremiumUser
    {
    }
    
    [Actor(Stereotype = "Application")]
    public static class CMS {}
}