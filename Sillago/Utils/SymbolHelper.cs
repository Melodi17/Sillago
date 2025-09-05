namespace Sillago.Utils;

using System.Text;
using Materials;

public class SymbolHelper
{
    /// <summary>
    /// Creates a chemical symbol for a compound based on its components and their amounts.
    /// For example, for components [{Value: H, Amount: 2}, {Value: O, Amount: 1}], it returns "H2O".
    /// </summary>
    /// <exception cref="ArgumentException">Thrown if components array is empty.</exception>
    public static string GetSymbol<T>(ValueComponent<T>[] components)
        where T : ISymbolizable
    {
        if (components.Length == 0)
            throw new ArgumentException("Components cannot be empty.", nameof(components));

        StringBuilder symbolBuilder = new();

        foreach (ValueComponent<T> component in components)
        {
            if (SymbolHelper.IsComplex(component.Value.Symbol))
                symbolBuilder.Append($"({component.Value.Symbol})");
            else
                symbolBuilder.Append(component.Value.Symbol);

            if (component.Amount > 1)
                symbolBuilder.Append($"{component.Amount}");
        }

        return symbolBuilder.ToString();
    }

    /// <summary>
    /// Returns true if the chemical symbol represents a complex compound (more than one element or contains numbers).
    /// Examples:
    ///     "H" -> false (simple)
    ///     "Al" -> false (simple)
    ///     "H2O" -> true (complex)
    ///     "C6H12O6" -> true (complex)
    /// </summary>
    /// <exception cref="ArgumentException">Thrown if symbol is null or empty.</exception>
    public static bool IsComplex(string symbol)
    {
        if (string.IsNullOrEmpty(symbol))
            throw new ArgumentException("Symbol cannot be null or empty.", nameof(symbol));

        if (symbol.Count(char.IsUpper) > 1 || symbol.Any(char.IsDigit))
            return true;

        return false;
    }

    /// <summary>
    /// Converts a chemical symbol into its polymerized form by appending 'n' to indicate polymerization.
    /// For example, "C2H4" becomes "(C2H4)n".
    /// If the symbol is already in polymer form (ends with 'n'), it is returned unchanged.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown if symbol is null or empty.</exception>
    public static string Polymerize(string symbol)
    {
        if (string.IsNullOrEmpty(symbol))
            throw new ArgumentException("Symbol cannot be null or empty.", nameof(symbol));

        // If the symbol is already a polymer, return it as is
        if (symbol.EndsWith("n", StringComparison.OrdinalIgnoreCase))
            return symbol;

        // Append 'n' to indicate polymerization
        return $"({symbol})n";
    }
    
    /// <summary>
    /// Converts given symbol to a format with ascii super and sub scripts.
    /// Example: "H2O" becomes "H₂O", "C6H12O6" becomes "C₆H₁₂O₆"
    /// </summary>
    public static string FormatSymbol(string symbol)
    {
        if (string.IsNullOrEmpty(symbol))
            throw new ArgumentException("Symbol cannot be null or empty.", nameof(symbol));

        Dictionary<char, char> superScriptMap = new()
        {
            { 'n', 'ⁿ' }
        };

        Dictionary<char, char> subScriptMap = new()
        {
            { '0', '₀' }, { '1', '₁' }, { '2', '₂' }, { '3', '₃' },
            { '4', '₄' }, { '5', '₅' }, { '6', '₆' }, { '7', '₇' },
            { '8', '₈' }, { '9', '₉' },
        };
        
        
        StringBuilder converted = new();
        char previousChar = '\0';
        foreach (char c in symbol)
        {
            // If the character is 'n' and the previous character is ')', convert to superscript
            if (superScriptMap.ContainsKey(c) && previousChar == ')')
                converted.Append(superScriptMap[c]);
            else if (subScriptMap.ContainsKey(c))
                converted.Append(subScriptMap[c]);
            else
                converted.Append(c);
            
            previousChar = c;
        }
        
        return converted.ToString();
    }
}