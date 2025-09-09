using System.Collections;

namespace Sillago.Materials.Types;

public class MetalMaterial : IngotMaterial
{
    public MetalMaterial(string name, int color, VisualSet visualSet, Element symbol, MaterialFlags flags, float density, float meltingPoint)
        : base(name, color, visualSet, symbol, flags, density, meltingPoint) { }

    protected MetalMaterial(string name) : base(name)
    {
        this.OverrideFormName(MaterialType.Liquid, $"Molten {name}");
    }

    public override IEnumerator Generate()
    {
        yield return base.Generate();
    }
}