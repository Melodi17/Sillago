using System.Collections;

namespace Sillago.Materials.Types;

using Items;

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
            crystal.MaceratesInto([Items.GetMaterial(this, MaterialType.Powder).Stack(4)]));

        if (this.Is(MaterialFlags.Ore))
            yield return new ItemMaterial(this, MaterialType.Ore);
    }
}