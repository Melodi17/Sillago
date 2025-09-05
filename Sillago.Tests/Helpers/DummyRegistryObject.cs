namespace Sillago.Tests;

public class DummyRegistryObject
{
    public string Name { get; set; }

    public DummyRegistryObject(string name)
    {
        this.Name = name;
    }
}

public class DummyRegistryObjectChild : DummyRegistryObject
{
    public string ExtraProperty { get; set; }
    public DummyRegistryObjectChild(string name, string extraProperty) : base(name)
    {
        this.ExtraProperty = extraProperty;
    }
}