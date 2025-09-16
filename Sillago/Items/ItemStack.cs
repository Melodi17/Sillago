namespace Sillago;

public class ItemStack
{
    public int Amount { get; set; }
    public Item Item { get; }
    
    public ItemStack(Item item, int amount = 1)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be non-negative");
        
        this.Item = item ?? throw new ArgumentNullException(nameof(item));
        this.Amount = amount;
    }
    
    public ItemStack Copy() => new(this.Item, this.Amount);
    
    public override string ToString()
    {
        if (this.Item.CountAsVolume)
            return $"{this.Item.Name} {this.Amount}mL";
        else
            return $"{this.Amount} x {this.Item.Name}";
    }
}