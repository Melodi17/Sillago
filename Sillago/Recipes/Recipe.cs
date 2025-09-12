namespace Sillago.Recipes;

using System.Text;
using Items;

public class Recipe
{
    public string Id { get; }
    public string Name { get; }
    public RecipeType Type { get; }

    public IReadOnlyList<RecipeIngredient> Inputs { get; }
    public IReadOnlyList<RecipeResult> Outputs { get; }

    public IReadOnlyList<IRecipeRequirement> Requirements { get; }
    public TimeSpan Duration { get; }

    public Recipe(
        string id,
        string name,
        RecipeType type,
        IReadOnlyList<RecipeIngredient> inputs,
        IReadOnlyList<RecipeResult> outputs,
        IReadOnlyList<IRecipeRequirement>? requirements = null,
        TimeSpan duration = default)
    {
        this.Id = id;
        this.Name = name;
        this.Type = type;
        this.Inputs = inputs;
        this.Outputs = outputs;
        this.Requirements = requirements ?? [];
        this.Duration = duration;
    }

    public string GetInfo()
    {
        StringBuilder sb = new();

        sb.AppendLine($"ID: {this.Id}");
        sb.AppendLine($"Duration: {this.Duration.TotalSeconds:0.##}s");

        sb.AppendLine($"Inputs:");
        foreach (RecipeIngredient input in this.Inputs)
            sb.AppendLine($"  - {input}");

        sb.AppendLine($"Outputs:");
        foreach (RecipeResult output in this.Outputs)
            sb.AppendLine($"  - {output}");

        foreach (IRecipeRequirement requirement in this.Requirements)
            sb.Append(requirement.GetInfo());

        return sb.ToString();
    }

    public bool AreRequirementsMet(IMachine machine)
    {
        return this.Requirements.All(r => r.IsMet(machine));
    }
    public bool AreInputsAvailable(Inventory input)
    {
        return this.Inputs.All(recipeInput => recipeInput.IsAvailable(input));
    }
    
    public void ConsumeInputs(Inventory input)
    {
        foreach (RecipeIngredient ingredient in this.Inputs)
        {
            if (ingredient.IsConsumed)
            {
                // Find the first option that is available in the input inventory
                ItemStack? matchedOption = ingredient.Options
                    .FirstOrDefault(option => input.GetTotalAmount(option.Item) >= option.Amount);
                
                if (matchedOption == null)
                    throw new InvalidOperationException("Not enough input items to consume. Was AreInputsAvailable checked before calling this method?");

                input.Remove(matchedOption.Item, matchedOption.Amount);
            }
        }
    }
    public bool CanProduceOutputs(Inventory output)
    {
        int requiredSlots = this.Outputs.Count;
        int availableSlots = output.Capacity - output.Stacks.Count;
        if (availableSlots < requiredSlots)
            return false;
        
        return true;
    }
    
    public void ProduceOutputs(Inventory output)
    {
        foreach (RecipeResult result in this.Outputs)
        {
            ItemStack? produced = result.GetResult();
            if (produced != null)
            {
                bool added = output.Add(produced);
                if (!added)
                    throw new InvalidOperationException("Not enough space in output inventory to add produced items. Was CanProduceOutputs checked before calling this method?");
            }
        }
    }
}