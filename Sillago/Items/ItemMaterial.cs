namespace Sillago;

using Sillago.Utils;

public class ItemMaterial : Item
{
    public readonly Material     Material;
    public readonly MaterialType Type;

    public override bool CountAsVolume => this.Type is MaterialType.Liquid or MaterialType.Gas;

    public ItemMaterial(Material material, MaterialType type) : base(
        Identifier.Create($"{material.Name}_{type}"),
        material.FormNames[type],
        material.GetDescription().ToString())
    {
        this.Material = material;
        this.Type     = type;
    }
}