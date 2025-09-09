using System.Collections;

namespace Sillago.Materials.Types;

using Items;

public class FluidMaterial : Material
{
    public float FreezingPoint;
    public float VaporisationPoint;

    public FluidMaterial(string name, int color, VisualSet visualSet, Element element, MaterialFlags flags, float density, float freezingPoint, float vaporisationPoint)
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

        ItemMaterial gas = new ItemMaterial(this, MaterialType.Gas);
        yield return gas;
        
        liquid.FreezesInto(ice, this.FreezingPoint);
        ice.ThawsInto(liquid, this.FreezingPoint);
        
        liquid.VaporisesInto(gas, this.VaporisationPoint);
        gas.CondensesInto(liquid, this.VaporisationPoint);
    }
}