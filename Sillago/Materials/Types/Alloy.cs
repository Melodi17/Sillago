using System.Collections;
using System.Text;

namespace Sillago.Materials.Types;

using Items;
using Recipes;
using Utils;

public class Alloy : MetalMaterial
{
    public AlloyComponent[] Components;
    public Alloy(string name, params AlloyComponent[] components)
        : base(name)
    {
        this.Components = components;
        this.Name = name;
        this.Symbol = new Compound(
            name,
            components.Select(x => new CompoundComponent(x.Value.Symbol, x.Amount)).ToArray());

        // split int color into ARGB components, then perform weighted sum
        this.Color = components.WeightedColorwiseSum(x => x.Amount, x => x.Value.Color);
        this.VisualSet = components.MaxBy(x => x.Amount)!.Value.VisualSet;

        this.Flags = Alloy.CalculateFlags(components);

        this.MeltingPoint = components
            .Where(x => x.Value is IngotMaterial)
            .WeightedSum(x => x.Amount, x => (x.Value as IngotMaterial)!.MeltingPoint);
        this.Density = components.WeightedSum(x => x.Amount, x => x.Value.Density);
    }

    private static MaterialFlags CalculateFlags(AlloyComponent[] components)
    {
        MaterialFlags flags = MaterialFlags.None;
        float totalAmount = components.Sum(x => x.Amount);
        foreach (AlloyComponent component in components)
        {
            MaterialFlags componentFlags = component.Value.Flags;
            bool keepRecessiveFlags = component.Amount / totalAmount > 0.5f;
            if (!keepRecessiveFlags)
                componentFlags &= ~MaterialFlags.Recessive;
            componentFlags &= ~MaterialFlags.Ephemeral;

            flags |= componentFlags;
        }

        if (flags.Is(MaterialFlags.ElectricallyConductive)
            && flags.Is(MaterialFlags.ElectricallyInsulating))
        {
            // If an alloy is both conductive and insulating, it is neither
            flags &= ~(MaterialFlags.ElectricallyConductive | MaterialFlags.ElectricallyInsulating);
        }

        if (flags.Is(MaterialFlags.ThermallyConductive)
            && flags.Is(MaterialFlags.ThermallyInsulating))
        {
            // If an alloy is both thermally conductive and thermally insulating, it is neither
            flags &= ~(MaterialFlags.ThermallyConductive | MaterialFlags.ThermallyInsulating);
        }

        if (flags.Is(MaterialFlags.Brittle) && flags.Is(MaterialFlags.Ductile))
        {
            // If an alloy is both brittle and ductile, it is neither
            flags &= ~(MaterialFlags.Brittle | MaterialFlags.Ductile);
        }

        return flags;
    }

    public override StringBuilder GetDescription()
    {
        StringBuilder sb = base.GetDescription();
        sb.AppendLine("Alloy Components:");
        float totalAmount = this.Components.Sum(x => x.Amount);
        foreach (AlloyComponent component in this.Components.OrderByDescending(x => x.Amount))
        {
            float percentage = component.Amount / totalAmount * 100;
            sb.AppendLine($"  - {component.Value.Name} {percentage:0.0}%");
        }
        return sb;
    }

    public override IEnumerator Generate()
    {
        yield return base.Generate();

        yield return this.Deferred(() =>
        {
            var ingot = Items.GetMaterialForm(this, MaterialType.Ingot);
            var powder = Items.GetMaterialForm(this, MaterialType.Powder);
            var moltenAlloy = Items.GetMaterialForm(this, MaterialType.Liquid);
            
            RecipeIngredient fusingGas = RecipeIngredient.AnyOf([
                // Items.GetMaterialForm(Materials.Argon, MaterialType.Gas).Stack(1),
                // Items.GetMaterialForm(Materials.Helium, MaterialType.Gas).Stack(5),
                Items.GetMaterialForm(Materials.Nitrogen, MaterialType.Gas).Stack(10),
                Items.GetMaterialForm(Materials.Hydrogen, MaterialType.Gas).Stack(20),
            ]);

            var mixingRecipeBuilder = new RecipeBuilder(RecipeType.Mixing)
                .NamePatterned($"<output> alloy <verb>")
                .AddOutput(powder.Stack(this.Components.Sum(x => x.Amount)))

                // Assume mixing takes 1 second per 30,000 kg/m³ of material processed
                .SetDuration(TimeSpan.FromSeconds(this.Components.Sum(x => x.Value.Density * x.Amount) / 30_000));

            foreach (AlloyComponent component in this.Components)
                mixingRecipeBuilder.AddInput(
                    Items.GetMaterialForm(component.Value, MaterialType.Powder).Stack(component.Amount));

            mixingRecipeBuilder.BuildAndRegister();

            var dustFusingRecipeBuilder = new RecipeBuilder(RecipeType.Fusing)
                .NamePatterned("<output> direct dust <verb>")
                .AddOutput(ingot.Stack(this.Components.Sum(x => x.Amount)))

                // Assume arc fusing takes 2 seconds per 10,000 kg/m³ of material processed
                .SetDuration(TimeSpan.FromSeconds((this.Components.Sum(x => x.Value.Density * x.Amount) / 10_000) * 2))
                .AddRequirement(TemperatureRequirement.Above(this.MeltingPoint));

            foreach (AlloyComponent component in this.Components)
                dustFusingRecipeBuilder.AddInput(
                    Items.GetMaterialForm(component.Value, MaterialType.Powder).Stack(component.Amount));

            dustFusingRecipeBuilder.BuildAndRegister();

            var ingotSmeltingRecipeBuilder = new RecipeBuilder(RecipeType.ArcFusing)
                .NamePatterned("<output> parallel ingot <verb>")
                .AddOutput(ingot.Stack(this.Components.Sum(x => x.Amount)))

                // Assume smelting takes 1 second per 10,000 kg/m³ of material processed
                .SetDuration(TimeSpan.FromSeconds(this.Components.Sum(x => x.Value.Density * x.Amount) / 10_000))
                
                // Require 5 units of fusing gas per ingot produced
                .AddInput(fusingGas * (5 * this.Components.Sum(x => x.Amount)))
                .AddRequirement(TemperatureRequirement.Above(this.MeltingPoint * 0.9f));
            
            foreach (AlloyComponent component in this.Components)
            {
                ItemMaterial form = Items.TryGetMaterialForm(component.Value, MaterialType.Ingot) ??
                                   Items.GetMaterialForm(component.Value, MaterialType.Powder);
                
                ingotSmeltingRecipeBuilder.AddInput(
                    form.Stack(component.Amount));
            }
            
            ingotSmeltingRecipeBuilder.BuildAndRegister();
            
            var moltenAlloyRecipeBuilder = new RecipeBuilder(RecipeType.AlloyReacting)
                .NamePatterned("<output> fusion <verb>")
                .AddOutput(moltenAlloy.Stack(this.Components.Sum(x => x.Amount) * 250))

                // Assume melting takes 1 second per 2000 kg/m³ of material processed
                .SetDuration(TimeSpan.FromSeconds(this.Components.Sum(x => x.Value.Density * x.Amount) / 2000))
                .AddRequirement(TemperatureRequirement.Above(this.MeltingPoint))
                .AddInput(fusingGas * (2 * this.Components.Sum(x => x.Amount)));
            
            foreach (AlloyComponent component in this.Components)
                moltenAlloyRecipeBuilder.AddInput(
                    Items.GetMaterialForm(component.Value, MaterialType.Liquid).Stack(component.Amount * 250));
            
            moltenAlloyRecipeBuilder.BuildAndRegister();
        });
    }
}