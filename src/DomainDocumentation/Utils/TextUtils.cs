using System.Text.RegularExpressions;

namespace DomainDrivenDesign.DiagramGenerators.Utils;

public static class TextUtils
{
    public static string ReplaceDotsWithWhitespaces(string identifier)
    {
        return Regex.Replace(identifier, "(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])", " $1");
    }
}