using DomainDocumentation.Attributes;

namespace DomainDocumentation.SampleDomain.ImportantPart;

[State]
public enum ActivationState
{
    /// <summary>
    /// The order needs to be confirmed by the supervisor.
    /// </summary>
    [TransformsInto<ActivationState>(ConfirmationInProcess, Trigger = "Confirmed")]
    ConfirmationPending,
    
    /// <summary>
    /// The order was confirmed by the supervisor 
    /// and is being processed by the system. 
    /// </summary>
    [TransformsInto<ActivationState>(Confirmed, Trigger = "Process finished")]
    ConfirmationInProcess,
        
    /// <summary>
    /// The order was successfully processed by the system.
    /// </summary>
    Confirmed
}