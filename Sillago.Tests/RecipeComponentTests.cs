namespace Sillago.Tests
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;

    [TestFixture]
    public class RecipeComponentTests
    {
        private Item _testItem;
        private ItemStack _testStack;

        [SetUp]
        public void SetUp()
        {
            _testItem = new Item("test_item", "Test Item");
            _testStack = new ItemStack(_testItem, 5);
        }

        [Test]
        public void RecipeIngredient_Of_ShouldCreateSingleOption()
        {
            // Act
            var ingredient = RecipeIngredient.Of(_testStack);

            // Assert
            Assert.That(ingredient.Options, Has.Count.EqualTo(1));
            Assert.That(ingredient.Options[0], Is.EqualTo(_testStack));
            Assert.That(ingredient.IsConsumed, Is.True, "Default should be consumed");
        }

        [Test]
        public void RecipeIngredient_Of_WithNotConsumed_ShouldCreateNonConsumedIngredient()
        {
            // Act
            var ingredient = RecipeIngredient.Of(_testStack, false);

            // Assert
            Assert.That(ingredient.Options, Has.Count.EqualTo(1));
            Assert.That(ingredient.IsConsumed, Is.False);
        }

        [Test]
        public void RecipeIngredient_AnyOf_ShouldCreateMultipleOptions()
        {
            // Arrange
            var stack1 = new ItemStack(_testItem, 3);
            var stack2 = new ItemStack(_testItem, 7);
            var options = new List<ItemStack> { stack1, stack2 };

            // Act
            var ingredient = RecipeIngredient.AnyOf(options);

            // Assert
            Assert.That(ingredient.Options, Has.Count.EqualTo(2));
            Assert.That(ingredient.Options, Contains.Item(stack1));
            Assert.That(ingredient.Options, Contains.Item(stack2));
        }

        [Test]
        public void RecipeIngredient_Constructor_WithEmptyOptions_ShouldThrow()
        {
            // Assert
            Assert.That(() => RecipeIngredient.AnyOf(new List<ItemStack>()),
                Throws.ArgumentException.With.Message.Contains("at least one option"));
        }

        [Test]
        public void RecipeIngredient_ImplicitConversion_ShouldWork()
        {
            // Act
            RecipeIngredient ingredient = _testStack;

            // Assert
            Assert.That(ingredient.Options, Has.Count.EqualTo(1));
            Assert.That(ingredient.Options[0], Is.EqualTo(_testStack));
        }

        [Test]
        public void RecipeIngredient_Contains_ShouldReturnTrueForMatchingItem()
        {
            // Arrange
            var ingredient = RecipeIngredient.Of(_testStack);

            // Assert
            Assert.That(ingredient.Contains(_testItem), Is.True);
        }

        [Test]
        public void RecipeIngredient_Contains_ShouldReturnFalseForNonMatchingItem()
        {
            // Arrange
            var ingredient = RecipeIngredient.Of(_testStack);
            var otherItem = new Item("other_item", "Other Item");

            // Assert
            Assert.That(ingredient.Contains(otherItem), Is.False);
        }

        [Test]
        public void RecipeIngredient_IsAvailable_ShouldReturnTrueWhenEnoughInInventory()
        {
            // Arrange
            var inventory = new Inventory(100);  // Capacity of 100
            inventory.Add(_testStack.Copy());
            inventory.Add(_testStack.Copy());                                  // Total: 10 items
            var ingredient = RecipeIngredient.Of(new ItemStack(_testItem, 8)); // Need 8

            // Act & Assert
            Assert.That(ingredient.IsAvailable(inventory), Is.True);
        }

        [Test]
        public void RecipeIngredient_IsAvailable_ShouldReturnFalseWhenNotEnoughInInventory()
        {
            // Arrange
            var inventory = new Inventory(100);                                // Capacity of 100
            inventory.Add(new ItemStack(_testItem, 3));                        // Only 3 items
            var ingredient = RecipeIngredient.Of(new ItemStack(_testItem, 8)); // Need 8

            // Act & Assert
            Assert.That(ingredient.IsAvailable(inventory), Is.False);
        }

        [Test]
        public void RecipeIngredient_MultiplyOperator_ShouldScaleAmounts()
        {
            // Arrange
            var ingredient = RecipeIngredient.Of(_testStack); // 5 items
        
            // Act
            var scaledIngredient = ingredient * 3;

            // Assert
            Assert.That(scaledIngredient.Options, Has.Count.EqualTo(1));
            Assert.That(scaledIngredient.Options[0].Amount, Is.EqualTo(15)); // 5 * 3
            Assert.That(scaledIngredient.IsConsumed, Is.EqualTo(ingredient.IsConsumed));
        }

        [Test]
        public void RecipeIngredient_MultiplyOperator_WithZeroOrNegative_ShouldThrow()
        {
            // Arrange
            var ingredient = RecipeIngredient.Of(_testStack);

            // Assert
            Assert.That(() => ingredient * 0,
                Throws.TypeOf<ArgumentOutOfRangeException>());
            Assert.That(() => ingredient * -1,
                Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void RecipeIngredient_ToString_WithConsumed_ShouldNotShowNC()
        {
            // Arrange
            var ingredient = RecipeIngredient.Of(_testStack, true);

            // Act
            var result = ingredient.ToString();

            // Assert
            Assert.That(result, Does.Not.Contain("(NC)"));
            Assert.That(result, Does.Contain(_testStack.ToString()));
        }

        [Test]
        public void RecipeIngredient_ToString_WithNotConsumed_ShouldShowNC()
        {
            // Arrange
            var ingredient = RecipeIngredient.Of(_testStack, false);

            // Act
            var result = ingredient.ToString();

            // Assert
            Assert.That(result, Does.Contain("(NC)"));
        }

        [Test]
        public void RecipeResult_ShouldHaveBasicProperties()
        {
            // Act
            var result = RecipeResult.Of(_testStack);

            // Assert
            Assert.That(result.Item, Is.EqualTo(_testItem));
            Assert.That(result.ResultChance, Is.EqualTo(100));
            Assert.That(result.MinResult, Is.EqualTo(5));
            Assert.That(result.MaxResult, Is.EqualTo(5));
        }

        [Test]
        public void RecipeResult_ImplicitConversion_ShouldWork()
        {
            // Act
            RecipeResult result = _testStack;

            // Assert
            Assert.That(result.Item, Is.EqualTo(_testItem));
            Assert.That(result.MinResult, Is.EqualTo(5));
        }
    }
}