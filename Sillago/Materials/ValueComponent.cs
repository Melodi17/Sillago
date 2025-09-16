namespace Sillago;

using Sillago.Symbols;

public class ValueComponent<T>
{
    public int Amount;
    public T   Value;

    public ValueComponent(T value, int amount = 1)
    {
        this.Value  = value;
        this.Amount = amount;
    }
}

public class CompoundComponent : ValueComponent<Symbol>
{
    public Symbol Component => Value;
    public int Count => Amount;
    
    public CompoundComponent(Symbol value, int amount = 1) : base(value, amount)
    {
        
    }
}

public class AlloyComponent : ValueComponent<Material>
{
    public AlloyComponent(Material value, int amount = 1) : base(value, amount)
    {
        
    }
}