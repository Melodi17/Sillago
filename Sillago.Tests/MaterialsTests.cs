namespace Sillago.Tests;

using Types;

[TestFixture]
public class MaterialsTests
{
    // Engine should be initialized by GlobalTestSetup

    [Test]
    public void GenerateItemsAndRecipes_ShouldCreateMaterials()
    {
        // Act - should already be called by engine initialization
        
        // Assert - materials should be available
        Assert.That(Materials.Aluminium, Is.Not.Null, "Aluminium material should be available");
        Assert.That(Materials.Water, Is.Not.Null, "Water material should be available");
        Assert.That(Materials.Iron, Is.Not.Null, "Iron material should be available");
        Assert.That(Materials.Copper, Is.Not.Null, "Copper material should be available");
    }

    [Test]
    public void StaticMaterials_ShouldHaveCorrectTypes()
    {
        // Assert - check material types
        Assert.That(Materials.Aluminium, Is.InstanceOf<MetalMaterial>(), "Aluminium should be a MetalMaterial");
        Assert.That(Materials.Water, Is.InstanceOf<FluidMaterial>(), "Water should be a FluidMaterial"); 
        Assert.That(Materials.Iron, Is.InstanceOf<MetalMaterial>(), "Iron should be a MetalMaterial");
        Assert.That(Materials.Copper, Is.InstanceOf<MetalMaterial>(), "Copper should be a MetalMaterial");
    }

    [Test]
    public void StaticMaterials_ShouldHaveCorrectNames()
    {
        // Assert
        Assert.That(Materials.Aluminium.Name, Is.EqualTo("Aluminium"));
        Assert.That(Materials.Water.Name, Is.EqualTo("Water"));
        Assert.That(Materials.Iron.Name, Is.EqualTo("Iron"));
        Assert.That(Materials.Copper.Name, Is.EqualTo("Copper"));
        Assert.That(Materials.Gold.Name, Is.EqualTo("Gold"));
    }

    [Test]
    public void StaticMaterials_ShouldHaveSymbols()
    {
        // Assert - all materials should have symbols
        Assert.That(Materials.Aluminium.Symbol, Is.Not.Null, "Aluminium should have a symbol");
        Assert.That(Materials.Water.Symbol, Is.Not.Null, "Water should have a symbol");
        Assert.That(Materials.Iron.Symbol, Is.Not.Null, "Iron should have a symbol");
        Assert.That(Materials.Copper.Symbol, Is.Not.Null, "Copper should have a symbol");
    }

    [Test]
    public void StaticMaterials_ShouldHaveColors()
    {
        // Assert - materials should have non-zero colors
        Assert.That(Materials.Aluminium.Color, Is.Not.EqualTo(0), "Aluminium should have a color");
        Assert.That(Materials.Water.Color, Is.Not.EqualTo(0), "Water should have a color");
        Assert.That(Materials.Iron.Color, Is.Not.EqualTo(0), "Iron should have a color");
        Assert.That(Materials.Copper.Color, Is.Not.EqualTo(0), "Copper should have a color");
    }

    [Test]
    public void StaticMaterials_ShouldHavePositiveDensity()
    {
        // Assert - materials should have realistic densities
        Assert.That(Materials.Aluminium.Density, Is.GreaterThan(0), "Aluminium should have positive density");
        Assert.That(Materials.Water.Density, Is.GreaterThan(0), "Water should have positive density");
        Assert.That(Materials.Iron.Density, Is.GreaterThan(0), "Iron should have positive density");
        Assert.That(Materials.Copper.Density, Is.GreaterThan(0), "Copper should have positive density");
        
        // Check some realistic ranges
        Assert.That(Materials.Water.Density, Is.InRange(900f, 1100f), "Water density should be around 1000 kg/m³");
        Assert.That(Materials.Aluminium.Density, Is.InRange(2500f, 3000f), "Aluminium density should be around 2700 kg/m³");
    }

    [Test]
    public void StaticMaterials_ShouldHaveFormNames()
    {
        // Arrange
        var material = Materials.Aluminium;

        // Assert - check form names are properly set
        Assert.That(material.FormNames, Is.Not.Empty, "Material should have form names");
        Assert.That(material.FormNames.ContainsKey(MaterialType.Ingot), Is.True, "Should have ingot form name");
        Assert.That(material.FormNames.ContainsKey(MaterialType.Powder), Is.True, "Should have powder form name");
        Assert.That(material.FormNames.ContainsKey(MaterialType.Liquid), Is.True, "Should have liquid form name");
        
        Assert.That(material.FormNames[MaterialType.Ingot], Is.EqualTo("Aluminium Ingot"));
        Assert.That(material.FormNames[MaterialType.Powder], Is.EqualTo("Aluminium Powder"));
        Assert.That(material.FormNames[MaterialType.Liquid], Is.EqualTo("Molten Aluminium")); // Note: Metals use "Molten" instead of "Liquid"
    }

    [Test]
    public void StaticMaterials_ShouldHaveFlags()
    {
        // Assert - materials should have appropriate flags
        Assert.That(Materials.Aluminium.Flags, Is.Not.EqualTo(MaterialFlags.None), 
            "Aluminium should have material flags");
        Assert.That(Materials.Iron.Flags, Is.Not.EqualTo(MaterialFlags.None), 
            "Iron should have material flags");
        
        // Check specific flags for metals
        Assert.That(Materials.Aluminium.Flags.HasFlag(MaterialFlags.ElectricallyConductive), Is.True, 
            "Aluminium should be electrically conductive");
        Assert.That(Materials.Copper.Flags.HasFlag(MaterialFlags.ElectricallyConductive), Is.True, 
            "Copper should be electrically conductive");
    }

    [Test]
    public void StaticMaterials_ShouldHaveVisualSets()
    {
        // Assert - materials should have visual sets (check that they're assigned, even if some use default values)
        // VisualSet is an enum, so we just verify they have been assigned values
        Assert.That(Enum.IsDefined(typeof(VisualSet), Materials.Aluminium.VisualSet), Is.True, 
            "Aluminium should have a valid visual set");
        Assert.That(Enum.IsDefined(typeof(VisualSet), Materials.Water.VisualSet), Is.True, 
            "Water should have a valid visual set");
        Assert.That(Enum.IsDefined(typeof(VisualSet), Materials.Iron.VisualSet), Is.True, 
            "Iron should have a valid visual set");
    }

    [Test]
    public void GenerateLinkingRecipes_ShouldCreateWaterDistillingRecipe()
    {
        // Arrange & Act - should be called during initialization
        
        // Assert - water distilling recipe should exist
        var distillingRecipes = Recipes.Entries.Where(r => r.Type == RecipeType.Distilling).ToList();
        Assert.That(distillingRecipes, Is.Not.Empty, "Should have distilling recipes");
        
        var waterDistillingRecipe = distillingRecipes.FirstOrDefault(r => 
            r.Inputs.Any(input => input.Options.Any(option => 
                option.Item.Name.ToLower().Contains("water"))));
        Assert.That(waterDistillingRecipe, Is.Not.Null, "Should have water distilling recipe");
    }

    [Test]
    public void MaterialTypes_ShouldBeDistinct()
    {
        // Arrange
        var materials = new[] { Materials.Aluminium, Materials.Water, Materials.Iron, Materials.Copper };

        // Act & Assert - each material should be a different instance
        for (int i = 0; i < materials.Length; i++)
        {
            for (int j = i + 1; j < materials.Length; j++)
            {
                Assert.That(materials[i], Is.Not.SameAs(materials[j]), 
                    $"Materials {materials[i].Name} and {materials[j].Name} should be different instances");
            }
        }
    }
}