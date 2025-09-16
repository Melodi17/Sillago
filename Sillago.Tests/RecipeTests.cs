namespace Sillago.Tests;

using Helpers;
using Requirements;

[TestFixture]
public class RecipeTests
{
    private Recipe CreateSampleRecipeWithRequirements(IRecipeRequirement[] requirements)
    {
        Recipe recipe = new(
            "dingle",
            "dingle",
            new RecipeType("noun", "verb", "machine"),
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
            new RecipeType("test_noun", "test_verb", "test_machine"),
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
}

public class DummyMachine : IMachine, IDummyCapability 
{
    
}