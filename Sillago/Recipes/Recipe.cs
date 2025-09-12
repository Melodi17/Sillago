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

    public bool AreRequirementsMet(MachineCapabilityHolder capabilityHolder)
    {
        return this.Requirements.All(r => r.IsMet(capabilityHolder));
    }
}