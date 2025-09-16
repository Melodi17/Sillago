namespace Sillago;

public class Item
{
    public string Id { get; }
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual bool CountAsVolume => false;
    public ItemStack Stack(int amount = 1) => new(this, amount);

    public Item(string id, string name, string description = "")
    {
        if (string.IsNullOrEmpty(id))
            throw new ArgumentException("Item ID cannot be null or empty.", nameof(id));
        
        this.Id          = id;
        this.Name        = name;
        this.Description = description;
    }
}