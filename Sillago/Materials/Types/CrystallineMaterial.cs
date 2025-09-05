using System.Collections;

namespace Sillago.Materials.Types;

using Items;

public class CrystallineMaterial : PowderMaterial
{
    public CrystallineMaterial(string name, int color, VisualSet visualSet, Element element, MaterialFlags flags, float density, float? liquificationPoint = null)
        : base(name, color, visualSet, element, flags, density, liquificationPoint) { }

    protected CrystallineMaterial(string name) : base(name) { }

    public override IEnumerator Generate()
    {
        yield return base.Generate();

        ItemMaterial crystal = new ItemMaterial(this, MaterialType.Crystal);
        yield return crystal;

        yield return this.Deferred(() =>
            crystal.PressesInto(Items.GetMaterial(this, MaterialType.Powder)));

        if (this.Is(MaterialFlags.Ore))
            yield return new ItemMaterial(this, MaterialType.Ore);
    }
}