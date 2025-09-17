namespace Sillago.Tests
{
    using System.Collections.Generic;
    using Helpers;
    using Utils;

    [TestFixture]
    public class RegistryTests
    {
        [Test]
        public void GetAllEntries_ReturnsAllOfTypeAndSubtypes()
        {
            var entries = Registry.GetAllEntries<DummyRegistryObject, DummyRegistry>();
            var expected = new List<DummyRegistryObject>
            {
                DummyRegistry.ObjA,
                DummyRegistry.ObjB,
                DummyRegistry.ObjC,
                DummyRegistry.ObjD,
                DummyRegistry.ObjE,
                DummyRegistry.ObjF
            };
            Assert.That(entries, Is.EquivalentTo(expected));
        }
    
        [Test]
        public void GetEntryByName_ReturnsCorrectEntry()
        {
            var entry = Registry.GetEntry<DummyRegistryObject, DummyRegistry>("ObjB");
            Assert.That(entry, Is.EqualTo(DummyRegistry.ObjB));
        }
    
        [Test]
        public void GetEntryByName_IgnoresCaseWhenSpecified()
        {
            var entry = Registry.GetEntry<DummyRegistryObject, DummyRegistry>("objc", ignoreCase: true);
            Assert.That(entry, Is.EqualTo(DummyRegistry.ObjC));
        }
    
        [Test]
        public void GetEntryByName_ReturnsNullIfNotFound()
        {
            var entry = Registry.GetEntry<DummyRegistryObject, DummyRegistry>("NonExistent");
            Assert.That(entry, Is.Null);
        }
    }
}