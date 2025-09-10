using System.Collections;

namespace Sillago.Materials.Types;

using Items;
using Utils;

public class FluidMaterial : Material
{
    public float FreezingPoint;
    public float VaporisationPoint;

    public FluidMaterial(string name, int color, VisualSet visualSet, Symbol symbol, MaterialFlags flags, float density, float freezingPoint, float vaporisationPoint)
        : this(name, freezingPoint, vaporisationPoint)
    {
        this.Name      = name;
        this.Color     = color;
        this.VisualSet = visualSet;
        this.Flags     = flags;
        this.Density   = density;
        this.Symbol    = symbol;

        this.FreezingPoint     = freezingPoint;
        this.VaporisationPoint = vaporisationPoint;
    }
    
    protected FluidMaterial(string name, float freezingPoint = 0f, float vaporisationPoint = 100f)
        : base(name)
    {
        if (IsAboveRoomTemperature(vaporisationPoint))
            // Liquids that boil above room temperature are usually called liquids
            this.OverrideFormName(MaterialType.Liquid, $"{name}");
        else
            // Fluids that boil below room temperature are usually called gases
            this.OverrideFormName(MaterialType.Gas, $"{name}");
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
    
    private static bool IsAboveRoomTemperature(float temperature)
        => temperature > 20f;
}