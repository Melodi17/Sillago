namespace Sillago.Types;

using System.Collections;
using Sillago.Symbols;

public class CrystallineMaterial : PowderMaterial
{
    public CrystallineMaterial(string name, int color, VisualSet visualSet, Element symbol, MaterialFlags flags, float density, float? liquificationPoint = null)
        : base(name, color, visualSet, symbol, flags, density, liquificationPoint) { }

    protected CrystallineMaterial(string name) : base(name) { }

    public override IEnumerator Generate()
    {
        yield return base.Generate();

        ItemMaterial crystal = new ItemMaterial(this, MaterialType.Crystal);
        yield return crystal;

        yield return this.Deferred(() =>
            new RecipeBuilder(RecipeType.Macerating)
                .NamePatterned($"<input> <verb>")
                .AddInput(crystal.Stack(2))
                .AddOutput(new ItemMaterial(this, MaterialType.Powder).Stack(5 * 3))
                .SetDuration(TimeSpan.FromSeconds(2))
                .BuildAndRegister());

        if (this.Is(MaterialFlags.Ore))
            yield return new ItemMaterial(this, MaterialType.Ore);
    }
}