namespace Sillago.Items;

public class Inventory
{
    public Inventory(int capacity)
    {
        this.Capacity = capacity;
        this.Items    = new List<ItemStack>(capacity);
    }
    public List<ItemStack> Items    { get; }
    public int             Capacity { get; }

    public event Action<Inventory, ItemStack> ItemAdded;
    public event Action<Inventory, ItemStack> ItemRemoved;
    public event Action<Inventory> ContentChanged;

    public bool Add(ItemStack itemStack)
    {
        if (itemStack == null || itemStack.Item == null || itemStack.Amount <= 0)
            return false;

        // Check if the item already exists in the inventory
        foreach (ItemStack existingItem in this.Items)
        {
            if (existingItem.Item.Id == itemStack.Item.Id)
            {
                existingItem.Amount += itemStack.Amount;
                this.ContentChanged?.Invoke(this);
                return true;
            }
        }
        
        if (this.Items.Count >= this.Capacity)
            return false;
        
        // If not found, add a new item stack
        ItemStack stack = itemStack.Copy();
        this.Items.Add(stack);
        this.ItemAdded?.Invoke(this, stack);
        this.ContentChanged?.Invoke(this);

        return true;
    }

    public bool Remove(Item item, int count = 1)
    {
        if (item == null || count <= 0)
            return false;

        ItemStack foundStack = this.Items.FirstOrDefault(x => x.Item.Id == item.Id);
        if (foundStack == null)
            return false;

        if (foundStack.Amount < count)
            return false;

        foundStack.Amount -= count;
        this.ContentChanged?.Invoke(this);
        if (foundStack.Amount == 0)
        {
            this.Items.Remove(foundStack);
            this.ItemRemoved?.Invoke(this, foundStack);
        }

        return true;
    }

    public bool Remove(ItemStack stack) => this.Remove(stack.Item, stack.Amount);

    public bool Contains(Item item, int count = 1)
    {
        if (item == null || count <= 0)
            return false;

        ItemStack foundStack = this.Items.FirstOrDefault(x => x.Item.Id == item.Id);
        if (foundStack == null)
            return false;

        if (foundStack.Amount < count)
            return false;

        return true;
    }

    public bool Contains(ItemStack stack) => this.Contains(stack.Item, stack.Amount);
}