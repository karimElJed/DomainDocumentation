using DomainDocumentation.Attributes;

namespace DomainDocumentation.SampleDomain.Documenting;

/// <summary>
/// The documentation should be clear and understandable. Therefore
/// a review is needed whenever the documentation is updated.
/// The documentation state reflects the current status of the documentation
/// during a review.
/// </summary>
[State]
public enum DocumentationState
{
    /// <summary>
    /// The source code changed and the documentation was updated.
    /// </summary>
    [TransformsInto<DocumentationState>(InReview, Trigger = "Pull Request created")]
    Draft,
    
    /// <summary>
    /// The reviewer started the review process.
    /// </summary>
    [TransformsInto<DocumentationState>(Commented, Trigger = "Lack of clarity")]
    [TransformsInto<DocumentationState>(Approved, Trigger = "Documentation is clear and complete")]
    InReview,
    
    /// <summary>
    /// The reviewer has open questions or found issues in the documentation.
    /// </summary>
    [TransformsInto<DocumentationState>(Fixed, Trigger = "The developer updated the documentation.")]
    Commented,
    
    /// <summary>
    /// Open questions and issues have been resolved by the developer.
    /// </summary>
    [TransformsInto<DocumentationState>(Approved, Trigger = "Reviewer is satisfied with the changes.")]
    [TransformsInto<DocumentationState>(Commented, Trigger = "Reviewer still has questions or proposals for changes.")]
    Fixed,
        
    /// <summary>
    /// The documentation is clear and complete and can be used by the team.
    /// </summary>
    Approved
}