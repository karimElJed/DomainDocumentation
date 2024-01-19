using DomainDrivenDesign.Core.Attributes;

namespace DomainDrivenDesign.SampleDomain.ImportantPart;

public class MyAggregate
{
    public ActivationState State { get; set; }
}


[State]
public enum ActivationState
{
    /// <summary>
    /// The invoice needs to be confirmed by the customer.
    /// </summary>
    [TransformsInto<ActivationState>(Confirmed, Trigger = "Confirmed (1-Step Approval)")]
    [TransformsInto<ActivationState>(SecondConfirmationPending, Trigger = "Confirmed (2-Step Approval)")]
    ConfirmationPending,
    
    /// <summary>
    /// The invoice was confirmed by the 1st approver and waits for confirmation of the 2nd approver. 
    /// </summary>
    [TransformsInto<ActivationState>(Confirmed, Trigger = "Confirmed (2-Step Approval)")]
    SecondConfirmationPending,
        
    /// <summary>
    /// The invoice was confirmed by the customer and can be debited.
    /// </summary>
    Confirmed
}