namespace Sillago.Types;

using System.Collections;
using Requirements;
using Symbols;

public class FluidMaterial : Material
{
    public float FreezingPoint;
    public float VaporisationPoint;

    public FluidMaterial(string name, int color, VisualSet visualSet, Symbol symbol, MaterialFlags flags, float density, float freezingPoint, float vaporisationPoint)
        : this(name, symbol, freezingPoint, vaporisationPoint)
    {
        this.Color     = color;
        this.VisualSet = visualSet;
        this.Flags     = flags;
        this.Density   = density;

        this.FreezingPoint     = freezingPoint;
        this.VaporisationPoint = vaporisationPoint;
    }
    
    protected FluidMaterial(string name, Symbol symbol, float freezingPoint = 0f, float vaporisationPoint = 100f)
        : base(name, symbol)
    {
        if (FluidMaterial.IsAboveRoomTemperature(vaporisationPoint))
            // Liquids that boil above room temperature are usually called liquids
            this.OverrideFormName(MaterialType.Liquid, $"{name}");
        else
            // Fluids that boil below room temperature are usually called gases
            this.OverrideFormName(MaterialType.Gas, $"{name}");
    }
    
    public override IEnumerator Generate()
    {
        ItemMaterial liquid = new ItemMaterial(this, MaterialType.Liquid);
        ItemMaterial ice = new ItemMaterial(this, MaterialType.Ice);
        ItemMaterial gas = new ItemMaterial(this, MaterialType.Gas);
        
        yield return liquid;
        yield return ice;
        yield return gas;
        
        // liquid -> ice
        yield return new RecipeBuilder(RecipeType.Freezing)
            .NamePatterned($"<input> <verb>")
            .AddInput(liquid.Stack(250))
            .AddOutput(ice.Stack(250))
            .SetDuration(TimeSpan.FromSeconds(5))
            .AddRequirement(TemperatureRequirement.Below(this.FreezingPoint))
            .Build();
        
        // ice -> liquid
        yield return new RecipeBuilder(RecipeType.Thawing)
            .NamePatterned($"<input> <verb>")
            .AddInput(ice.Stack(250))
            .AddOutput(liquid.Stack(250))
            .SetDuration(TimeSpan.FromSeconds(5))
            .AddRequirement(TemperatureRequirement.Above(this.FreezingPoint))
            .Build();
        
        // liquid -> gas
        yield return new RecipeBuilder(RecipeType.Vaporising)
            .NamePatterned($"<input> <verb>")
            .AddInput(liquid.Stack(250))
            .AddOutput(gas.Stack(250))
            .SetDuration(TimeSpan.FromSeconds(5))
            .AddRequirement(TemperatureRequirement.Above(this.VaporisationPoint))
            .Build();
        
        // gas -> liquid
        yield return new RecipeBuilder(RecipeType.Condensing)
            .NamePatterned($"<input> <verb>")
            .AddInput(gas.Stack(250))
            .AddOutput(liquid.Stack(250))
            .SetDuration(TimeSpan.FromSeconds(5))
            .AddRequirement(TemperatureRequirement.Below(this.VaporisationPoint))
            .Build();
    }
    
    private static bool IsAboveRoomTemperature(float temperature)
        => temperature > 20f;
}