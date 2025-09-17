namespace Sillago.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class ItemMaterialTests
    {
        private Material _testMaterial;

        // Engine should be initialized by GlobalTestSetup

        [SetUp]
        public void SetUp()
        {
            // Use water as a test material since we know it exists
            _testMaterial = Materials.Water;
        }

        [Test]
        public void Constructor_WithValidParameters_ShouldSucceed()
        {
            // Act
            var itemMaterial = new ItemMaterial(_testMaterial, MaterialType.Liquid);

            // Assert
            Assert.That(itemMaterial.Material, Is.EqualTo(_testMaterial));
            Assert.That(itemMaterial.Type, Is.EqualTo(MaterialType.Liquid));
            Assert.That(itemMaterial.Id, Does.Contain("water"));
            Assert.That(itemMaterial.Id, Does.Contain("liquid"));
        }

        [Test]
        public void CountAsVolume_WithLiquidType_ShouldReturnTrue()
        {
            // Act
            var itemMaterial = new ItemMaterial(_testMaterial, MaterialType.Liquid);

            // Assert
            Assert.That(itemMaterial.CountAsVolume, Is.True, "Liquid materials should count as volume");
        }

        [Test]
        public void CountAsVolume_WithGasType_ShouldReturnTrue()
        {
            // Act
            var itemMaterial = new ItemMaterial(_testMaterial, MaterialType.Gas);

            // Assert
            Assert.That(itemMaterial.CountAsVolume, Is.True, "Gas materials should count as volume");
        }

        [Test]
        public void CountAsVolume_WithSolidType_ShouldReturnFalse()
        {
            // Act
            var itemMaterial = new ItemMaterial(_testMaterial, MaterialType.Ingot);

            // Assert
            Assert.That(itemMaterial.CountAsVolume, Is.False, "Solid materials should not count as volume");
        }

        [Test]
        public void CountAsVolume_WithOreType_ShouldReturnFalse()
        {
            // Act
            var itemMaterial = new ItemMaterial(_testMaterial, MaterialType.Ore);

            // Assert
            Assert.That(itemMaterial.CountAsVolume, Is.False, "Ore materials should not count as volume");
        }

        [Test]
        public void Constructor_ShouldSetNameFromMaterialFormNames()
        {
            // Act
            var liquidItem = new ItemMaterial(_testMaterial, MaterialType.Liquid);
            var ingotItem = new ItemMaterial(_testMaterial, MaterialType.Ingot);

            // Assert
            Assert.That(liquidItem.Name, Is.EqualTo(_testMaterial.FormNames[MaterialType.Liquid]));
            Assert.That(ingotItem.Name, Is.EqualTo(_testMaterial.FormNames[MaterialType.Ingot]));
        }

        [Test]
        public void Constructor_ShouldSetDescriptionFromMaterial()
        {
            // Act
            var itemMaterial = new ItemMaterial(_testMaterial, MaterialType.Liquid);

            // Assert
            Assert.That(itemMaterial.Description, Is.EqualTo(_testMaterial.GetDescription().ToString()));
        }

        [Test]
        public void Constructor_ShouldGenerateProperIdFromMaterialAndType()
        {
            // Arrange
            var materialName = _testMaterial.Name.ToLower();
            var typeName = MaterialType.Liquid.ToString().ToLower();

            // Act
            var itemMaterial = new ItemMaterial(_testMaterial, MaterialType.Liquid);

            // Assert - ID should follow the pattern "{material_name}_{type}"
            var expectedId = materialName + "_" + typeName;
            Assert.That(itemMaterial.Id.ToLower(), Is.EqualTo(expectedId));
        }

        [Test]
        public void MultipleTypesFromSameMaterial_ShouldHaveDifferentIds()
        {
            // Act
            var liquidItem = new ItemMaterial(_testMaterial, MaterialType.Liquid);
            var gasItem = new ItemMaterial(_testMaterial, MaterialType.Gas);
            var ingotItem = new ItemMaterial(_testMaterial, MaterialType.Ingot);

            // Assert
            Assert.That(liquidItem.Id, Is.Not.EqualTo(gasItem.Id));
            Assert.That(liquidItem.Id, Is.Not.EqualTo(ingotItem.Id));
            Assert.That(gasItem.Id, Is.Not.EqualTo(ingotItem.Id));
        }

        [Test]
        public void ItemMaterial_ShouldInheritFromItem()
        {
            // Act
            var itemMaterial = new ItemMaterial(_testMaterial, MaterialType.Liquid);

            // Assert
            Assert.That(itemMaterial, Is.InstanceOf<Item>());
        }

        [Test]
        public void Stack_ShouldWorkInherited()
        {
            // Arrange
            var itemMaterial = new ItemMaterial(_testMaterial, MaterialType.Liquid);

            // Act
            var stack = itemMaterial.Stack(100);

            // Assert
            Assert.That(stack.Item, Is.EqualTo(itemMaterial));
            Assert.That(stack.Amount, Is.EqualTo(100));
        }
    }
}