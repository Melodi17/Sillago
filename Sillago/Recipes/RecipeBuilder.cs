namespace Sillago.Recipes;

using Items;

public class RecipeBuilder
{
    public string Id { get; }
    public RecipeType Type { get; }

    private string _name;
    private readonly List<RecipeIngredient> _inputs = [];
    private readonly List<RecipeResult> _outputs = [];
    private readonly List<IRecipeRequirement> _requirements = [];
    private TimeSpan _duration = TimeSpan.FromSeconds(0.5);
    
    public RecipeBuilder(string id, RecipeType type)
    {
        this.Id = id;
        this.Type = type;
        this._name = id;
    }
    
    public RecipeBuilder Name(string name)
    {
        this._name = name;
        return this;
    }
    
    public RecipeBuilder AddInput(Item item, int quantity = 1)
    {
        this._inputs.Add(item.Stack(quantity));
        return this;
    }
    public RecipeBuilder AddInputs(List<RecipeIngredient> ingredients)
    {
        this._inputs.AddRange(ingredients);
        return this;
    }
    public RecipeBuilder AddOutput(Item item, int quantity = 1)
    {
        this._outputs.Add(item.Stack(quantity));
        return this;
    }
    public RecipeBuilder AddOutputs(List<RecipeResult> outputs)
    {
        this._outputs.AddRange(outputs);
        return this;
    }

    public RecipeBuilder AddRequirement(IRecipeRequirement requirement)
    {
        this._requirements.Add(requirement);
        return this;
    }

    public RecipeBuilder SetDuration(TimeSpan duration)
    {
        this._duration = duration;
        return this;
    }

    public Recipe Build()
    {
        return new Recipe(this.Id, this._name, this.Type, this._inputs, this._outputs, this._requirements, this._duration);
    }
}