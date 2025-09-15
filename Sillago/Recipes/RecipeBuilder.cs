namespace Sillago.Recipes;

using Items;
using Materials;
using Utils;

public class RecipeBuilder
{
    public RecipeType Type { get; }

    private Func<string>? _name;
    private Func<TimeSpan>? _duration;
    private readonly List<RecipeIngredient> _inputs = [];
    private readonly List<RecipeResult> _outputs = [];
    private readonly List<IRecipeRequirement> _requirements = [];

    public RecipeBuilder(RecipeType type)
    {
        this.Type = type;
    }

    public RecipeBuilder Name(string name)
    {
        this._name = () => name;
        return this;
    }
    public RecipeBuilder AddInput(ItemStack stack)
    {
        this._inputs.Add(stack);
        return this;
    }
    public RecipeBuilder AddInput(RecipeIngredient ingredient)
    {
        this._inputs.Add(ingredient);
        return this;
    }
    public RecipeBuilder AddInputs(List<RecipeIngredient> ingredients)
    {
        this._inputs.AddRange(ingredients);
        return this;
    }
    public RecipeBuilder AddOutput(ItemStack output)
    {
        this._outputs.Add(output);
        return this;
    }
    public RecipeBuilder AddOutput(RecipeResult output)
    {
        this._outputs.Add(output);
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
        this._duration = () => duration;
        return this;
    }
    
    public RecipeBuilder SetDuration(Func<List<RecipeIngredient>, List<RecipeResult>, TimeSpan> durationFunc)
    {
        this._duration = () => durationFunc(this._inputs, this._outputs);
        return this;
    }

    public Recipe Build()
    {
        string id = CreateId();
        string? name = this._name?.Invoke();
        TimeSpan? duration = this._duration?.Invoke();
        
        if (string.IsNullOrEmpty(name))
            throw new InvalidOperationException("Recipe must have a name.");

        if (this._inputs.Count == 0)
            throw new InvalidOperationException("Recipe must have at least one input.");

        if (this._outputs.Count == 0)
            throw new InvalidOperationException("Recipe must have at least one output.");

        if (duration == null || duration == TimeSpan.Zero)
            throw new InvalidOperationException("Recipe must have a non-zero duration.");

        return new Recipe(
            id,
            name,
            this.Type,
            this._inputs,
            this._outputs,
            this._requirements,
            duration.Value);
    }
    
    public void BuildAndRegister()
    {
        Recipe recipe = this.Build();
        Recipes.Register(recipe);
    }

    private string CreateId()
    {
        string inputPart = string.Join("_", this._inputs.Select(i => $"{i.Options[0].Item.Id}{i.Options[0].Amount}"));
        string outputPart = string.Join("_", this._outputs.Select(o => $"{o.Item.Id}{o.MinResult}"));
        return Identifier.Create($"{this.Type.Noun.ToLower()}_{inputPart}_to_{outputPart}");
    }

    /// <summary>
    /// Names the recipe automatically based on the pattern.<br/>
    /// - Use &lt;noun&gt; to insert processing noun.<br/>
    /// - Use &lt;verb&gt; to insert processing verb.<br/>
    /// - Use &lt;inputs&gt; to insert input item names.<br/>
    /// - Use &lt;input&gt; to insert the first input item name.<br/>
    /// - Use &lt;outputs&gt; to insert output item names.<br/>
    /// - Use &lt;output&gt; to insert the first output item name.<br/>
    /// Example: "&lt;process&gt; &lt;input&gt; into &lt;outputs&gt;" -> "Smelting Iron Ore into Iron Ingot, Slag"
    /// </summary>
    public RecipeBuilder NamePatterned(string pattern)
    {
        this._name = () =>
        {
            string inputs = string.Join(", ", this._inputs.Select(i => i.Options[0].Item.Name));
            string outputs = string.Join(", ", this._outputs.Select(o => o.Item.Name));
            string primaryInputName = GetPrimaryInput().Name;
            string primaryOutputName = GetPrimaryOutput().Name;
            
            return pattern
                .Replace("<noun>", this.Type.Noun.ToLower())
                .Replace("<verb>", this.Type.Verb.ToLower())
                .Replace("<inputs>", inputs)
                .Replace("<input>", primaryInputName)
                .Replace("<outputs>", outputs)
                .Replace("<output>", primaryOutputName)
                .TitleCase();
        };

        return this;
    }

    private Item GetPrimaryOutput()
    {
        if (this._outputs.Count == 0)
            throw new InvalidOperationException("Recipe has no outputs.");

        return this
            ._outputs
            .OrderByDescending(x => x.ResultChance)
            .ThenByDescending(x => x.MinResult)
            .First()
            .Item;
    }

    private Item GetPrimaryInput()
    {
        if (this._inputs.Count == 0)
            throw new InvalidOperationException("Recipe has no inputs.");

        Item? primaryInput = this._inputs.FirstOrDefault(x => x.IsConsumed)?.Options[0].Item;
        if (primaryInput != null)
            return primaryInput;

        return this._inputs.First().Options[0].Item;
    }
}