namespace Sillago.Recipes;

using Materials;

public interface IMoldCapability : ICapability
{
    MaterialType MoldType { get; }
}