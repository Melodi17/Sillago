namespace Sillago.Symbols
{
    public abstract class Symbol
    {
        public static CompoundComponent operator *(Symbol element, int amount) => new(element, amount);
    }
}