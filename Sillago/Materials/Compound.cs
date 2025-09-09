namespace Sillago.Materials;

using Utils;

public class Compound : Symbol
{
    public string Name { get; }
    public readonly CompoundComponent[] Components;
    public Compound(string name, params CompoundComponent[] components)
    {
        this.Name = name;
        this.Components = components;
    }
}