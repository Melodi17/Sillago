namespace ConsoleInterface;

using Sillago.Recipes;

public class MyMachine : IMachine, ITemperatureCapability
{
    public MyMachine()
    {
        
    }
    public float Temperature { get; }
}