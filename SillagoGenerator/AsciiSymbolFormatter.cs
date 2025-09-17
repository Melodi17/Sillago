namespace Sillago.Utils
{
    using System.Text;
    using Symbols;

    public class CodeSymbolFormatter : SymbolFormatter
    {
        protected override string Format(Element element) => $"Element.{element.Symbol}";
        protected override string Format(Compound compound)
        {
            StringBuilder builder = new();
            builder.Append("new Compound(");

            for (int i = 0; i < compound.Components.Length; i++)
            {
                CompoundComponent component = compound.Components[i];
                builder.Append(this.Format(component));

                if (i < compound.Components.Length - 1)
                    builder.Append(", ");
            }

            builder.Append(")");
            return builder.ToString();
        }

        protected override string Format(CompoundComponent component)
        {
            return $"{this.Format(component.Value)} * {component.Amount}";
        }

        protected override string Format(Polymer polymer) => $"{this.Format(polymer.Source)}.Polymer()";
    }
}