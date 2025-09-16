namespace Sillago.Types;

using System.Collections;
using Requirements;
using Symbols;

public class IngotMaterial : Material
{
    public float MeltingPoint;

    public IngotMaterial(
        string name,
        int color,
        VisualSet visualSet,
        Symbol symbol,
        MaterialFlags flags,
        float density,
        float meltingPoint) : this(name, symbol)
    {
        this.Color = color;
        this.VisualSet = visualSet;
        this.Flags = flags;
        this.Density = density;

        this.MeltingPoint = meltingPoint;
    }
    
    protected IngotMaterial(string name, Symbol symbol) : base(name, symbol)
    {
    }
    
    public override IEnumerator Generate()
    {
        ItemMaterial ingot = new ItemMaterial(this, MaterialType.Ingot);
        ItemMaterial finePowder = new ItemMaterial(this, MaterialType.FinePowder);
        ItemMaterial powder = new ItemMaterial(this, MaterialType.Powder);
        ItemMaterial rod = new ItemMaterial(this, MaterialType.Rod);
        ItemMaterial nugget = new ItemMaterial(this, MaterialType.Nugget);
        ItemMaterial fluid = new ItemMaterial(this, MaterialType.Liquid);

        yield return ingot;
        yield return finePowder;
        yield return powder;
        yield return rod;
        yield return nugget;
        yield return fluid;

        yield return new RecipeBuilder(RecipeType.Compacting)
            .NamePatterned($"<input> <verb>")
            .AddInput(finePowder.Stack(5))
            .AddOutput(powder)
            .SetDuration(TimeSpan.FromSeconds(0.2))
            .Build();

        yield return new RecipeBuilder(RecipeType.Compacting)
            .NamePatterned($"<input> <verb>")
            .AddInput(nugget.Stack(5))
            .AddOutput(ingot)
            .SetDuration(TimeSpan.FromSeconds(0.5))
            .Build();

        yield return new RecipeBuilder(RecipeType.Smelting)
            .NamePatterned("<input> <verb>")
            .AddInput(powder.Stack(1))
            .AddOutput(ingot)
            .SetDuration(TimeSpan.FromSeconds(0.5))
            .AddRequirement(TemperatureRequirement.Above(this.MeltingPoint * 0.9f))
            .Build();

        yield return new RecipeBuilder(RecipeType.Lathing)
            .NamePatterned($"<input> <verb>")
            .AddInput(ingot.Stack(1))
            .AddOutput(rod.Stack(2))
            .SetDuration(TimeSpan.FromSeconds(1))
            .Build();

        if (!this.Is(MaterialFlags.Brittle))
        {
            ItemMaterial plate = new ItemMaterial(this, MaterialType.Plate);
            yield return plate;
            
            yield return new RecipeBuilder(RecipeType.Pressing)
                .NamePatterned($"<input> <verb>")
                .AddInput(ingot.Stack(1))
                .AddOutput(plate.Stack(1))
                .SetDuration(TimeSpan.FromSeconds(1))
                .Build();
        }

        if (this.Is(MaterialFlags.Ductile))
        {
            var coil = new ItemMaterial(this, MaterialType.Coil);

            yield return coil;
            
            yield return new RecipeBuilder(RecipeType.Wiremilling)
                .NamePatterned($"<input> <verb>")
                .AddInput(rod.Stack(2))
                .AddOutput(coil.Stack(1))
                .SetDuration(TimeSpan.FromSeconds(1))
                .Build();
        }

        if (this.Is(MaterialFlags.ElectricallyConductive))
        {
            var wire = new ItemMaterial(this, MaterialType.Wire);
            var fineWire = new ItemMaterial(this, MaterialType.FineWire);
            var cable = new ItemMaterial(this, MaterialType.Cable);
            
            yield return wire;
            yield return fineWire;
            yield return cable;

            // Rod -> wire
            yield return new RecipeBuilder(RecipeType.Wiremilling)
                .NamePatterned($"<input> <verb>")
                .AddInput(rod.Stack(1))
                .AddOutput(wire.Stack(2))
                .SetDuration(TimeSpan.FromSeconds(1))
                .Build();

            // Wire -> fine wire
            yield return new RecipeBuilder(RecipeType.Wiremilling)
                .NamePatterned($"<input> <verb>")
                .AddInput(wire.Stack(1))
                .AddOutput(fineWire.Stack(2))
                .SetDuration(TimeSpan.FromSeconds(1))
                .Build();
            
            // Direct rod -> fine wire
            yield return new RecipeBuilder(RecipeType.Wiremilling)
                .NamePatterned($"<input> <verb>")
                .AddInput(rod.Stack(1))
                .AddOutput(fineWire.Stack(4))
                .SetDuration(TimeSpan.FromSeconds(2))
                .Build();

            yield return this.Deferred(() =>
            {
                var coating =
                    RecipeIngredient.Of(
                        Items.GetMaterialForm(Materials.Rubber, MaterialType.Liquid).Stack(50));

                // Fine wire + rubber -> cable
                new RecipeBuilder(RecipeType.ChemicalBathing)
                    .NamePatterned($"<input> <verb>")
                    .AddInput(wire.Stack(1))
                    .AddInput(coating)
                    .AddOutput(cable.Stack(1))
                    .SetDuration(TimeSpan.FromSeconds(5))
                    .BuildAndRegister();
            });
        }

        if (this is not MetalMaterial)
        {
            yield return new RecipeBuilder(RecipeType.Smelting)
                .NamePatterned("<input> <verb>")
                .AddInput(ingot.Stack(1))
                .AddOutput(fluid.Stack(250))
                .SetDuration(TimeSpan.FromSeconds(2))
                .AddRequirement(TemperatureRequirement.Above(this.MeltingPoint))
                .Build();
            
            yield return new RecipeBuilder(RecipeType.Casting)
                .NamePatterned("<input> <verb>")
                .AddInput(fluid.Stack(250))
                .AddOutput(ingot.Stack(1))
                .SetDuration(TimeSpan.FromSeconds(2))
                .AddRequirement(MoldRequirement.Of(MaterialType.Ingot))
                .Build();
        }
    }
}