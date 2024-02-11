using DomainDocumentation.Attributes;

namespace DomainDocumentation.SampleDomain.ImportantPart.UseCases;

/// <summary>
/// This use case is relevant for two different kind of users.
/// The UseCaseAttribute indicates that it is a use case.
/// Also this description is some kind of meaningless,
/// but in a real use case you would explain some more details about the business process.
/// </summary>
/// <remarks>This is a remark</remarks>
[UseCase]
[TriggeredBy(typeof(Actors.Admin), Reason = @"Sometimes the customer will call the admin to solve a problem.
In this case the admin will do the changes in behalf of the user.")]
[TriggeredBy(typeof(Actors.PremiumUser), Reason = "The user wants to have a better life.")]
[TriggeredBy(typeof(Actors.CMS), Reason = "When a normal user is awarded as premium the CMS triggers this use case to reward the user.")]
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