namespace Sillago;

using Utils;

/// <summary>
/// Registry class for managing and accessing all defined items.
/// </summary>
public static class Items
{
    private static readonly Dictionary<string, Item> _entries = new();

    /// <summary>
    /// Fetches all registered items.
    /// </summary>
    public static IEnumerable<Item> Entries => Items._entries.Values;

    /// <summary>
    /// Registers a new item. Throws an exception if an item with the same ID already exists.
    /// </summary>
    public static void Register(Item item)
    {
        if (item == null || string.IsNullOrEmpty(item.Id))
            throw new ArgumentException("Item or Item ID cannot be null or empty.");

        if (Items._entries.ContainsKey(item.Id))
            throw new ArgumentException($"A item with ID '{item.Id}' is already registered.");

        Items._entries[item.Id] = item;
    }

    /// <summary>
    /// Retrieves an item by its unique ID. Throws an exception if not found.
    /// </summary>
    public static Item Get(string id)
    {
        if (string.IsNullOrEmpty(id))
            throw new ArgumentException("Item ID cannot be null or empty.");

        if (Items._entries.TryGetValue(id, out Item? item))
            return item;

        throw new KeyNotFoundException($"No item found with ID '{id}'.");
    }

    /// <summary>
    /// Retrieves a specific item material based on the given material and material type/form.
    /// </summary>
    public static ItemMaterial GetMaterialForm(Material material, MaterialType type)
    {
        string itemId = Identifier.Create($"{material.Name}_{type}");
        if (Items._entries.TryGetValue(itemId, out Item? item) && item is ItemMaterial itemMaterial)
            return itemMaterial;

        throw new KeyNotFoundException($"No item found for material '{material.Name}' with type '{type}'.");
    }
    
    /// <summary>
    /// Tries to retrieve a specific item material based on the given material and material type/form.
    /// If not found, returns null.
    /// </summary>
    public static ItemMaterial? TryGetMaterialForm(Material material, MaterialType type)
    {
        string itemId = Identifier.Create($"{material.Name}_{type}");
        if (Items._entries.TryGetValue(itemId, out Item? item) && item is ItemMaterial itemMaterial)
            return itemMaterial;

        return null;
    }
}