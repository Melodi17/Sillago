namespace Sillago.Tests.Helpers
{
    public class DummyRegistry
    {
        public static readonly DummyRegistryObject ObjA = new();
        public static readonly DummyRegistryObject ObjB = new();
        public static readonly DummyRegistryObject ObjC = new();
    
        public static readonly DummyRegistryObjectChild ObjD = new();
        public static readonly DummyRegistryObjectChild ObjE = new();
        public static readonly DummyRegistryObjectChild ObjF = new();
    }
}