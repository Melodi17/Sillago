using System.Collections;
using System.Text;

namespace Sillago.Materials;

using Utils;

public abstract class Material : ISymbolizable
{
    public int Color;     // ARGB format, e.g. 0xFF0000FF for blue
    public float Density; // kg/m^3

    // public ConductionProperties ConductionProperties;
    // public FluidHoldingProperties FluidHoldingProperties;

    public string Name;
    public string Symbol { get; set; }
    public MaterialFlags Flags { get; set; }
    public VisualSet VisualSet { get; set; }
    public Dictionary<MaterialType, string> FormNames;
    
    public Material(string name)
    {
        this.FormNames = new()
        {
            { MaterialType.Powder, $"{name} Powder" },
            { MaterialType.Ingot, $"{name} Ingot" },
            { MaterialType.Rod, $"{name} Rod" },
            { MaterialType.Liquid, $"Liquid {name}" },
            { MaterialType.Gas, $"{name} Gas" },
            { MaterialType.Ice, $"Frozen {name}" },
            { MaterialType.Plate, $"{name} Plate" },
            { MaterialType.Wire, $"{name} Wire" },
            { MaterialType.Cable, $"{name} Cable" },
            { MaterialType.Coil, $"{name} Coil" },
            { MaterialType.Ore, $"{name} Ore" },
            { MaterialType.Crystal, $"{name} Crystal" },
            { MaterialType.Culture, $"Culture of {name}" },
        };
    }

    public Material OverrideFormName(MaterialType type, string name)
    {
        if (this.FormNames.ContainsKey(type))
            this.FormNames[type] = name;
        else
            throw new ArgumentException($"Material type {type} not found in {this.Name} forms.");

        return this;
    }

    public abstract IEnumerator Generate();

    protected object Deferred(Action action) => action;

    public bool Is(MaterialFlags flag) => (this.Flags & flag) == flag;

    public virtual StringBuilder GetDescription()
    {
        StringBuilder sb = new();

        // sb.AppendLine($"{this.Symbol}");
        sb.AppendLine($"Density: {this.Density} kg/m³");

        return sb;
    }

    public static AlloyComponent operator *(Material material, int amount)
        => new AlloyComponent(material, amount);
}