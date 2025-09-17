namespace Sillago.Symbols
{
    public class Compound : Symbol
    {
        public string? Name { get; }
        public readonly CompoundComponent[] Components;
        public Compound(string name, params CompoundComponent[] components)
        {
            this.Name = name;
            this.Components = components;
        }
    
        public Compound(params CompoundComponent[] components)
        {
            this.Name = null;
            this.Components = components;
        }
    }
}