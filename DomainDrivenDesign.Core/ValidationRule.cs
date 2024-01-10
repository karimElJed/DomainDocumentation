using System.Diagnostics;

namespace DomainDrivenDesign.Core;

[DebuggerDisplay("{Description}")]
public class ValidationRule<T>
{
    private readonly Func<T,bool> _validationFunction;

    public ValidationRule(string description, Func<T, bool> validation)
    {
        Description = description;
        _validationFunction = validation;
    }

    public string Description { get; set; }

    public void Check(T value)
    {
        if (!_validationFunction(value))
        {
            throw new DomainException(Description);
        }
    }
}