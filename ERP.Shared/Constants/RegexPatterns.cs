using System.Text.RegularExpressions;

namespace ERP.Shared.Constants;

public static partial class RegexPatterns
{
    public static readonly Regex EmailRegexPattern = EmailRegexPatternAttr();
    public static readonly Regex ColorRegexPattern = ColorRegexPatternAttr();

    [GeneratedRegex(
        "^([0-9a-zA-Z]([\\+\\-_\\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]*\\.)+[a-zA-Z0-9]{2,17})$",
        RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant)]
    private static partial Regex EmailRegexPatternAttr();

    [GeneratedRegex(
        @"^#?([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$",
        RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant)]
    private static partial Regex ColorRegexPatternAttr();
}
