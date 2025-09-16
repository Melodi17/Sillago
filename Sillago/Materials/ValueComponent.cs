namespace Sillago;

using Symbols;

public interface IValueComponent<T>
{
    public int Amount { get; set; }
    public T   Value { get; set; }
}

public class AlloyComponent : IValueComponent<Material>
{
    public Material Value { get; set; }
    public int Amount { get; set; }
    
    public AlloyComponent(Material value, int amount = 1)
    {
        this.Value = value;
        this.Amount = amount;
    }
}

public class CompoundComponent : Symbol, IValueComponent<Symbol>
{
    public Symbol Value { get; set; }
    public int Amount { get; set; }
    public CompoundComponent(Symbol value, int amount = 1)
    {
        this.Value = value;
        this.Amount = amount;
    }
}