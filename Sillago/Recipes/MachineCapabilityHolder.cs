namespace Sillago.Recipes;

public class MachineCapabilityHolder
{
    private readonly Dictionary<Type, ICapability> _capabilities = new();

    public void Add<T>(T capability) where T : ICapability =>
        this._capabilities[typeof(T)] = capability;

    public T Get<T>() where T : class, ICapability =>
        this._capabilities.TryGetValue(typeof(T), out var c) ? (T)c : throw new KeyNotFoundException($"Machine does not support capability of type {typeof(T).Name}");
}