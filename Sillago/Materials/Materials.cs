namespace Sillago.Materials;

using Items;
using Types;

public partial class Materials
{
    internal static void GenerateLinkingRecipes()
    {
        var waterFluid = Items.GetMaterial(Materials.Water, MaterialType.Liquid);
        var distilledFluid = Items.GetMaterial(Materials.DistilledWater, MaterialType.Liquid);
        
        waterFluid.DistillsInto(100, [distilledFluid.Stack(90)]);
    }
}