namespace Sillago.Items;

using Materials;
using Recipes;
using Utils;

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

    public void SmeltsInto(ItemMaterial result, float temperature, int inputQuantity = 1, int outputQuantity = 1)
    {
        RecipeBuilder builder = new RecipeBuilder($"{this.Id}_smelting_into_{result.Id}", RecipeType.Smelting)
            .Name($"Smelting {this.Name} into {result.Name}")
            .AddInput(this, inputQuantity)
            .AddOutput(result, outputQuantity)
            .AddRequirement(new TemperatureRequirement(temperature))
            .SetDuration(TimeSpan.FromSeconds(this.Material.Density / 2500));
        
        Recipes.Register(builder.Build());
    }

    public void ArcSmeltsInto(ItemMaterial result, float temperature)
    {
        RecipeBuilder builder = new RecipeBuilder($"{this.Id}_arc_smelting_into_{result.Id}", RecipeType.ArcSmelting)
            .Name($"Arc Smelting {this.Name} into {result.Name}")
            .AddInput(this, 1)
            .AddOutput(result, 1)
            .AddRequirement(new TemperatureRequirement(temperature))
            .SetDuration(TimeSpan.FromSeconds(this.Material.Density / 1000));
        
        Recipes.Register(builder.Build());
    }
    
    public void FusesFrom(List<ItemStack> ingredients, float temperature, int outputQuantity = 1)
    {
        if (ingredients.Count == 0)
            throw new ArgumentException("Cannot fuse from an empty ingredient list.");
        
        RecipeBuilder builder = new RecipeBuilder($"{this.Id}_fusing_from_{string.Join("_", ingredients.Select(i => i.Item.Id))}", RecipeType.Fusing)
            .Name($"Fusing {string.Join(", ", ingredients.Select(i => i.Item.Name))} into {this.Name}")
            .AddInputs(ingredients.Select(x=>new RecipeIngredient([x])).ToList())
            .AddOutput(this, outputQuantity)
            .AddRequirement(new TemperatureRequirement(temperature))
            .SetDuration(ingredients.Count * TimeSpan.FromSeconds(0.5) + TimeSpan.FromSeconds(this.Material.Density / 1200));
        
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
            .AddInputs(ingredients.Select(x=>new RecipeIngredient([x])).ToList())
            .AddOutput(this, outputQuantity)
            .SetDuration(ingredients.Count * TimeSpan.FromSeconds(0.5) + TimeSpan.FromSeconds(this.Material.Density / 2000));
        
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
            .SetDuration(TimeSpan.FromSeconds(this.Material.Density / 4000));
        
        Recipes.Register(builder.Build());
    }
    public void LathesInto(ItemMaterial lathedForm, int inputQuantity = 1, int outputQuantity = 1)
    {
        RecipeBuilder builder = new RecipeBuilder($"{this.Id}_lathing_into_{lathedForm.Id}", RecipeType.Lathing)
            .Name($"Lathing {this.Name} into {lathedForm.Name}")
            .AddInput(this, inputQuantity)
            .AddOutput(lathedForm, outputQuantity)
            .SetDuration(TimeSpan.FromSeconds(this.Material.Density / 6000));
        
        Recipes.Register(builder.Build());
    }
    public void FreezesInto(ItemMaterial result, float freezingPoint)
    {
        RecipeBuilder builder = new RecipeBuilder($"{this.Id}_freezing_into_{result.Id}", RecipeType.Freezing)
            .Name($"Freezing {this.Name} into {result.Name}")
            .AddInput(this, 1)
            .AddOutput(result, 1)
            .AddRequirement(new TemperatureRequirement(atMax: freezingPoint))
            .SetDuration(TimeSpan.FromSeconds(this.Material.Density / 5000) * 5);
        
        Recipes.Register(builder.Build());
    }
    public void ThawsInto(ItemMaterial result, float freezingPoint)
    {
        RecipeBuilder builder = new RecipeBuilder($"{this.Id}_thawing_into_{result.Id}", RecipeType.Thawing)
            .Name($"Thawing {this.Name} into {result.Name}")
            .AddInput(this, 1)
            .AddOutput(result, 1)
            .AddRequirement(new TemperatureRequirement(atLeast: freezingPoint))
            .SetDuration(TimeSpan.FromSeconds(this.Material.Density / 5000) * 7);
        
        Recipes.Register(builder.Build());
    }
    public void VaporisesInto(ItemMaterial result, float vaporisationPoint)
    {
        RecipeBuilder builder = new RecipeBuilder($"{this.Id}_vaporising_into_{result.Id}", RecipeType.Vaporising)
            .Name($"Vaporising {this.Name} into {result.Name}")
            .AddInput(this, 1)
            .AddOutput(result, 1)
            .AddRequirement(new TemperatureRequirement(atLeast: vaporisationPoint))
            .SetDuration(TimeSpan.FromSeconds(this.Material.Density / 3000) * 10);
        
        Recipes.Register(builder.Build());
    }
    
    public void CondensesInto(ItemMaterial result, float vaporisationPoint)
    {
        RecipeBuilder builder = new RecipeBuilder($"{this.Id}_condensing_into_{result.Id}", RecipeType.Condensing)
            .Name($"Condensing {this.Name} into {result.Name}")
            .AddInput(this, 1)
            .AddOutput(result, 1)
            .AddRequirement(new TemperatureRequirement(atMax: vaporisationPoint))
            .SetDuration(TimeSpan.FromSeconds(this.Material.Density / 3000) * 8);
        
        Recipes.Register(builder.Build());
    }
    public void Incubates(float minTemp, float maxTemp, Material feedsOn)
    {
        RecipeBuilder builder = new RecipeBuilder($"{this.Id}_incubating_on_{feedsOn.Name}", RecipeType.Incubating)
            .Name($"Incubating {this.Name} on {feedsOn.Name}")
            .AddInput(this, 1)
            .AddInput(new ItemMaterial(feedsOn, MaterialType.Liquid), 1)
            .AddOutput(this, 5)
            .AddRequirement(new TemperatureRequirement(atLeast: minTemp, atMax: maxTemp))
            .SetDuration(TimeSpan.FromMinutes(30));
        
        Recipes.Register(builder.Build());
    }
    public void MaceratesInto(List<ItemStack> outputs)
    {
        if (outputs.Count == 0)
            throw new ArgumentException("Cannot macerate into an empty output list.");

        RecipeBuilder builder = new RecipeBuilder(
            $"{this.Id}_macerating_into_{string.Join("_", outputs.Select(o => o.Item.Id))}",
            RecipeType.Macerating)
            .Name($"Macerating {this.Name} into {string.Join(", ", outputs.Select(o => o.Item.Name))}")
            .AddInput(this, 1)
            .AddOutputs(outputs.Select(x=>new RecipeResult(x)).ToList())
            .SetDuration(TimeSpan.FromSeconds(this.Material.Density / 2000) + TimeSpan.FromSeconds(5));
        
        Recipes.Register(builder.Build());
    }
    public void DistillsInto( int inputQuantity, List<ItemStack> results, TemperatureRequirement? temperatureRequirement = null)
    {
        RecipeBuilder builder = new RecipeBuilder(
                $"{this.Id}_distilling_into_{string.Join("_", results.Select(r => r.Item.Id))}",
                RecipeType.Distilling)
            .Name($"Distilling {this.Name} into {string.Join(", ", results.Select(r => r.Item.Name))}")
            .AddInput(this, inputQuantity)
            .AddOutputs(results.Select(x=>new RecipeResult(x)).ToList())
            .SetDuration(TimeSpan.FromSeconds(this.Material.Density / 2500) * inputQuantity);
        
        if (temperatureRequirement != null)
            builder.AddRequirement(temperatureRequirement);
        
        Recipes.Register(builder.Build());
    }
}