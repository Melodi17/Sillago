namespace Sillago
{
    using System;
    using Utils;

    public class Chips
    {
        public static Item SiliconWafer = new Item(
            "silicon_wafer",
            "Silicon Wafer",
            "A thin slice of silicon used in electronics manufacturing.");
        public static Item SiliconWaferRCA1 = ProcessableItem.Adding(Chips.SiliconWafer, "RCA-1 Cleaned");
        public static Item SiliconWaferRCA2 = ProcessableItem.Adding(Chips.SiliconWaferRCA1, "RCA-2 Cleaned");
        public static Item SiliconWaferOxidized = ProcessableItem.Adding(Chips.SiliconWaferRCA2, "Thermal Oxidation");

        public static void GenerateItemsAndRecipes()
        {
            var wafers = Registry.GetAllEntries<Item, Chips>();
            foreach (var wafer in wafers)
                Items.Register(wafer);
            
            // Ammonium hydroxide (NH4OH) + Hydrogen Peroxide (H2O2)
            new RecipeBuilder(RecipeType.ChemicalBathing)
                .Name("RCA-1 Clean")
                .AddInput(Chips.SiliconWafer.Stack(1))
                .AddInput(Items.GetMaterialForm(Materials.AmmoniumHydroxide, MaterialType.Liquid).Stack(100))
                .AddInput(Items.GetMaterialForm(Materials.HydrogenPeroxide, MaterialType.Liquid).Stack(100))
                .AddOutput(Chips.SiliconWaferRCA1.Stack(1))
                .SetDuration(TimeSpan.FromMinutes(10))
                .BuildAndRegister();
            
            // Hydrogen Chloride (HCl) + Hydrogen Peroxide (H2O2) 
            new RecipeBuilder(RecipeType.ChemicalBathing)
                .Name("RCA-2 Clean")
                .AddInput(Chips.SiliconWaferRCA1.Stack(1))
                .AddInput(Items.GetMaterialForm(Materials.HydrogenChloride, MaterialType.Liquid).Stack(100))
                .AddInput(Items.GetMaterialForm(Materials.HydrogenPeroxide, MaterialType.Liquid).Stack(100))
                .AddOutput(Chips.SiliconWaferRCA2.Stack(1))
                .SetDuration(TimeSpan.FromMinutes(10))
                .BuildAndRegister();
            
            // Thermal Oxidation
            new RecipeBuilder(RecipeType.ThermalOxidation)
                .Name("Silicon Wafer Thermal Oxidization")
                .AddInput(Chips.SiliconWaferRCA2.Stack(1))
                .AddOutput(Chips.SiliconWaferOxidized.Stack(1))
                .SetDuration(TimeSpan.FromMinutes(1))
                .BuildAndRegister();
            
            // Example chip: n-type silicon with phosphorus doping using DUV lithography
            var nTypeChip = new ChipBuilder("n_type")
                .WithDopant(Materials.PhosphorylChloride, 10)
                .WithExposure(LightType.DUV, 100.0f, 193)
                .BuildAndRegister();
        }
    }
}