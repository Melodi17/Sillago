namespace Sillago.Tests;

using Helpers;
using Requirements;

[TestFixture]
public class RecipeTests
{
    public RecipeType SampleRecipeType = new("noun", "verb", "machine");
    private Recipe CreateSampleRecipeWithRequirements(IRecipeRequirement[] requirements)
    {
        Recipe recipe = new(
            "test_id",
            "Test Recipe",
            this.SampleRecipeType,
            [],
            [],
            requirements);
        return recipe;
    }

    [Test]
    public void AreRequirementsMet_NoRequirements_ReturnsTrue()
    {
        Recipe recipe = CreateSampleRecipeWithRequirements([]);
        DummyMachine machine = new();

        Assert.That(recipe.AreRequirementsMet(machine), Is.True);
    }

    [Test]
    public void AreRequirementsMet_AllRequirementsMet_ReturnsTrue()
    {
        IRecipeRequirement req1 = new DummyRequirement(true);
        IRecipeRequirement req2 = new DummyRequirement(true);

        Recipe recipe = this.CreateSampleRecipeWithRequirements([req1, req2]);
        DummyMachine machine = new();

        Assert.That(recipe.AreRequirementsMet(machine), Is.True);
    }

    [Test]
    public void AreRequirementsMet_OneRequirementNotMet_ReturnsFalse()
    {
        IRecipeRequirement req1 = new DummyRequirement(true);
        IRecipeRequirement req2 = new DummyRequirement(false);

        Recipe recipe = this.CreateSampleRecipeWithRequirements([req1, req2]);
        DummyMachine machine = new();

        Assert.That(recipe.AreRequirementsMet(machine), Is.False);
    }

    [Test]
    public void GetInfo_IncludesAllDetails()
    {
        ItemStack itemInput = new Item("InputItem", "Input Item").Stack(2);
        ItemStack itemOutput = new Item("OutputItem", "Output Item").Stack(1);

        IRecipeRequirement req1 = new DummyRequirement(true);
        IRecipeRequirement req2 = new DummyRequirement(false);

        Recipe recipe = new(
            "test_id",
            "Test Recipe",
            SampleRecipeType,
            [itemInput],
            [itemOutput],
            [req1, req2],
            TimeSpan.FromSeconds(5));

        string info = recipe.GetInfo();

        Assert.That(info, Does.Contain("ID: test_id"));
        Assert.That(info, Does.Contain("Duration: 5s"));
        Assert.That(info, Does.Contain("Inputs:"));
        Assert.That(info, Does.Contain("- 2 x Input Item"));
        Assert.That(info, Does.Contain("Outputs:"));
        Assert.That(info, Does.Contain("- 1 x Output Item"));
        Assert.That(info, Does.Contain("Dummy Requirement"), "Should include requirement info");
    }
    
    [Test]
    public void AreInputsAvailable_NoInputs_ReturnsTrue()
    {
        Recipe recipe = new(
            "test_id",
            "Test Recipe",
           this.SampleRecipeType,
            [],
            []);
        Inventory inventory = new(99);

        Assert.That(recipe.AreInputsAvailable(inventory), Is.True);
    }
    
    [Test]
    public void AreInputsAvailable_InputsAvailable_ReturnsTrue()
    {
        Item itemA = new("item_a", "Item A");
        
        RecipeIngredient ingredient = RecipeIngredient.Of(itemA.Stack(2));
        Recipe recipe = new(
            "test_id",
            "Test Recipe",
            this.SampleRecipeType,
            [ingredient],
            []);
        
        Inventory inventory = new(99);
        inventory.Add(itemA.Stack(2));
        
        Assert.That(recipe.AreInputsAvailable(inventory), Is.True);
    }
    
    [Test]
    public void AreInputsAvailable_InputsAvailableAlternate_ReturnsTrue()
    {
        Item itemA = new("item_a", "Item A");
        Item itemB = new("item_b", "Item B");
        
        RecipeIngredient ingredient = RecipeIngredient.AnyOf([itemA.Stack(2), itemB.Stack(3)]);
        Recipe recipe = new(
            "test_id",
            "Test Recipe",
            this.SampleRecipeType,
            [ingredient],
            []);
        
        Inventory inventory = new(99);
        inventory.Add(itemB.Stack(3));
        
        Assert.That(recipe.AreInputsAvailable(inventory), Is.True);
    }
    
    [Test]
    public void AreInputsAvailable_InputsNotAvailable_ReturnsFalse()
    {
        Item itemA = new("item_a", "Item A");
        
        RecipeIngredient ingredient = RecipeIngredient.Of(itemA.Stack(2));
        Recipe recipe = new(
            "test_id",
            "Test Recipe",
            this.SampleRecipeType,
            [ingredient],
            []);
        
        Inventory inventory = new(99);
        inventory.Add(itemA.Stack(1));
        
        Assert.That(recipe.AreInputsAvailable(inventory), Is.False);
    }
    
    [Test]
    public void AreInputsAvailable_OneOfMultipleInputsNotAvailable_ReturnsFalse()
    {
        Item itemA = new("item_a", "Item A");
        Item itemB = new("item_b", "Item B");
        
        RecipeIngredient ingredient1 = RecipeIngredient.Of(itemA.Stack(2));
        RecipeIngredient ingredient2 = RecipeIngredient.Of(itemB.Stack(3));
        Recipe recipe = new(
            "test_id",
            "Test Recipe",
            this.SampleRecipeType,
            [ingredient1, ingredient2],
            []);
        
        Inventory inventory = new(99);
        inventory.Add(itemA.Stack(2));
        
        Assert.That(recipe.AreInputsAvailable(inventory), Is.False);
    }
    
    [Test]
    public void ConsumeInputs_InputsConsumed_RemovesFromInventory()
    {
        Item itemA = new("item_a", "Item A");
        Item itemB = new("item_b", "Item B");
        
        RecipeIngredient ingredient1 = RecipeIngredient.Of(itemA.Stack(2));
        RecipeIngredient ingredient2 = RecipeIngredient.Of(itemB.Stack(3));
        Recipe recipe = new(
            "test_id",
            "Test Recipe",
            this.SampleRecipeType,
            [ingredient1, ingredient2],
            []);
        
        Inventory inventory = new(99);
        inventory.Add(itemA.Stack(5));
        inventory.Add(itemB.Stack(5));
        
        recipe.ConsumeInputs(inventory);
        
        Assert.That(inventory.GetTotalAmount(itemA), Is.EqualTo(3));
        Assert.That(inventory.GetTotalAmount(itemB), Is.EqualTo(2));
    }
    
    [Test]
    public void ConsumeInputs_HandlesAlternatesInput_RemovesFromInventory()
    {
        Item itemA = new("item_a", "Item A");
        Item itemB = new("item_b", "Item B");
        
        RecipeIngredient ingredient1 = RecipeIngredient.AnyOf([itemA.Stack(2), itemB.Stack(3)]);
        Recipe recipe = new(
            "test_id",
            "Test Recipe",
            this.SampleRecipeType,
            [ingredient1],
            []);
        
        Inventory inventory = new(99);
        inventory.Add(itemB.Stack(5));
        
        recipe.ConsumeInputs(inventory);
        
        Assert.That(inventory.GetTotalAmount(itemA), Is.EqualTo(0));
        Assert.That(inventory.GetTotalAmount(itemB), Is.EqualTo(2));
    }
    
    [Test]
    public void ConsumeInputs_NonConsumedInput_DoesNotRemoveFromInventory()
    {
        Item itemA = new("item_a", "Item A");
        Item itemB = new("item_b", "Item B");
        
        RecipeIngredient ingredient1 = RecipeIngredient.Of(itemA.Stack(2), isConsumed: false);
        RecipeIngredient ingredient2 = RecipeIngredient.Of(itemB.Stack(3));
        Recipe recipe = new(
            "test_id",
            "Test Recipe",
            this.SampleRecipeType,
            [ingredient1, ingredient2],
            []);
        
        Inventory inventory = new(99);
        inventory.Add(itemA.Stack(5));
        inventory.Add(itemB.Stack(5));
        
        recipe.ConsumeInputs(inventory);
        
        Assert.That(inventory.GetTotalAmount(itemA), Is.EqualTo(5));
        Assert.That(inventory.GetTotalAmount(itemB), Is.EqualTo(2));
    }
    
    [Test]
    public void ConsumeInputs_InsufficientInput_ThrowsException()
    {
        Item itemA = new("item_a", "Item A");
        
        RecipeIngredient ingredient = RecipeIngredient.Of(itemA.Stack(2));
        Recipe recipe = new(
            "test_id",
            "Test Recipe",
            this.SampleRecipeType,
            [ingredient],
            []);
        
        Inventory inventory = new(99);
        inventory.Add(itemA.Stack(1));
        
        Assert.That(() => recipe.ConsumeInputs(inventory), Throws.InvalidOperationException);
    }
    
    [Test]
    public void CanProduceOutputs_SufficientSpace_ReturnsTrue()
    {
        Item itemA = new("item_a", "Item A");
        Item itemB = new("item_b", "Item B");
        
        RecipeResult result1 = RecipeResult.Of(itemA.Stack(2));
        RecipeResult result2 = RecipeResult.Of(itemB.Stack(3));
        Recipe recipe = new(
            "test_id",
            "Test Recipe",
            this.SampleRecipeType,
            [],
            [result1, result2]);
        
        Inventory inventory = new(5); // 5 slots available
        
        Assert.That(recipe.CanProduceOutputs(inventory), Is.True);
    }
    
    [Test]
    public void CanProduceOutputs_InsufficientSpace_ReturnsFalse()
    {
        Item itemA = new("item_a", "Item A");
        Item itemB = new("item_b", "Item B");
        
        RecipeResult result1 = RecipeResult.Of(itemA.Stack(2));
        RecipeResult result2 = RecipeResult.Of(itemB.Stack(3));
        Recipe recipe = new(
            "test_id",
            "Test Recipe",
            this.SampleRecipeType,
            [],
            [result1, result2]);
        
        Inventory inventory = new(1); // Only 1 slot available
        
        Assert.That(recipe.CanProduceOutputs(inventory), Is.False);
    }
    
    [Test]
    public void ProduceOutputs_AddsToInventory()
    {
        Item itemA = new("item_a", "Item A");
        Item itemB = new("item_b", "Item B");
        
        RecipeResult result1 = RecipeResult.Of(itemA.Stack(2));
        RecipeResult result2 = RecipeResult.Of(itemB.Stack(3));
        Recipe recipe = new(
            "test_id",
            "Test Recipe",
            this.SampleRecipeType,
            [],
            [result1, result2]);
        
        Inventory inventory = new(5); // 5 slots available
        
        recipe.ProduceOutputs(inventory);
        
        Assert.That(inventory.GetTotalAmount(itemA), Is.EqualTo(2));
        Assert.That(inventory.GetTotalAmount(itemB), Is.EqualTo(3));
    }
    
    [Test]
    public void ProduceOutputs_InsufficientSpace_ThrowsException()
    {
        Item itemA = new("item_a", "Item A");
        Item itemB = new("item_b", "Item B");
        
        RecipeResult result1 = RecipeResult.Of(itemA.Stack(2));
        RecipeResult result2 = RecipeResult.Of(itemB.Stack(3));
        Recipe recipe = new(
            "test_id",
            "Test Recipe",
            this.SampleRecipeType,
            [],
            [result1, result2]);
        
        Inventory inventory = new(1); // Only 1 slot available
        
        Assert.That(() => recipe.ProduceOutputs(inventory), Throws.InvalidOperationException);
    }
}