namespace Contract.Utilities;

public static class LanguageUtility
{
    /// <summary>
    ///     Converts a given language code to its corresponding ISO 639-1 two-letter code.
    /// </summary>
    /// <param name="languageCode">
    ///     The input language code. This can be either a full language name (e.g. "ENGLISH", "VIETNAMESE")
    ///     or an abbreviated form (e.g. "EN", "VI"). The input is normalized before matching.
    /// </param>
    /// <returns>
    ///     A string representing the ISO 639-1 code ("en" for English or "vi" for Vietnamese).
    /// </returns>
    /// <exception cref="ArgumentException">
    ///     Thrown when <paramref name="languageCode"/> is null, empty, or does not match any known language.
    /// </exception>
    public static string ToIso6391(string languageCode)
    {
        if (string.IsNullOrWhiteSpace(languageCode))
        {
            throw new ArgumentException("Language code cannot be null or empty.", nameof(languageCode));
        }
        var normalized = languageCode.Trim().ToUpperInvariant();

        switch (normalized)
        {
            case "VIETNAMESE":
            case "VI":
                return "vi";
            case "ENGLISH":
            case "EN":
                return "en";
            default:
                throw new ArgumentException($"Language code '{languageCode}' does not much any case");
        }
    }
}
