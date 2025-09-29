namespace Sillago
{
    using System;
    using System.Collections.Generic;
    using Utils;

    public class ChipBuilder
    {
        private string _name;

        private List<Dopant> _dopants = new();
        private List<Exposure> _exposures = new();

        public ChipBuilder(string name)
        {
            this._name = name;
        }

        public ChipBuilder WithDopant(Material dopant, int concentration)
        {
            this._dopants.Add(new Dopant(dopant, concentration));
            return this;
        }

        public ChipBuilder WithExposure(LightType type, float dose, int focalLength)
        {
            this._exposures.Add(new Exposure(type, dose, focalLength));
            return this;
        }

        public Item BuildAndRegister()
        {
            if (this._dopants.Count != this._exposures.Count)
                throw new Exception("Dopants and exposures must be of identical length");

            Item lastItem = Chips.SiliconWaferOxidized;

            foreach (var (dopant, exposure) in System.Linq.Enumerable.Zip(this._dopants, this._exposures))
            {
                var waferPhotoresist = ProcessableItem.Adding(lastItem, "Photoresist Coating");
                var waferPhotoresistBaked = ProcessableItem.Adding(waferPhotoresist, "Photoresist Soft Baked");

                var waferExposed = ProcessableItem.Adding(
                    waferPhotoresistBaked,
                    $"{exposure.Type} Lithography ({this._name}, {exposure.Dose}mJ/cm², {exposure.FocalLength}nm)");

                var waferDeveloped = ProcessableItem.Adding(waferExposed, "Developed");
                var waferDevelopedBaked = ProcessableItem.Adding(waferDeveloped, "Photoresist Hard Baked");
                var waferEtched = ProcessableItem.Adding(waferDevelopedBaked, "Etched");
                var waferStripped = ProcessableItem.Adding(waferEtched, "Photoresist Stripped");

                var waferDoped = ProcessableItem.Adding(
                    waferStripped,
                    $"{dopant.Material}-Doped ({dopant.Concentration}%)");
                
                var waferDopedAnnealed = ProcessableItem.Adding(waferDoped, "Annealed");
                lastItem = waferDopedAnnealed;
                
                Items.Register(waferPhotoresist);
                Items.Register(waferPhotoresistBaked);
                Items.Register(waferExposed);
                Items.Register(waferDeveloped);
                Items.Register(waferDevelopedBaked);
                Items.Register(waferEtched);
                Items.Register(waferStripped);
                Items.Register(waferDoped);
                Items.Register(waferDopedAnnealed);
                
                new RecipeBuilder(RecipeType.SpinCoating)
                    .NamePatterned("<output> <verb>")
                    .AddInput(lastItem.Stack(1))
                    .AddInput(Items.GetMaterialForm(Materials.DnqNovolakPhotoresist, MaterialType.Liquid).Stack(25))
                    .AddOutput(waferPhotoresist.Stack(1))
                    .SetDuration(TimeSpan.FromSeconds(15))
                    .BuildAndRegister();
                
                new RecipeBuilder(RecipeType.HotplateBaking)
                    .NamePatterned("<output> <verb>")
                    .AddInput(waferPhotoresist.Stack(1))
                    .AddOutput(waferPhotoresistBaked.Stack(1))
                    .SetDuration(TimeSpan.FromMinutes(1))
                    .BuildAndRegister();
                
                // Specific pattern is not required since machine is programmed for it by user recipe selection
                new RecipeBuilder(RecipeType.Photolithography)
                    .NamePatterned($"<output> <verb>")
                    .AddInput(waferPhotoresistBaked.Stack(1))
                    .AddOutput(waferExposed.Stack(1))
                    // TODO add light wavelength requirement
                    // TODO add lens requirement for focal length
                    .SetDuration(TimeSpan.FromSeconds(30))
                    .BuildAndRegister();
                
                new RecipeBuilder(RecipeType.SpinCoating)
                    .NamePatterned("<output> <verb>")
                    .AddInput(waferExposed.Stack(1))
                    .AddInput(Items.GetMaterialForm(Materials.TetramethylammoniumHydroxide, MaterialType.Liquid).Stack(25))
                    .AddOutput(waferDeveloped.Stack(1))
                    .SetDuration(TimeSpan.FromSeconds(15))
                    .BuildAndRegister();
                
                new RecipeBuilder(RecipeType.HotplateBaking)
                    .NamePatterned("<output> <verb>")
                    .AddInput(waferDeveloped.Stack(1))
                    .AddOutput(waferDevelopedBaked.Stack(1))
                    .SetDuration(TimeSpan.FromMinutes(1))
                    .BuildAndRegister();
                
                new RecipeBuilder(RecipeType.ChemicalBathing)
                    .NamePatterned("<output> <verb>")
                    .AddInput(waferDevelopedBaked.Stack(1))
                    .AddInput(Items.GetMaterialForm(Materials.HydrofluoricAcid, MaterialType.Liquid).Stack(50))
                    .AddOutput(waferEtched.Stack(1))
                    .SetDuration(TimeSpan.FromSeconds(30))
                    .BuildAndRegister();
                
                new RecipeBuilder(RecipeType.ChemicalBathing)
                    .NamePatterned("<output> <verb>")
                    .AddInput(waferEtched.Stack(1))
                    .AddInput(Items.GetMaterialForm(Materials.Acetone, MaterialType.Liquid).Stack(50))
                    .AddOutput(waferStripped.Stack(1))
                    .SetDuration(TimeSpan.FromSeconds(30))
                    .BuildAndRegister();
                
                new RecipeBuilder(RecipeType.Doping)
                    .NamePatterned("<output> <verb>")
                    .AddInput(waferStripped.Stack(1))
                    .AddInput(Items.GetMaterialForm(dopant.Material, MaterialType.Gas).Stack(dopant.Concentration))
                    .AddOutput(waferDoped.Stack(1))
                    .SetDuration(TimeSpan.FromSeconds(dopant.Concentration * 2))
                    .BuildAndRegister();
                
                new RecipeBuilder(RecipeType.ThermalOxidation)
                    .NamePatterned("<output> <verb>")
                    .AddInput(waferDoped.Stack(1))
                    .AddOutput(waferDopedAnnealed.Stack(1))
                    .SetDuration(TimeSpan.FromMinutes(1))
                    .BuildAndRegister();
            }

            Item chip = new Item(
                id: Identifier.Create($"chip_{this._name}"),
                name: $"{this._name.TitleCase()} Chip",
                description: $"A {this._name} chip fabricated on a processed wafer."
            );
            
            Items.Register(chip);
            
            new RecipeBuilder(RecipeType.Cutting)
                .NamePatterned("<output> <verb>")
                .AddInput(lastItem.Stack(1))
                .AddOutput(chip.Stack(8))
                .SetDuration(TimeSpan.FromSeconds(30))
                .BuildAndRegister();
            
            return chip;
        }
    }
}