namespace Sillago.Tests;

public class DummyRegistry
{
    public static readonly DummyRegistryObject ObjA = new DummyRegistryObject("A");
    public static readonly DummyRegistryObject ObjB = new DummyRegistryObject("B");
    public static readonly DummyRegistryObject ObjC = new DummyRegistryObject("C");
    
    public static readonly DummyRegistryObjectChild ObjD = new DummyRegistryObjectChild("D", "ExtraD");
    public static readonly DummyRegistryObjectChild ObjE = new DummyRegistryObjectChild("E", "ExtraE");
    public static readonly DummyRegistryObjectChild ObjF = new DummyRegistryObjectChild("F", "ExtraF");
}