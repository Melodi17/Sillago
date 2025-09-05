using System.Collections;

namespace Sillago.Materials.Types;

using Items;

public class FluidMaterial : Material
{
    public float FreezingPoint;
    public float VaporisationPoint;

    public FluidMaterial(string name, int color, VisualSet visualSet, Element element, MaterialFlags flags, float density, float freezingPoint = 0f, float vaporisationPoint = 0f)
        : this(name)
    {
        this.Name      = name;
        this.Color     = color;
        this.VisualSet = visualSet;
        this.Flags     = flags;
        this.Density   = density;
        this.Symbol    = element.Symbol;

        this.FreezingPoint     = freezingPoint;
        this.VaporisationPoint = vaporisationPoint;
    }
    
    protected FluidMaterial(string name) : base(name)
    {
        this.OverrideFormName(MaterialType.Liquid, $"{name}");
    }
    
    public override IEnumerator Generate()
    {
        ItemMaterial liquid = new ItemMaterial(this, MaterialType.Liquid);
        yield return liquid;

        ItemMaterial ice = new ItemMaterial(this, MaterialType.Ice);
        yield return ice;
        // liquid.FreezesInto(ice, this.FreezingPoint);

        ItemMaterial gas = new ItemMaterial(this, MaterialType.Gas);
        yield return gas;
        // liquid.VaporisesInto(gas, this.VaporisationPoint);
    }
}