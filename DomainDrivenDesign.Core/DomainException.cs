namespace DomainDrivenDesign.Core;

public class DomainException : ApplicationException
{
    public DomainException(string message) :base(message)
    {
    }
}