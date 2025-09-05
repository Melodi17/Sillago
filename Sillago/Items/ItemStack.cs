namespace Sillago.Items;

public class ItemStack
{
    public int Amount;
    public Item Item;
    public ItemStack(Item item, int amount = 1)
    {
        this.Item   = item;
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