namespace Sillago;

using System.Collections;

/// <summary>
/// Basic inventory system container.
/// </summary>
public class Inventory : IEnumerable<ItemStack>
{
    public Inventory(int capacity)
    {
        this.Capacity = capacity;
        this.Stacks = new List<ItemStack>(capacity);
    }
    public List<ItemStack> Stacks { get; }
    public int Capacity { get; }

    public event Action<Inventory, ItemStack>? ItemAdded;
    public event Action<Inventory, ItemStack>? ItemRemoved;
    public event Action<Inventory>? ContentChanged;
    
    public IEnumerator<ItemStack> GetEnumerator() => this.Stacks.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

    public virtual bool Add(ItemStack itemStack)
    {
        if (itemStack.Amount <= 0)
            return false;

        // Check if the item already exists in the inventory
        foreach (ItemStack existingItem in this.Stacks)
        {
            if (existingItem.Item.Id == itemStack.Item.Id)
            {
                existingItem.Amount += itemStack.Amount;
                this.ContentChanged?.Invoke(this);
                return true;
            }
        }

        if (this.Stacks.Count >= this.Capacity)
            return false;

        // If not found, add a new item stack
        ItemStack stack = itemStack.Copy();
        this.Stacks.Add(stack);
        this.ItemAdded?.Invoke(this, stack);
        this.ContentChanged?.Invoke(this);

        return true;
    }

    public virtual bool Remove(Item item, int count = 1)
    {
        if (count <= 0)
            return false;

        ItemStack? foundStack = this.Stacks.FirstOrDefault(x => x.Item.Id == item.Id);
        if (foundStack == null)
            return false;

        if (foundStack.Amount < count)
            return false;

        foundStack.Amount -= count;
        this.ContentChanged?.Invoke(this);
        if (foundStack.Amount == 0)
        {
            this.Stacks.Remove(foundStack);
            this.ItemRemoved?.Invoke(this, foundStack);
        }

        return true;
    }

    public bool Remove(ItemStack stack) => this.Remove(stack.Item, stack.Amount);

    public virtual bool Contains(Item item, int count = 1)
    {
        if (count <= 0)
            return false;

        ItemStack? foundStack = this.Stacks.FirstOrDefault(x => x.Item.Id == item.Id);
        if (foundStack == null)
            return false;

        if (foundStack.Amount < count)
            return false;

        return true;
    }

    public bool Contains(ItemStack stack) => this.Contains(stack.Item, stack.Amount);
    public int GetTotalAmount(Item item)
    {
        return this.Stacks
            .Where(x => x.Item == item)
            .Sum(x => x.Amount);
    }
}