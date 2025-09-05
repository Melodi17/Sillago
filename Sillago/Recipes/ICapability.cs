namespace Sillago.Recipes;

public interface ICapability;

/// <summary>
/// Indicates that the Machine supports temperature control.
/// </summary>
public class TemperatureCapability : ICapability
{
    public float Temperature { set; get; }
}