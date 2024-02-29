using DomainDocumentation.Attributes;

// ReSharper disable InconsistentNaming

namespace DomainDocumentation.UnitTest.TestDomain;

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
    /// A buyer is a user who shops and buys stuff in this shop.
    /// </summary>
    [Actor]
    public static class PremiumUser {}

    /// <summary>
    /// A premium buyer is a user that has already bought a lot of items in this shop.
    /// </summary>
    [Actor]
    public static class PremiumBuyer
    {
    }
    
    /// <summary>
    /// The Content Management System (CMS) accesses this application
    /// in order to prepare marketing emails and other stuff.
    /// </summary>
    [Actor(Stereotype = "Application")]
    public static class CMS {}
}