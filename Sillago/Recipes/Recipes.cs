namespace Sillago.Recipes;

using Items;

/// <summary>
/// Registry class for managing and accessing all defined recipes.
/// </summary>
public static class Recipes
{
    private static readonly Dictionary<string, Recipe> _entries = new();

    /// <summary>
    /// Fetches all registered recipes.
    /// </summary>
    public static IEnumerable<Recipe> Entries => Recipes._entries.Values;

    /// <summary>
    /// Registers a new recipe. Throws an exception if a recipe with the same ID already exists.
    /// </summary>
    public static void Register(Recipe recipe)
    {
        if (recipe == null || string.IsNullOrEmpty(recipe.Id))
            throw new ArgumentException("Recipe or Recipe ID cannot be null or empty.");

        if (Recipes._entries.ContainsKey(recipe.Id))
            throw new ArgumentException($"A recipe with ID '{recipe.Id}' is already registered.");

        Recipes._entries[recipe.Id] = recipe;
    }

    /// <summary>
    /// Retrieves a recipe by its unique ID. Throws an exception if not found.
    /// </summary>
    public static Recipe Get(string id)
    {
        if (string.IsNullOrEmpty(id))
            throw new ArgumentException("Recipe ID cannot be null or empty.");

        if (Recipes._entries.TryGetValue(id, out Recipe? recipe))
            return recipe;

        throw new KeyNotFoundException($"No recipe found with ID '{id}'.");
    }
    
    /// <summary>
    /// Retrieves all recipes of a specific type.
    /// </summary>
    public static IEnumerable<Recipe> OfType(RecipeType type)
    {
        return Recipes.Entries.Where(x => x.Type == type);
    }
    
    /// <summary>
    /// All recipes that produce the given item as an output.
    /// </summary>
    public static IEnumerable<Recipe> Producing(Item item)
    {
        return Recipes.Entries.Where(r => r.Outputs.Any(o => o.Item == item));
    }
    
    /// <summary>
    /// All recipes that consume the given item as an input.
    /// </summary>
    public static IEnumerable<Recipe> Consuming(Item item)
    {
        return Recipes.Entries.Where(r => r.Inputs.Any(i => i.Item == item));
    }
}