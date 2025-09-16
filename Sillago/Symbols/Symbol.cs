namespace Sillago.Symbols;

/// <summary>
/// Base class for all chemical symbols (elements, compounds, polymers, etc.)
/// </summary>
public abstract class Symbol
{
    /// <summary>
    /// Gets the display text representation of this symbol
    /// </summary>
    public abstract string DisplayText { get; }
    
    /// <summary>
    /// Gets the short symbol representation (e.g., "H2O", "Fe", "C6H12O6")
    /// </summary>
    public abstract string ShortSymbol { get; }
    
    public override string ToString() => DisplayText;
}

/// <summary>
/// A generic symbol for materials without specific chemical composition
/// </summary>
public class GenericSymbol : Symbol
{
    public string Name { get; }
    public override string DisplayText => Name;
    public override string ShortSymbol => Name.Length > 3 ? Name[..3].ToUpper() : Name.ToUpper();
    
    public GenericSymbol(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }
}