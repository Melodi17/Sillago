namespace Sillago;

using System.Collections;
using System.Text;
using Symbols;

public abstract class Material
{
    /// <summary>
    /// Uses a 0xRRGGBB format.
    /// </summary>
    public int Color { get; protected set; }

    /// <summary>
    /// The density of the material in kilograms per cubic meter (kg/m³).
    /// </summary>
    public float Density { get; protected set; }

    // public ConductionProperties ConductionProperties;
    // public FluidHoldingProperties FluidHoldingProperties;

    public string Name { get; private set; }
    public Symbol Symbol { get; private set; }

    /// <summary>
    /// Flags that define the properties and behaviors of the material.
    /// </summary>
    public MaterialFlags Flags { get; protected set; }

    /// <summary>
    /// Visual set for rendering the material in different forms.
    /// </summary>
    public VisualSet VisualSet { get; protected set; }

    /// <summary>
    /// Form names for different material states, e.g., Powder, Ingot, Liquid, etc.
    /// </summary>
    public Dictionary<MaterialType, string> FormNames { get; }

    public Material(string name, Symbol symbol)
    {
        this.Name = name;
        this.Symbol = symbol;
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
            { MaterialType.HotIngot, $"Hot {name} Ingot" },
            { MaterialType.FinePowder, $"Fine {name} Powder" },
            { MaterialType.Nugget, $"{name} Nugget" },
            { MaterialType.FineWire, $"Fine {name} Wire" }
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

    public static AlloyComponent operator *(Material material, int amount) => new AlloyComponent(material, amount);
}