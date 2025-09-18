namespace Sillago
{
    using System;

    public class SulfuricAcidLine
    {
        public static void Generate()
        {
            var sulfur = Items.GetMaterialForm(Materials.Sulfur, MaterialType.Powder);
            var water = Items.GetMaterialForm(Materials.Water, MaterialType.Liquid);
            var oleum = Items.GetMaterialForm(Materials.Oleum, MaterialType.Liquid);
            var sulfuricAcid = Items.GetMaterialForm(Materials.SulfuricAcid, MaterialType.Liquid);
            
            var sulfurTrioxide = Items.GetMaterialForm(Materials.SulfurTrioxide, MaterialType.Gas);
            var sulfurDioxide = Items.GetMaterialForm(Materials.SulfurDioxide, MaterialType.Gas);
            var steam = Items.GetMaterialForm(Materials.Water, MaterialType.Gas);
            var oxygen = Items.GetMaterialForm(Materials.Oxygen, MaterialType.Gas);
            var hydrogen = Items.GetMaterialForm(Materials.Hydrogen, MaterialType.Gas);
            
            // Sulfur x1 + Oxygen 25ml -> Sulfur Dioxide 25ml
            new RecipeBuilder(RecipeType.Reacting)
                .NamePatterned($"<inputs> <verb>")
                .AddInput(sulfur.Stack(1))
                .AddInput(oxygen.Stack(25))
                .AddOutput(sulfurDioxide.Stack(25))
                .SetDuration(TimeSpan.FromSeconds(1))
                .BuildAndRegister();
                
            
            // Sulfur Dioxide 25ml + Oxygen 25ml -> Sulfur Trioxide 50ml
            new RecipeBuilder(RecipeType.Reacting)
                .NamePatterned($"<inputs> <verb>")
                .AddInput(sulfurDioxide.Stack(25))
                .AddInput(oxygen.Stack(25))
                .AddOutput(sulfurTrioxide.Stack(50))
                .SetDuration(TimeSpan.FromSeconds(1))
                .BuildAndRegister();
            
            
            // Sulfur Trioxide 25ml + Water 50ml -> Sulfuric Acid 25ml + Steam 25ml
            new RecipeBuilder(RecipeType.Reacting)
                .NamePatterned($"<inputs> <verb>")
                .AddInput(sulfurTrioxide.Stack(25))
                .AddInput(water.Stack(50))
                .AddOutput(sulfuricAcid.Stack(50))
                .AddOutput(steam.Stack(25))
                .SetDuration(TimeSpan.FromSeconds(1))
                .BuildAndRegister();
            
            // Sulfur Trioxide 25ml + Hydrogen 25ml -> Oleum 50ml
            new RecipeBuilder(RecipeType.Reacting)
                .NamePatterned($"<inputs> <verb>")
                .AddInput(sulfurTrioxide.Stack(25))
                .AddInput(hydrogen.Stack(25))
                .AddOutput(oleum.Stack(50))
                .SetDuration(TimeSpan.FromSeconds(1))
                .BuildAndRegister();
            
            // Oleum 25ml + Water 25ml -> Sulfuric Acid 50ml + Hydrogen 25ml
            new RecipeBuilder(RecipeType.Reacting)
                .NamePatterned($"<inputs> <verb>")
                .AddInput(oleum.Stack(25))
                .AddInput(water.Stack(25))
                .AddOutput(sulfuricAcid.Stack(50))
                .AddOutput(hydrogen.Stack(25))
                .SetDuration(TimeSpan.FromSeconds(1))
                .BuildAndRegister();
        }
    }
}