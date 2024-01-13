using DomainDrivenDesign.Core.Attributes;

namespace DomainDrivenDesign.SampleDomain;

/// <summary>
/// This use case is relevant for two different kind of users.
/// The <see cref="UseCaseAttribute"/> indicates that it is a use case.
/// Also this description is some kind of meaningless,
/// but in a real use case you would explain some more details about the business process.
/// </summary>
/// <remarks>This is a remark</remarks>
[UseCase]
[TriggeredBy(typeof(Actors.Admin))]
[TriggeredBy(typeof(Actors.PremiumUser))]
public class UseCaseWithMultipleActors
{

    /// <summary>
    /// Gets or sets a number.
    /// </summary>
    public int EineZahl { get; set; }

    /// <summary>
    /// Executes something.
    /// </summary>
    /// <param name="einParameter">Something meaningful.</param>
    public void EineMethode(string einParameter)
    {
        
    }
}