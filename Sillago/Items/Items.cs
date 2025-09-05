namespace Sillago.Items;

using Materials;
using Utils;

public static class Items
{
    private static readonly Dictionary<string, Item> _entries = new();

    public static IEnumerable<Item> Entries => Items._entries.Values;

    public static void Register(Item item)
    {
        if (item == null || string.IsNullOrEmpty(item.Id))
            throw new ArgumentException("Item or Item ID cannot be null or empty.");

        if (Items._entries.ContainsKey(item.Id))
            throw new ArgumentException($"A item with ID '{item.Id}' is already registered.");

        Items._entries[item.Id] = item;
    }

    public static Item Get(string id)
    {
        if (string.IsNullOrEmpty(id))
            throw new ArgumentException("Item ID cannot be null or empty.");

        if (Items._entries.TryGetValue(id, out Item item))
            return item;

        throw new KeyNotFoundException($"No item found with ID '{id}'.");
    }

    public static ItemMaterial GetMaterial(Material material, MaterialType type)
    {
        string itemId = Identifier.Create($"{material.Name}_{type}");
        if (Items._entries.TryGetValue(itemId, out Item? item) && item is ItemMaterial itemMaterial)
            return itemMaterial;

        throw new KeyNotFoundException($"No item found for material '{material.Name}' with type '{type}'.");
    }
    
    public static ItemMaterial? TryGetMaterial(Material material, MaterialType type)
    {
        string itemId = Identifier.Create($"{material.Name}_{type}");
        if (Items._entries.TryGetValue(itemId, out Item? item) && item is ItemMaterial itemMaterial)
            return itemMaterial;

        return null;
    }
}