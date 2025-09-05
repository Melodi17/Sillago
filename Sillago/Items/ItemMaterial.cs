namespace Sillago.Items;

using Materials;
using Recipes;
using Utils;

public class ItemMaterial : Item
{
    public Material     Material;
    public MaterialType Type;

    public override bool CountAsVolume => this.Type is MaterialType.Liquid or MaterialType.Gas;

    public ItemMaterial(Material material, MaterialType type)
    {
        this.Material = material;
        this.Type     = type;

        this.Name        = material.FormNames[type];
        this.Id          = Utils.Identifier.Create($"{material.Name}_{type}");
        this.Description = material.GetDescription().ToString();
    }

    public void SmeltsInto(ItemMaterial result, float temperature, int inputQuantity = 1, int outputQuantity = 1)
    {
        RecipeBuilder builder = new RecipeBuilder($"{this.Id}_smelting_into_{result.Id}", RecipeType.Smelting)
            .Name($"Smelting {this.Name} into {result.Name}")
            .AddInput(this, inputQuantity)
            .AddOutput(result, outputQuantity)
            .AddRequirement(new TemperatureRequirement(temperature))
            .SetDuration(this.Material.Density * TimeSpan.FromMilliseconds(5));
        
        Recipes.Register(builder.Build());
    }
    
    public void FusesFrom(List<ItemStack> ingredients, float temperature, int outputQuantity = 1)
    {
        if (ingredients.Count == 0)
            throw new ArgumentException("Cannot fuse from an empty ingredient list.");
        
        RecipeBuilder builder = new RecipeBuilder($"{this.Id}_fusing_from_{string.Join("_", ingredients.Select(i => i.Item.Id))}", RecipeType.Fusing)
            .Name($"Fusing {string.Join(", ", ingredients.Select(i => i.Item.Name))} into {this.Name}")
            .AddInputs(ingredients)
            .AddOutput(this, outputQuantity)
            .AddRequirement(new TemperatureRequirement(temperature))
            .SetDuration(ingredients.Count * TimeSpan.FromSeconds(0.5));
        
        Recipes.Register(builder.Build());
    }
    
    public void MixesFrom(List<ItemStack> ingredients, int outputQuantity = 1, TemperatureRequirement? temperatureRequirement = null)
    {
        if (ingredients.Count == 0)
            throw new ArgumentException("Cannot mix from an empty ingredient list.");

        RecipeBuilder builder = new RecipeBuilder(
            $"{this.Id}_mixing_from_{string.Join("_", ingredients.Select(i => i.Item.Id))}",
            RecipeType.Mixing)
            .Name($"Mixing {string.Join(", ", ingredients.Select(i => i.Item.Name))} into {this.Name}")
            .AddInputs(ingredients)
            .AddOutput(this, outputQuantity)
            .SetDuration(ingredients.Count * TimeSpan.FromSeconds(0.5));
        
        if (temperatureRequirement != null)
            builder.AddRequirement(temperatureRequirement);
        
        Recipes.Register(builder.Build());
    }
    public void PressesInto(ItemMaterial pressedForm, int inputQuantity = 1, int outputQuantity = 1)
    {
        RecipeBuilder builder = new RecipeBuilder($"{this.Id}_pressing_into_{pressedForm.Id}", RecipeType.Pressing)
            .Name($"Pressing {this.Name} into {pressedForm.Name}")
            .AddInput(this, inputQuantity)
            .AddOutput(pressedForm, outputQuantity)
            .SetDuration(this.Material.Density * TimeSpan.FromMilliseconds(10));
        
        Recipes.Register(builder.Build());
    }
    public void LathesInto(ItemMaterial lathedForm, int inputQuantity = 1, int outputQuantity = 1)
    {
        RecipeBuilder builder = new RecipeBuilder($"{this.Id}_lathing_into_{lathedForm.Id}", RecipeType.Lathing)
            .Name($"Lathing {this.Name} into {lathedForm.Name}")
            .AddInput(this, inputQuantity)
            .AddOutput(lathedForm, outputQuantity)
            .SetDuration(this.Material.Density * TimeSpan.FromMilliseconds(3));
        
        Recipes.Register(builder.Build());
    }
}