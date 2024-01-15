using DomainDrivenDesign.Core.Attributes;
// ReSharper disable InconsistentNaming

namespace DomainDrivenDesign.SampleDomain;

public static class Actors
{
    /// <summary>
    /// The administrator of the application. This kind of users have god mode permissions
    /// and can do whatever they want.
    /// </summary>
    [Actor]
    public static class Admin
    {
    }

    /// <summary>
    /// User become premium users after spending a lot of money in this product.
    /// They get better support and some extra features.
    /// </summary>
    [Actor]
    public static class PremiumUser
    {
    }
    
    /// <summary>
    /// The Content Management System (CMS) accesses this application
    /// in order to prepare marketing emails and other stuff.
    /// </summary>
    [Actor(Stereotype = "Application")]
    public static class CMS {}
}