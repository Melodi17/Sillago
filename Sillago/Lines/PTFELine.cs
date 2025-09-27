namespace Sillago
{
    using System;
    using Requirements;

    public class PTFELine
    {
        public static void Generate()
        {
            var hydrogen = Items.GetMaterialForm(Materials.Hydrogen, MaterialType.Gas);
            var fluoride = Items.GetMaterialForm(Materials.Fluoride, MaterialType.Powder);
            var hydrogenFluoride = Items.GetMaterialForm(Materials.HydrogenFluoride, MaterialType.Gas);
            
            var methane = Items.GetMaterialForm(Materials.Methane, MaterialType.Gas);
            var chlorine = Items.GetMaterialForm(Materials.Chlorine, MaterialType.Gas);
            var trichloromethane = Items.GetMaterialForm(Materials.Trichloromethane, MaterialType.Liquid);
            
            var chlorotrifluoromethane = Items.GetMaterialForm(Materials.Chlorotrifluoromethane, MaterialType.Gas);
            var tetrafluoroethylene = Items.GetMaterialForm(Materials.Tetrafluoroethylene, MaterialType.Gas);
            var ptfe = Items.GetMaterialForm(Materials.Polytetrafluoroethylene, MaterialType.Powder);
            
            // Hydrogen + Fluorine -> Hydrogen Fluoride
            new RecipeBuilder(RecipeType.Reacting)
                .NamePatterned($"<output> <verb> from <inputs>")
                .AddInput(hydrogen.Stack(25))
                .AddInput(fluoride.Stack(1))
                .AddOutput(hydrogenFluoride.Stack(25))
                .SetDuration(TimeSpan.FromSeconds(1))
                .BuildAndRegister();
            
            // Methane 25ml + Chlorine 75ml -> Trichloromethane 100ml
            new RecipeBuilder(RecipeType.Reacting)
                .NamePatterned($"<output> <verb> from <inputs>")
                .AddInput(methane.Stack(25))
                .AddInput(chlorine.Stack(75))
                .AddOutput(trichloromethane.Stack(100))
                .SetDuration(TimeSpan.FromSeconds(1))
                .BuildAndRegister();
            
            // Hydrogen Fluoride 25ml + Trichloromethane 75ml -> Hydrogen Chloride 25ml + Chlorotrifluoromethane 75ml
            new RecipeBuilder(RecipeType.Reacting)
                .NamePatterned($"<inputs> <verb> to form <outputs>")
                .AddInput(hydrogenFluoride.Stack(25))
                .AddInput(trichloromethane.Stack(75))
                .AddOutput(chlorotrifluoromethane.Stack(25))
                .AddOutput(chlorotrifluoromethane.Stack(75))
                .SetDuration(TimeSpan.FromSeconds(1))
                .BuildAndRegister();
            
            // Pyrolysis of Chlorotrifluoromethane to produce Tetrafluoroethylene
            new RecipeBuilder(RecipeType.Pyrolysis)
                .NamePatterned($"<input> <verb> to form <outputs>")
                .AddInput(chlorotrifluoromethane.Stack(25))
                .AddOutput(tetrafluoroethylene.Stack(25))
                .SetDuration(TimeSpan.FromSeconds(5))
                .AddRequirement(TemperatureRequirement.Above(550))
                .BuildAndRegister();
            
            // Polymerization of Tetrafluoroethylene to produce PTFE
            new RecipeBuilder(RecipeType.Polymerization)
                .NamePatterned($"<input> <verb> to form <output>")
                .AddInput(tetrafluoroethylene.Stack(250))
                .AddOutput(ptfe.Stack(1))
                .SetDuration(TimeSpan.FromSeconds(10))
                .BuildAndRegister();
        }
    }
}