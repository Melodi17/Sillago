namespace Sillago
{
    using System;
    using System.Text;

    public class RecipeResult
    {
        public Item Item { get; }
        public int ResultChance { get; } // Percentage chance (0-100)
        public int MinResult { get; }
        public int MaxResult { get; }
        public string AmountRange => this.MinResult == this.MaxResult 
            ? this.MinResult.ToString() 
            : $"{this.MinResult}-{this.MaxResult}";

        private RecipeResult(
            ItemStack item,
            int resultChance = 100)
        {
            if (resultChance < 0 || resultChance > 100)
                throw new ArgumentOutOfRangeException(nameof(resultChance), "Result chance must be between 0 and 100.");

            this.Item = item.Item;
            this.ResultChance = resultChance;
            this.MinResult = item.Amount;
            this.MaxResult = item.Amount;
        }
    
        private RecipeResult(
            Item item,
            int minResult,
            int maxResult,
            int resultChance = 100)
        {
            if (resultChance < 0 || resultChance > 100)
                throw new ArgumentOutOfRangeException(nameof(resultChance), "Result chance must be between 0 and 100.");
            if (minResult < 0)
                throw new ArgumentOutOfRangeException(nameof(minResult), "Min result cannot be negative.");
            if (maxResult < minResult)
                throw new ArgumentOutOfRangeException(nameof(maxResult), "Max result cannot be less than min result.");

            this.Item = item;
            this.ResultChance = resultChance;
            this.MinResult = minResult;
            this.MaxResult = maxResult;
        }
    
        public static RecipeResult Of(
            ItemStack item,
            int resultChance = 100) => new(item, resultChance);
    
        public static RecipeResult OfBetween(
            Item item,
            int minResult,
            int maxResult,
            int resultChance = 100) => new(item, minResult, maxResult, resultChance);
    
        public ItemStack? GetResult()
        {
            Random rand = new();
            if (rand.Next(0, 100) < this.ResultChance)
            {
                int amount = rand.Next(this.MinResult, this.MaxResult + 1);
                return new ItemStack(this.Item, amount);
            }
        
            return null;
        }
    
        public static implicit operator RecipeResult(ItemStack itemStack) => new(itemStack);
    
        public static implicit operator RecipeResult(Item item) => new(item, 1, 1);
    
        public override string ToString()
        {
            bool isCertain = this.ResultChance == 100;
        
            StringBuilder sb = new();
            if (this.Item.CountAsVolume)
                sb.Append($"{this.Item.Name} {this.AmountRange}mL");
            else
                sb.Append( $"{this.AmountRange} x {this.Item.Name}");
        
            if (!isCertain)
                sb.Append($" ({this.ResultChance}%)");
        
            return sb.ToString();
        }
    }
}