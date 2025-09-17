namespace Sillago.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class ItemTests
    {
        [Test]
        public void Constructor_WithValidParameters_ShouldSucceed()
        {
            // Arrange & Act
            var item = new Item("test_id", "Test Item", "Test description");

            // Assert
            Assert.That(item.Id, Is.EqualTo("test_id"));
            Assert.That(item.Name, Is.EqualTo("Test Item"));
            Assert.That(item.Description, Is.EqualTo("Test description"));
            Assert.That(item.CountAsVolume, Is.False, "Default CountAsVolume should be false");
        }

        [Test]
        public void Constructor_WithEmptyDescription_ShouldUseDefault()
        {
            // Arrange & Act
            var item = new Item("test_id", "Test Item");

            // Assert
            Assert.That(item.Description, Is.EqualTo(""));
        }

        [Test]
        public void Constructor_WithNullOrEmptyId_ShouldThrow()
        {
            // Assert
            Assert.That(() => new Item(null!, "Test Item"), 
                Throws.ArgumentException.With.Message.Contains("Item ID cannot be null or empty"));
            Assert.That(() => new Item("", "Test Item"), 
                Throws.ArgumentException.With.Message.Contains("Item ID cannot be null or empty"));
            // Note: whitespace-only string is not caught by the current implementation
            // This is the actual behavior - whitespace strings are allowed
        }

        [Test]
        public void Stack_WithDefaultAmount_ShouldCreateStackOfOne()
        {
            // Arrange
            var item = new Item("test_id", "Test Item");

            // Act
            var stack = item.Stack();

            // Assert
            Assert.That(stack.Item, Is.EqualTo(item));
            Assert.That(stack.Amount, Is.EqualTo(1));
        }

        [Test]
        public void Stack_WithSpecificAmount_ShouldCreateCorrectStack()
        {
            // Arrange
            var item = new Item("test_id", "Test Item");

            // Act
            var stack = item.Stack(5);

            // Assert
            Assert.That(stack.Item, Is.EqualTo(item));
            Assert.That(stack.Amount, Is.EqualTo(5));
        }

        [Test]
        public void Name_ShouldBeSettable()
        {
            // Arrange
            var item = new Item("test_id", "Original Name");

            // Act
            item.Name = "Updated Name";

            // Assert
            Assert.That(item.Name, Is.EqualTo("Updated Name"));
        }

        [Test]
        public void Description_ShouldBeSettable()
        {
            // Arrange
            var item = new Item("test_id", "Test Item", "Original Description");

            // Act
            item.Description = "Updated Description";

            // Assert
            Assert.That(item.Description, Is.EqualTo("Updated Description"));
        }

        [Test]
        public void Id_ShouldBeReadOnly()
        {
            // Arrange & Act
            var item = new Item("test_id", "Test Item");

            // Assert - Id should only have a getter
            Assert.That(item.Id, Is.EqualTo("test_id"));
            // No setter available to test - this is enforced by the compiler
        }
    }

    [TestFixture]
    public class ItemStackTests
    {
        private Item _testItem;

        [SetUp]
        public void SetUp()
        {
            _testItem = new Item("test_id", "Test Item", "Test description");
        }

        [Test]
        public void Constructor_WithValidParameters_ShouldSucceed()
        {
            // Act
            var stack = new ItemStack(_testItem, 5);

            // Assert
            Assert.That(stack.Item, Is.EqualTo(_testItem));
            Assert.That(stack.Amount, Is.EqualTo(5));
        }

        [Test]
        public void Constructor_WithDefaultAmount_ShouldCreateStackOfOne()
        {
            // Act
            var stack = new ItemStack(_testItem);

            // Assert
            Assert.That(stack.Item, Is.EqualTo(_testItem));
            Assert.That(stack.Amount, Is.EqualTo(1));
        }

        [Test]
        public void Constructor_WithZeroOrNegativeAmount_ShouldThrow()
        {
            // Assert
            Assert.That(() => new ItemStack(_testItem, 0),
                Throws.ArgumentException.With.Message.Contains("Amount must be greater than zero"));
            Assert.That(() => new ItemStack(_testItem, -1),
                Throws.ArgumentException.With.Message.Contains("Amount must be greater than zero"));
        }

        [Test]
        public void Amount_ShouldBeSettable()
        {
            // Arrange
            var stack = new ItemStack(_testItem, 5);

            // Act
            stack.Amount = 10;

            // Assert
            Assert.That(stack.Amount, Is.EqualTo(10));
        }

        [Test]
        public void Copy_ShouldCreateExactDuplicate()
        {
            // Arrange
            var originalStack = new ItemStack(_testItem, 7);

            // Act
            var copiedStack = originalStack.Copy();

            // Assert
            Assert.That(copiedStack.Item, Is.EqualTo(originalStack.Item));
            Assert.That(copiedStack.Amount, Is.EqualTo(originalStack.Amount));
            Assert.That(copiedStack, Is.Not.SameAs(originalStack), "Copy should be a different instance");
        }

        [Test]
        public void Copy_ModificationsShouldNotAffectOriginal()
        {
            // Arrange
            var originalStack = new ItemStack(_testItem, 3);
            var copiedStack = originalStack.Copy();

            // Act
            copiedStack.Amount = 10;

            // Assert
            Assert.That(originalStack.Amount, Is.EqualTo(3), "Original should be unchanged");
            Assert.That(copiedStack.Amount, Is.EqualTo(10), "Copy should be modified");
        }

        [Test]
        public void ToString_WithRegularItem_ShouldShowCountAndName()
        {
            // Arrange
            var stack = new ItemStack(_testItem, 3);

            // Act
            var result = stack.ToString();

            // Assert
            Assert.That(result, Is.EqualTo("3 x Test Item"));
        }

        [Test]
        public void ToString_WithVolumeItem_ShouldShowVolumeFormat()
        {
            // Arrange
            var volumeItem = new VolumeTestItem("liquid_id", "Test Liquid");
            var stack = new ItemStack(volumeItem, 250);

            // Act
            var result = stack.ToString();

            // Assert
            Assert.That(result, Is.EqualTo("Test Liquid 250mL"));
        }

        [Test]
        public void ToString_WithSingleRegularItem_ShouldShowCorrectFormat()
        {
            // Arrange
            var stack = new ItemStack(_testItem, 1);

            // Act
            var result = stack.ToString();

            // Assert
            Assert.That(result, Is.EqualTo("1 x Test Item"));
        }

        // Helper class for testing volume items
        private class VolumeTestItem : Item
        {
            public override bool CountAsVolume => true;

            public VolumeTestItem(string id, string name) : base(id, name) { }
        }
    }
}