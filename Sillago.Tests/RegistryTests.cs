namespace Sillago.Tests;

using Utils;

[TestFixture]
public class RegistryTests
{

    [Test]
    public void GetAllEntries_ReturnsAllOfType()
    {
        var entries = Registry.GetAllEntries<DummyRegistryObject, DummyRegistry>();
        Assert.
    }
}