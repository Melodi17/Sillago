using System.Collections;

namespace Sillago.Materials.Types;

using Items;

public class PowderMaterial : Material
{
    public float? LiquificationPoint;
    public PowderMaterial(string name, int color, VisualSet visualSet, Element element, MaterialFlags flags, float density, float? liquificationPoint = null)
        : this(name)
    {
        this.Name               = name;
        this.Color              = color;
        this.VisualSet          = visualSet;
        this.Flags              = flags;
        this.Density            = density;
        this.Symbol             = element.Symbol;
        this.LiquificationPoint = liquificationPoint;
    }

    protected PowderMaterial(string name) : base(name)
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

            powder.SmeltsInto(liquid, this.LiquificationPoint.Value, outputQuantity: 350);
        }
    }
}