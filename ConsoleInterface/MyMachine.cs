namespace ConsoleInterface;

using Sillago.Items;
using Sillago.Recipes;

public class MyMachine : IMachine, ITemperatureCapability
{
    public Inventory Input { get; } = new(4);
    public Inventory Output { get; } = new(4);
    public Recipe? CurrentRecipe { get; private set; }
    public RecipeType Type => RecipeType.Smelting;
    public string Status { get; private set; } = "Idle";
    public float? Progress { get; private set; } = null;
    public float Temperature { get; private set; }
    
    public void Tick(float deltaTime)
    {
        if (this.Progress >= 1f)
            FinishProcessing();
        else if (this.Progress != null)
            UpdateProgress(deltaTime);
        else 
            Process();
    }

    public string Process()
    {
        if (this.CurrentRecipe == null)
            return "No recipe set.";

        if (!CurrentRecipe.AreRequirementsMet(this))
            return "Requirements not met.";
        
        if (!CurrentRecipe.AreInputsAvailable(this.Input))
            return "Not enough input items.";
        
        CurrentRecipe.ConsumeInputs(this.Input);
        Progress = 0f;
        return "Working...";
    }

    private void FinishProcessing()
    {
        if (this.CurrentRecipe == null)
            throw new InvalidOperationException("No recipe set.");
        
        if (!CurrentRecipe.CanProduceOutputs(this.Output))
        {
            Status = "Output Full";
            return;
        }
        
        this.Progress = null;
        this.Status = "Idle";
        
        CurrentRecipe.ProduceOutputs(this.Output);
    }

    public void UpdateProgress(float delta)
    {
        if (this.CurrentRecipe == null || this.Progress == null)
            return;

        this.Progress += delta / (float)this.CurrentRecipe.Duration.TotalSeconds;
    }
    
    public void SetTemperature(float temperature)
    {
        this.Temperature = temperature;
    }
    
    public void SetRecipe(Recipe recipe)
    {
        this.CurrentRecipe = recipe;
    }
}