namespace Sillago.Recipes;

using Items;

public class RecipeIngredient
{
    public List<ItemStack> Options { get; }
    public bool IsConsumed { get; }
    
    public RecipeIngredient(List<ItemStack> options, bool isConsumed = true)
    {
        if (options.Count == 0)
            throw new ArgumentException("Recipe ingredient must have at least one option.");

        this.Options = options;
        this.IsConsumed = isConsumed;
    }
    
    public override string ToString()
    {
        if (this.IsConsumed)
            return string.Join(" OR ", this.Options.Select(o => o.ToString()));
        else
            return string.Join(" OR ", this.Options.Select(o => o.ToString())) + " (NC)";
    }
    
    public static implicit operator RecipeIngredient(ItemStack itemStack) => new([itemStack]);
    public bool Contains(Item item)
    {
        return this.Options.Any(o => o.Item == item);
    }
}