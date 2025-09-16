namespace Sillago;

public class ItemStack
{
    public Item Item;
    public int Amount { get; set; }
    public ItemStack(Item item, int amount = 1)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be greater than zero.");

        this.Item = item;
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