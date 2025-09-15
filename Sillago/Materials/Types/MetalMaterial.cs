using System.Collections;

namespace Sillago.Materials.Types;

using Items;
using Recipes;

public class MetalMaterial : IngotMaterial
{
    private const float MOLTEN_FREEZING_TEMPERATURE = 20.0f;
    
    public MetalMaterial(
        string name, int color, VisualSet visualSet, Element symbol, MaterialFlags flags, float density, float meltingPoint)
        : base(
            name,
            color,
            visualSet,
            symbol,
            flags,
            density,
            meltingPoint)
    {
        this.OverrideFormName(MaterialType.Liquid, $"Molten {name}");
    }

    protected MetalMaterial(string name) : base(name)
    {
        this.OverrideFormName(MaterialType.Liquid, $"Molten {name}");
    }

    public override IEnumerator Generate()
    {
        yield return base.Generate();
        
        ItemMaterial hotIngot = new ItemMaterial(this, MaterialType.HotIngot);
        yield return hotIngot;
        
        yield return Deferred(() =>
        {
            ItemMaterial ingot = Items.GetMaterialForm(this, MaterialType.Ingot);
            ItemMaterial molten = Items.GetMaterialForm(this, MaterialType.Liquid);
            
            new RecipeBuilder(RecipeType.ArcSmelting)
                .NamePatterned("<input> <verb>")
                .AddInput(ingot.Stack(1))
                .AddOutput(molten.Stack(250))
                .SetDuration(TimeSpan.FromSeconds(5))
                .AddRequirement(TemperatureRequirement.Above(this.MeltingPoint))
                .BuildAndRegister(); 
        
            new RecipeBuilder(RecipeType.Casting)
                .NamePatterned("<input> <verb>")
                .AddInput(molten.Stack(250))
                .AddOutput(hotIngot.Stack(1))
                .SetDuration(TimeSpan.FromSeconds(2))
                .AddRequirement(MoldRequirement.Of(MaterialType.Ingot))
                .BuildAndRegister();
            
            new RecipeBuilder(RecipeType.BlastFreezing)
                .NamePatterned("<input> <verb>")
                .AddInput(hotIngot.Stack())
                .AddOutput(ingot)
                .SetDuration(TimeSpan.FromSeconds(5))
                .AddRequirement(TemperatureRequirement.Below(MOLTEN_FREEZING_TEMPERATURE))
                .BuildAndRegister();
        });
    }
}