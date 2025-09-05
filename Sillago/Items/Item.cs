namespace Sillago.Items;

using Utils;

public class Item
{
    public string Id;
    public string Name;
    public string Description;
    public virtual bool CountAsVolume => false;
    public ItemStack Stack(int amount = 1) => new(this, amount);
}