namespace Sillago.Symbols;

public class Compound : Symbol
{
    public string? Name { get; }
    public readonly CompoundComponent[] Components;
    
    public override string DisplayText => Name ?? GenerateDisplayText();
    public override string ShortSymbol => GenerateShortSymbol();
    
    public Compound(string name, params CompoundComponent[] components)
    {
        this.Name = name;
        this.Components = components;
    }
    
    public Compound(params CompoundComponent[] components)
    {
        this.Name = null;
        this.Components = components;
    }
    
    private string GenerateDisplayText()
    {
        if (Components.Length == 0) return "Unknown Compound";
        return string.Join(" + ", Components.Select(c => $"{c.Count}x {c.Component.DisplayText}"));
    }
    
    private string GenerateShortSymbol()
    {
        if (Components.Length == 0) return "?";
        return string.Join("", Components.Select(c => 
            c.Count == 1 ? c.Component.ShortSymbol : $"{c.Component.ShortSymbol}{c.Count}"));
    }
}