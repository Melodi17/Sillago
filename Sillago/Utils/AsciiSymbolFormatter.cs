namespace Sillago.Utils;

using System.Text;
using Symbols;

public class AsciiSymbolFormatter : SymbolFormatter
{
    private Dictionary<char, char> _subScriptMap = new()
    {
        { '0', '₀' }, { '1', '₁' }, { '2', '₂' }, { '3', '₃' },
        { '4', '₄' }, { '5', '₅' }, { '6', '₆' }, { '7', '₇' },
        { '8', '₈' }, { '9', '₉' },
    };
    
    protected override string Format(Element element) => element.ShortSymbol;
    protected override string Format(Compound compound)
    {
        StringBuilder builder = new();

        foreach (CompoundComponent component in compound.Components)
        {
            if (component.Value is Compound)
                builder.Append($"({this.Format(component.Value)})");
            else
                builder.Append(this.Format(component.Value));

            if (component.Amount > 1)
                builder.Append(new string(component.Amount.ToString().Select(c =>  this._subScriptMap[c]).ToArray()));
        }

        return builder.ToString();
    }

    protected override string Format(Polymer polymer)
        => $"({this.Format(polymer.Source)})ⁿ";
}