namespace Sillago.Tests;

using Helpers;
using Recipes;

[TestFixture]
public class MachineCapabilityHolderTests
{

    [Test]
    public void Get_MissingCapability_Fails()
    {
        MachineCapabilityHolder capabilityHolder = new();
        Assert.Throws<KeyNotFoundException>(() => capabilityHolder.Get<DummyCapability>());
    }
    
    [Test]
    public void Get_ExistingCapability_Succeeds()
    {
        MachineCapabilityHolder capabilityHolder = new();
        DummyCapability capability = new();
        capabilityHolder.Add(capability);
        
        var retrieved = capabilityHolder.Get<DummyCapability>();
        Assert.That(retrieved, Is.SameAs(capability));
    }
}