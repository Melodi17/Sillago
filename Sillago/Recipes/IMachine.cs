namespace Sillago.Recipes;

public interface IMachine
{
    public T Get<T>()
        where T : ICapability
    {
        if (this is not T)
            throw new InvalidOperationException($"Machine does not support capability {typeof(T).Name}.");
        
        return (T)this;
    }
}