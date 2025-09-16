namespace Sillago.Symbols;

public class Polymer : Symbol
{
    public Symbol Source { get; }
    public Polymer(Symbol source)
    {
        this.Source = source;
    }
}

public static class PolymerExtensions
{
    public static Polymer Polymer(this Symbol source) => new Polymer(source);
}