using System.Collections;

namespace Sillago.Materials.Types;

using Items;

public class IngotMaterial : Material
{
    public float MeltingPoint;

    public IngotMaterial(string name, int color, VisualSet visualSet, Element element, MaterialFlags flags, float density, float meltingPoint) : this(name)
    {
        this.Name      = name;
        this.Color     = color;
        this.VisualSet = visualSet;
        this.Flags     = flags;
        this.Density   = density;
        this.Symbol    = element.Symbol;

        this.MeltingPoint = meltingPoint;
    }

    protected IngotMaterial(string name) : base(name)
    {
        this.OverrideFormName(MaterialType.Liquid, $"Molten {name}");
    }

    public override IEnumerator Generate()
    {
        ItemMaterial powder = new ItemMaterial(this, MaterialType.Powder);
        ItemMaterial ingot = new ItemMaterial(this,  MaterialType.Ingot);
        ItemMaterial rod = new ItemMaterial(this,    MaterialType.Rod);

        yield return powder;
        yield return ingot;
        yield return rod;

        powder.SmeltsInto(ingot, this.MeltingPoint / 3f);
        ingot.LathesInto(rod);
        
        ItemMaterial molten = new ItemMaterial(this, MaterialType.Liquid);
        yield return molten;

        if (!this.Is(MaterialFlags.Brittle))
        {
            ItemMaterial plate = new ItemMaterial(this, MaterialType.Plate);
            yield return plate;

            ingot.PressesInto(plate);
        }

        if (this.Is(MaterialFlags.ElectricallyConductive))
        {
            yield return new ItemMaterial(this, MaterialType.Wire);
            yield return new ItemMaterial(this, MaterialType.Cable);
        }

        if (this.Is(MaterialFlags.Ductile))
            yield return new ItemMaterial(this, MaterialType.Coil);

        if (this.Is(MaterialFlags.Ore))
            yield return new ItemMaterial(this, MaterialType.Ore);
    }
}