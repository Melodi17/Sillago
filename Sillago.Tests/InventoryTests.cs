namespace Sillago.Tests;

[TestFixture]
public class InventoryTests
{
    private Item apple;
    private Item banana;
    private ItemStack appleStack;
    private ItemStack bananaStack;

    [SetUp]
    public void Setup()
    {
        apple = new Item("apple", "apple");
        banana = new Item("banana", "banana");
        appleStack = new ItemStack(apple, 5);
        bananaStack = new ItemStack(banana, 3);
    }

    [Test]
    public void Add_NewItem_AddsToInventoryAndRaisesEvents()
    {
        Inventory inventory = new(2);

        ItemStack? added = null;
        inventory.ItemAdded += (inv, stack) => added = stack;

        bool contentChanged = false;
        inventory.ContentChanged += inv => contentChanged = true;

        bool result = inventory.Add(appleStack);

        Assert.That(result, Is.True);
        Assert.That(inventory.Stacks.Count, Is.EqualTo(1));
        Assert.That(inventory.Stacks[0].Item, Is.EqualTo(apple));
        Assert.That(added, Is.Not.Null);
        Assert.That(contentChanged, Is.True);
    }

    [Test]
    public void Add_ExistingItem_IncreasesAmountOnly()
    {
        Inventory inventory = new(2);
        inventory.Add(new ItemStack(apple, 2));

        bool itemAddedCalled = false;
        inventory.ItemAdded += (_, _) => itemAddedCalled = true;

        bool result = inventory.Add(new ItemStack(apple, 3));

        Assert.That(result, Is.True);
        Assert.That(inventory.Stacks.Count, Is.EqualTo(1));
        Assert.That(inventory.Stacks[0].Amount, Is.EqualTo(5));
        Assert.That(itemAddedCalled, Is.False);
    }

    [Test]
    public void Add_FullCapacity_ReturnsFalse()
    {
        Inventory inventory = new(1);
        inventory.Add(appleStack);

        bool result = inventory.Add(bananaStack);

        Assert.That(result, Is.False);
        Assert.That(inventory.Stacks.Count, Is.EqualTo(1));
    }

    [Test]
    public void Add_WithZeroAmount_Throws()
    {
        Inventory inventory = new(2);
        Assert.Throws<ArgumentException>(() => inventory.Add(new ItemStack(apple, 0)));
        Assert.Throws<ArgumentException>(() => inventory.Add(new ItemStack(apple, -1)));
    }

    [Test]
    public void Remove_ReducesAmountAndRaisesContentChanged()
    {
        Inventory inventory = new(2);
        inventory.Add(new ItemStack(apple, 5));

        bool contentChanged = false;
        inventory.ContentChanged += inv => contentChanged = true;

        bool result = inventory.Remove(apple, 2);

        Assert.That(result, Is.True);
        Assert.That(inventory.Stacks[0].Amount, Is.EqualTo(3));
        Assert.That(contentChanged, Is.True);
    }

    [Test]
    public void Remove_ExactAmount_RemovesStackAndRaisesItemRemoved()
    {
        Inventory inventory = new(2);
        inventory.Add(new ItemStack(apple, 2));

        ItemStack? removed = null;
        inventory.ItemRemoved += (inv, stack) => removed = stack;

        bool result = inventory.Remove(apple, 2);

        Assert.That(result, Is.True);
        Assert.That(inventory.Stacks.Count, Is.EqualTo(0));
        Assert.That(removed, Is.Not.Null);
    }

    [Test]
    public void Remove_NotEnoughItems_ReturnsFalse()
    {
        Inventory inventory = new(2);
        inventory.Add(new ItemStack(apple, 2));

        bool result = inventory.Remove(apple, 5);

        Assert.That(result, Is.False);
        Assert.That(inventory.Stacks.Count, Is.EqualTo(1));
        Assert.That(inventory.Stacks[0].Amount, Is.EqualTo(2));
    }

    [Test]
    public void Remove_ItemNotFound_ReturnsFalse()
    {
        Inventory inventory = new(2);
        inventory.Add(appleStack);

        bool result = inventory.Remove(banana, 1);

        Assert.That(result, Is.False);
    }

    [Test]
    public void Remove_WithZeroOrNegativeCount_ReturnsFalse()
    {
        Inventory inventory = new(2);
        inventory.Add(appleStack);

        Assert.That(inventory.Remove(apple, 0), Is.False);
        Assert.That(inventory.Remove(apple, -1), Is.False);
    }

    [Test]
    public void Remove_ByStackWrapperWorks()
    {
        Inventory inventory = new(2);
        inventory.Add(appleStack);

        bool result = inventory.Remove(new ItemStack(apple, 5));

        Assert.That(result, Is.True);
        Assert.That(inventory.Stacks, Is.Empty);
    }

    [Test]
    public void Contains_ReturnsTrueWhenEnoughItems()
    {
        Inventory inventory = new(2);
        inventory.Add(new ItemStack(apple, 5));

        bool result = inventory.Contains(apple, 3);

        Assert.That(result, Is.True);
    }

    [Test]
    public void Contains_ReturnsFalseWhenNotEnoughItems()
    {
        Inventory inventory = new(2);
        inventory.Add(new ItemStack(apple, 2));

        bool result = inventory.Contains(apple, 3);

        Assert.That(result, Is.False);
    }

    [Test]
    public void Contains_ItemNotPresent_ReturnsFalse()
    {
        Inventory inventory = new(2);
        inventory.Add(appleStack);

        bool result = inventory.Contains(banana, 1);

        Assert.That(result, Is.False);
    }

    [Test]
    public void Contains_WithZeroOrNegativeCount_ReturnsFalse()
    {
        Inventory inventory = new(2);
        inventory.Add(appleStack);

        Assert.That(inventory.Contains(apple, 0), Is.False);
        Assert.That(inventory.Contains(apple, -1), Is.False);
    }

    [Test]
    public void Contains_ByStackWrapperWorks()
    {
        Inventory inventory = new(2);
        inventory.Add(new ItemStack(apple, 5));

        bool result = inventory.Contains(new ItemStack(apple, 5));

        Assert.That(result, Is.True);
    }

    [Test]
    public void GetTotalAmount_SumsCorrectlyAcrossStacks()
    {
        Inventory inventory = new(3);
        inventory.Add(new ItemStack(apple, 2));
        inventory.Add(new ItemStack(banana, 4));
        inventory.Add(new ItemStack(apple, 3));

        int total = inventory.GetTotalAmount(apple);

        Assert.That(total, Is.EqualTo(5));
    }

    [Test]
    public void GetTotalAmount_NoMatches_ReturnsZero()
    {
        Inventory inventory = new(2);
        inventory.Add(appleStack);

        int total = inventory.GetTotalAmount(banana);

        Assert.That(total, Is.EqualTo(0));
    }

    [Test]
    public void Enumerator_IteratesOverStacks()
    {
        Inventory inventory = new(2);
        inventory.Add(appleStack);
        inventory.Add(bananaStack);

        List<ItemStack> items = inventory.ToList();

        Assert.That(items.Count, Is.EqualTo(2));
        Assert.That(items.Any(x => x.Item == apple));
        Assert.That(items.Any(x => x.Item == banana));
    }

    [Test]
    public void NonGenericEnumerator_IteratesOverStacks()
    {
        Inventory inventory = new(2);
        inventory.Add(appleStack);

        List<object> items = new();
        foreach (object stack in (System.Collections.IEnumerable) inventory)
        {
            items.Add(stack);
        }

        Assert.That(items.Count, Is.EqualTo(1));
        Assert.That(items[0], Is.InstanceOf<ItemStack>());
    }
}