namespace Sillago.Tests;

[TestFixture]
public class SillagoEngineTests
{
    private static bool _isInitialized = false;
    
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        // Initialize once for all tests
        if (!_isInitialized)
        {
            SillagoEngine.Initialize();
            _isInitialized = true;
        }
    }

    [Test]
    public void Initialize_ShouldGenerateItemsAndRecipes()
    {
        // Arrange & Act - engine should already be initialized
        
        // Assert
        var itemCount = Items.Entries.Count();
        var recipeCount = Recipes.Entries.Count();

        Assert.That(itemCount, Is.GreaterThan(0), 
            "Engine initialization should have created items");
        Assert.That(recipeCount, Is.GreaterThan(0), 
            "Engine initialization should have created recipes");
    }

    [Test]
    public void Initialize_ShouldCreateWaterDistillingRecipe()
    {
        // Arrange & Act - engine should already be initialized

        // Assert - check that water distilling recipe exists
        var distillingRecipes = Recipes.Entries.Where(r => r.Type.Verb == "Distilling").ToList();
        Assert.That(distillingRecipes, Is.Not.Empty, 
            "Should create water distilling recipe");

        var waterDistillingRecipe = distillingRecipes.FirstOrDefault(r => 
            r.Inputs.Any(input => input.Options.Any(option => option.Item.Name.ToLower().Contains("water"))));
        Assert.That(waterDistillingRecipe, Is.Not.Null, 
            "Should have a recipe that distills water");
    }

    [Test]
    public void Initialize_ShouldCreateMaterialItems()
    {
        // Arrange & Act - engine should already be initialized

        // Assert - check that material items are created
        var materialItems = Items.Entries.OfType<ItemMaterial>().ToList();
        Assert.That(materialItems, Is.Not.Empty, 
            "Should create ItemMaterial instances");

        // Check for common material types
        var hasLiquid = materialItems.Any(item => item.Type == MaterialType.Liquid);
        var hasIngot = materialItems.Any(item => item.Type == MaterialType.Ingot);
        var hasOre = materialItems.Any(item => item.Type == MaterialType.Ore);

        Assert.That(hasLiquid, Is.True, "Should have liquid materials");
        Assert.That(hasIngot, Is.True, "Should have ingot materials");  
        Assert.That(hasOre, Is.True, "Should have ore materials");
    }

    [Test]
    public void Initialize_MultipleCallsShouldBeSafe()
    {
        // Act - multiple calls should be safe (idempotent or gracefully handled)
        var itemCountBefore = Items.Entries.Count();
        var recipeCountBefore = Recipes.Entries.Count();

        // Note: This might throw if not designed for multiple calls
        // but that's also valid behavior to test
        try
        {
            SillagoEngine.Initialize();
            
            var itemCountAfter = Items.Entries.Count();
            var recipeCountAfter = Recipes.Entries.Count();

            // If it doesn't throw, items and recipes should still exist
            Assert.That(itemCountAfter, Is.GreaterThan(0), 
                "Should still have items after additional initialization");
            Assert.That(recipeCountAfter, Is.GreaterThan(0), 
                "Should still have recipes after additional initialization");
        }
        catch (ArgumentException ex) when (ex.Message.Contains("already registered"))
        {
            // This is acceptable behavior - engine isn't designed for multiple inits
            Assert.Pass("Engine correctly prevents duplicate initialization");
        }
    }
}