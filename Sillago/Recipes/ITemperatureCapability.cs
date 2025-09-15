namespace Sillago.Recipes;

/// <summary>
/// Indicates that the Machine supports temperature control.
/// </summary>
public interface ITemperatureCapability : ICapability
{
    public float Temperature { get; }
}