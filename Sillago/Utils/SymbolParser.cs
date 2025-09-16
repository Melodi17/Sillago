namespace Sillago.Utils;

using Symbols;

public class SymbolParser
{
    public static Symbol Parse(string input)
    {
        input = input.Trim().Replace(" ", "");
        if (string.IsNullOrEmpty(input))
            throw new ArgumentException("Input cannot be null or empty.");

        if (SymbolParser.IsElement(input))
            return SymbolParser.ParseElement(input);

        if (IsPolymer(input))
            return SymbolParser.ParsePolymer(input);

        return ParseCompound(input);
    }
    
    private static bool IsElement(string symbol)
    {
        if (symbol.Count(char.IsUpper) > 1 || symbol.Any(char.IsDigit) || symbol.Length > 2)
            return false;

        return true;
    }
    
    private static Element ParseElement(string input)
    {
        return Element.FindBySymbol(input)
               ?? throw new ArgumentException($"Element with symbol '{input}' not found.");
    }

    private static bool IsPolymer(string symbol)
    {
        return symbol.StartsWith('(') && symbol.EndsWith(")n");
    }
    
    private static Polymer ParsePolymer(string input)
    {
        string innerSymbol = input[1..^2].Trim();
        Symbol source = SymbolParser.Parse(innerSymbol);
        return source.Polymer();
    }

    private static Compound ParseCompound(string input)
    {
        List<CompoundComponent> components = new();
        
        int i = 0;
        while (i < input.Length)
        {
            string part = ParseCompoundPart(input, ref i);
            int amount = ParseNumber(input, ref i);

            Symbol symbol = SymbolParser.Parse(part);
            components.Add(new CompoundComponent(symbol, amount));
        }
        
        if (components.Count == 0)
            throw new ArgumentException("No valid components found in compound symbol.");

        return new Compound(components.ToArray());
    }
    
    private static int ParseNumber(string input, ref int index)
    {
        int start = index;
        while (index < input.Length && char.IsDigit(input[index]))
            index++;

        if (start == index)
            return 1;

        string numberStr = input[start..index];
        if (int.TryParse(numberStr, out int number))
            return number;

        throw new ArgumentException($"Invalid number format: '{numberStr}'");
    }
    
    private static string ParseCompoundPart(string input, ref int index)
    {
        if (input[index] == '(')
        {
            int depth = 1;
            int start = index + 1;
            index++;
            while (index < input.Length && depth > 0)
            {
                if (input[index] == '(') depth++;
                else if (input[index] == ')') depth--;
                index++;
            }
            if (depth != 0)
                throw new ArgumentException("Mismatched parentheses in compound symbol.");
            
            return input[start..(index - 1)].Trim();
        }
        
        int startIdx = index;
        index++;
        if (index < input.Length && char.IsLower(input[index])) // Check for two-letter element symbols
            index++;
        return input[startIdx..index].Trim();
    }
}