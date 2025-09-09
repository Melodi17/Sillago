namespace Sillago.Utils;

using Materials;
using Materials.Types;

public abstract class SymbolFormatter
{
    public string Format(Symbol symbol) => symbol switch
    {
        Element e => this.Format(e),
        Compound c => this.Format(c),
        Polymer p => this.Format(p),
        _ => throw new NotSupportedException($"Symbol type {symbol.GetType().Name} is not supported.")
    };
    
    protected abstract string Format(Element element);
    protected abstract string Format(Compound compound);
    protected abstract string Format(Polymer polymer);
}