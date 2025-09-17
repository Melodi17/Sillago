namespace Sillago.Tests
{
    using System;
    using Helpers;

    [TestFixture]
    public class MachineCapabilityHolderTests
    {

        [Test]
        public void Get_MissingCapability_Fails()
        {
            IMachine machine = new DummyMachine();
            Assert.Throws<InvalidOperationException>(() => machine.Get<IOtherDummyCapability>());
        }
    
        [Test]
        public void Get_ExistingCapability_Succeeds()
        {
            DummyMachine dummy = new DummyMachine();
            IMachine machine = dummy;
        
            var retrieved = machine.Get<IDummyCapability>();
            Assert.That(retrieved, Is.SameAs(dummy));
        }
    }
}