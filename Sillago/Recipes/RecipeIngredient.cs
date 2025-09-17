namespace Sillago
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class RecipeIngredient
    {
        public List<ItemStack> Options { get; }
        public bool IsConsumed { get; }
    
        protected RecipeIngredient(List<ItemStack> options, bool isConsumed = true)
        {
            if (options.Count == 0)
                throw new ArgumentException("Recipe ingredient must have at least one option.");

            this.Options = options;
            this.IsConsumed = isConsumed;
        }
    
        public static RecipeIngredient AnyOf(List<ItemStack> options, bool isConsumed = true)
        {
            return new RecipeIngredient(options, isConsumed);
        }
    
        public static RecipeIngredient Of(ItemStack option, bool isConsumed = true)
        {
            return new RecipeIngredient(new List<ItemStack> { option }, isConsumed);
        }
    
        public override string ToString()
        {
            if (this.IsConsumed)
                return string.Join(" OR ", this.Options.Select(o => o.ToString()));
            else
                return string.Join(" OR ", this.Options.Select(o => o.ToString())) + " (NC)";
        }
    
        public static implicit operator RecipeIngredient(ItemStack itemStack) => new(new List<ItemStack> { itemStack });
        public bool Contains(Item item)
        {
            return this.Options.Any(o => o.Item == item);
        }
        public bool IsAvailable(Inventory input)
        {
            return this.Options.Any(option => input.GetTotalAmount(option.Item) >= option.Amount);
        }
    
        public static RecipeIngredient operator *(RecipeIngredient ingredient, int multiplier)
        {
            if (multiplier <= 0)
                throw new ArgumentOutOfRangeException(nameof(multiplier), "Multiplier must be greater than zero.");

            List<ItemStack> scaledOptions = ingredient.Options
                .Select(o => new ItemStack(o.Item, o.Amount * multiplier))
                .ToList();

            return new RecipeIngredient(scaledOptions, ingredient.IsConsumed);
        }
    }
}