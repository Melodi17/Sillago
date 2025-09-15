namespace Sillago.Materials;

using Items;
using Recipes;
using Types;

public partial class Materials
{
    internal static void GenerateLinkingRecipes()
    {
        var waterFluid = Items.GetMaterialForm(Materials.Water, MaterialType.Liquid);
        var distilledFluid = Items.GetMaterialForm(Materials.DistilledWater, MaterialType.Liquid);
        
        // Water -> Distilled Water
        new RecipeBuilder(RecipeType.Distilling)
            .NamePatterned($"<input> <verb>")
            .AddInput(waterFluid.Stack(250))
            .AddOutput(distilledFluid.Stack(200))
            .SetDuration(TimeSpan.FromSeconds(2))
            // .AddRequirement(HasRequirement.Filter)
            .BuildAndRegister();
    }
}