namespace Sillago.Tests
{
    using Symbols;
    using System.Collections;
    using System.Text;
    using NUnit.Framework;
    using Types;

    [TestFixture]
    public class MaterialBaseTests
    {
        private TestMaterial _testMaterial;

        [SetUp]
        public void SetUp()
        {
            _testMaterial = new TestMaterial("Test Material", Element.H);
        }

        [Test]
        public void Constructor_WithValidParameters_ShouldSucceed()
        {
            // Arrange & Act
            var material = new TestMaterial("Test", Element.C);

            // Assert
            Assert.That(material.Name, Is.EqualTo("Test"));
            Assert.That(material.Symbol, Is.EqualTo(Element.C));
        }

        [Test]
        public void FormNames_ShouldBePopulatedByDefault()
        {
            // Assert - all standard form names should exist
            Assert.That(_testMaterial.FormNames, Is.Not.Empty);
            Assert.That(_testMaterial.FormNames.Count, Is.GreaterThan(10), "Should have many form names");
        
            // Check specific form names
            Assert.That(_testMaterial.FormNames[MaterialType.Powder], Is.EqualTo("Test Material Powder"));
            Assert.That(_testMaterial.FormNames[MaterialType.Ingot], Is.EqualTo("Test Material Ingot"));
            Assert.That(_testMaterial.FormNames[MaterialType.Liquid], Is.EqualTo("Liquid Test Material"));
            Assert.That(_testMaterial.FormNames[MaterialType.Gas], Is.EqualTo("Test Material Gas"));
            Assert.That(_testMaterial.FormNames[MaterialType.Ore], Is.EqualTo("Test Material Ore"));
        }

        [Test]
        public void OverrideFormName_WithValidType_ShouldUpdateName()
        {
            // Act
            var result = _testMaterial.OverrideFormName(MaterialType.Powder, "Custom Powder");

            // Assert
            Assert.That(_testMaterial.FormNames[MaterialType.Powder], Is.EqualTo("Custom Powder"));
            Assert.That(result, Is.SameAs(_testMaterial), "Should return same instance for chaining");
        }

        [Test]
        public void OverrideFormName_WithInvalidType_ShouldThrow()
        {
            // Arrange - create a custom MaterialType that doesn't exist in default FormNames
            var invalidType = (MaterialType)999;

            // Assert
            Assert.That(() => _testMaterial.OverrideFormName(invalidType, "Test Name"),
                Throws.ArgumentException.With.Message.Contains("Material type"));
        }

        [Test]
        public void Is_WithMatchingFlag_ShouldReturnTrue()
        {
            // Arrange
            _testMaterial.TestFlags = MaterialFlags.ElectricallyConductive | MaterialFlags.Ductile;

            // Assert
            Assert.That(_testMaterial.Is(MaterialFlags.ElectricallyConductive), Is.True);
            Assert.That(_testMaterial.Is(MaterialFlags.Ductile), Is.True);
            Assert.That(_testMaterial.Is(MaterialFlags.ElectricallyConductive | MaterialFlags.Ductile), Is.True);
        }

        [Test]
        public void Is_WithNonMatchingFlag_ShouldReturnFalse()
        {
            // Arrange
            _testMaterial.TestFlags = MaterialFlags.ElectricallyConductive;

            // Assert
            Assert.That(_testMaterial.Is(MaterialFlags.Ductile), Is.False);
            Assert.That(_testMaterial.Is(MaterialFlags.Ore), Is.False);
        }

        [Test]
        public void GetDescription_ShouldIncludeDensity()
        {
            // Arrange
            _testMaterial.TestDensity = 1500.5f;

            // Act
            var description = _testMaterial.GetDescription().ToString();

            // Assert
            Assert.That(description, Does.Contain("Density: 1500.5 kg/m³"));
        }

        [Test]
        public void GetDescription_ShouldReturnStringBuilder()
        {
            // Act
            var result = _testMaterial.GetDescription();

            // Assert
            Assert.That(result, Is.InstanceOf<StringBuilder>());
            Assert.That(result.ToString(), Is.Not.Empty);
        }

        [Test]
        public void MultiplyOperator_ShouldCreateAlloyComponent()
        {
            // Act
            var component = _testMaterial * 5;

            // Assert
            Assert.That(component, Is.InstanceOf<AlloyComponent>());
            Assert.That(component.Value, Is.EqualTo(_testMaterial));
            Assert.That(component.Amount, Is.EqualTo(5));
        }

        [Test]
        public void Properties_ShouldHaveCorrectAccessibility()
        {
            // Assert - check that properties are accessible
            Assert.That(_testMaterial.Name, Is.Not.Null);
            Assert.That(_testMaterial.Symbol, Is.Not.Null);
            Assert.That(_testMaterial.FormNames, Is.Not.Null);
            Assert.That(_testMaterial.Color, Is.TypeOf<int>());
            Assert.That(_testMaterial.Density, Is.TypeOf<float>());
            Assert.That(_testMaterial.Flags, Is.TypeOf<MaterialFlags>());
            Assert.That(_testMaterial.VisualSet, Is.Not.EqualTo(default(VisualSet)));
        }

        // Test implementation of abstract Material class
        private class TestMaterial : Material
        {
            public MaterialFlags TestFlags 
            { 
                get => Flags; 
                set => Flags = value; 
            }

            public float TestDensity 
            { 
                get => Density; 
                set => Density = value; 
            }

            public TestMaterial(string name, Symbol symbol) : base(name, symbol)
            {
                Color = 0xFF0000;  // Red
                Density = 1000f;
                Flags = MaterialFlags.None;
                VisualSet = VisualSet.Dull;
            }

            public override IEnumerator Generate()
            {
                // Simple test implementation
                yield return "test";
            }
        }
    }
}