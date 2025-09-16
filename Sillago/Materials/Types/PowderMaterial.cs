namespace Sillago.Types;

using System.Collections;
using Requirements;
using Symbols;

public class PowderMaterial : Material
{
    public float? LiquificationPoint;
    public PowderMaterial(string name, int color, VisualSet visualSet, Symbol symbol, MaterialFlags flags, float density, float? liquificationPoint = null)
        : this(name, symbol)
    {
        this.Color              = color;
        this.VisualSet          = visualSet;
        this.Flags              = flags;
        this.Density            = density;
        this.LiquificationPoint = liquificationPoint;
    }

    protected PowderMaterial(string name, Symbol symbol) : base(name, symbol)
    {
        this.OverrideFormName(MaterialType.Liquid, $"Liquified {name}");
    }

    public override IEnumerator Generate()
    {
        ItemMaterial powder = new ItemMaterial(this, MaterialType.Powder);
        yield return powder;

        if (this.LiquificationPoint.HasValue)
        {
            ItemMaterial liquid = new ItemMaterial(this, MaterialType.Liquid);
            yield return liquid;

            yield return new RecipeBuilder(RecipeType.Liquification)
                .NamePatterned($"<input> <verb>")
                .AddInput(powder.Stack())
                .AddOutput(liquid.Stack(250))
                .SetDuration(TimeSpan.FromSeconds(5))
                .AddRequirement(TemperatureRequirement.Above(this.LiquificationPoint.Value))
                .Build();
        }
    }
}