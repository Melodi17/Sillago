namespace Sillago.Recipes;

public static class Recipes
{
    private static readonly Dictionary<string, Recipe> _entries = new();

    public static IEnumerable<Recipe> Entries => Recipes._entries.Values;

    public static void Register(Recipe recipe)
    {
        if (recipe == null || string.IsNullOrEmpty(recipe.Id))
            throw new ArgumentException("Recipe or Recipe ID cannot be null or empty.");

        if (Recipes._entries.ContainsKey(recipe.Id))
            throw new ArgumentException($"A recipe with ID '{recipe.Id}' is already registered.");

        Recipes._entries[recipe.Id] = recipe;
    }

    public static Recipe Get(string id)
    {
        if (string.IsNullOrEmpty(id))
            throw new ArgumentException("Recipe ID cannot be null or empty.");

        if (Recipes._entries.TryGetValue(id, out Recipe recipe))
            return recipe;

        throw new KeyNotFoundException($"No recipe found with ID '{id}'.");
    }
    public static IEnumerable<Recipe> OfType(RecipeType type)
    {
        return Recipes.Entries.Where(x => x.Type == type);
    }
}