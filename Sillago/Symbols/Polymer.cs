namespace Sillago.Symbols;

public class Polymer : Symbol
{
    public Symbol Source { get; }
    public override string DisplayText => $"Poly({Source.DisplayText})";
    public override string ShortSymbol => $"({Source.ShortSymbol})n";
    
    public Polymer(Symbol source)
    {
        this.Source = source ?? throw new ArgumentNullException(nameof(source));
    }
}

public static class PolymerExtensions
{
    public static Polymer Polymer(this Symbol source) => new Polymer(source);
}