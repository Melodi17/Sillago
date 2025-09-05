namespace Sillago.Utils;

public static class Identifier
{
    public static string Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or whitespace.", nameof(name));

        bool FilterOutUnsupportedCharacters(char c) => char.IsLetterOrDigit(c) || c == '_' || c == ' ';
        char ConvertSpacesToUnderscores(char c) => c                                == ' ' ? '_' : c;
        
        string id = new string(name
            .Trim()
            .ToLowerInvariant()
            .Where(FilterOutUnsupportedCharacters)
            .Select(ConvertSpacesToUnderscores)
            .ToArray());
        
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException("Name must contain at least one valid identifier character.", nameof(name));
        
        return id;
    }
}