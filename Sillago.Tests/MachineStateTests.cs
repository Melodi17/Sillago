namespace Sillago.Tests;

using Helpers;
using Recipes;

[TestFixture]
public class MachineStateTests
{

    [Test]
    public void Get_MissingCapability_Fails()
    {
        MachineState state = new();
        Assert.Throws<KeyNotFoundException>(() => state.Get<DummyCapability>());
    }
    
    [Test]
    public void Get_ExistingCapability_Succeeds()
    {
        MachineState state = new();
        DummyCapability capability = new();
        state.Add(capability);
        
        var retrieved = state.Get<DummyCapability>();
        Assert.That(retrieved, Is.SameAs(capability));
    }
}