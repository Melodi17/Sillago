namespace Sillago.Utils;

using System.Text;
using Symbols;

public class DefaultSymbolFormatter : SymbolFormatter
{
    protected override string Format(Element element) => element.Symbol;
    protected override string Format(Compound compound)
    {
        StringBuilder builder = new();

        foreach (CompoundComponent component in compound.Components)
            builder.Append(this.Format(component));

        return builder.ToString();
    }
    
    protected override string Format(CompoundComponent component)
    {
        StringBuilder builder = new();
        
        if (component.Value is Compound)
            builder.Append($"({this.Format(component.Value)})");
        else
            builder.Append(this.Format(component.Value));

        if (component.Amount > 1)
            builder.Append($"{component.Amount}");

        return builder.ToString();
    }
    
    protected override string Format(Polymer polymer)
        => $"({this.Format(polymer.Source)})n";
}