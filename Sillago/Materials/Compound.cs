namespace Sillago.Materials;

using Utils;

public class Compound : Element
{
    private readonly CompoundComponent[] _components;
    public Compound(string name, params CompoundComponent[] components)
        : base(name, SymbolHelper.GetSymbol(components), components[0].Value.AtomicNumber)
    {
        this._components = components;
    }

    public Compound Polymer()
    {
        this.Symbol = SymbolHelper.Polymerize(this.Symbol);
        return this;
    }
}