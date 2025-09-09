using System.Collections;
using System.Text;

namespace Sillago.Materials.Types;

using Items;
using Utils;

public class Alloy : IngotMaterial
{
    public AlloyComponent[] Components;
    public Alloy(string name, params AlloyComponent[] components)
        : base(name)
    {
        this.Components = components;
        this.Name = name;
        this.Symbol = new Compound(name, components.Select(x => new CompoundComponent(x.Value.Symbol, x.Amount)).ToArray());

        // split int color into ARGB components, then perform weighted sum
        this.Color = components.WeightedColorwiseSum(x => x.Amount, x => x.Value.Color);
        this.VisualSet = components.MaxBy(x => x.Amount)!.Value.VisualSet;

        this.Flags = Alloy.CalculateFlags(components);

        this.MeltingPoint = components
            .Where(x => x.Value is IngotMaterial)
            .WeightedSum(x => x.Amount, x => (x.Value as IngotMaterial)!.MeltingPoint);
        this.Density = components.WeightedSum(x => x.Amount, x => x.Value.Density);
    }

    private static MaterialFlags CalculateFlags(AlloyComponent[] components)
    {
        MaterialFlags flags = MaterialFlags.None;
        float totalAmount = components.Sum(x => x.Amount);
        foreach (AlloyComponent component in components)
        {
            MaterialFlags componentFlags = component.Value.Flags;
            bool keepRecessiveFlags = component.Amount / totalAmount > 0.5f;
            if (!keepRecessiveFlags)
                componentFlags &= ~MaterialFlags.Recessive;
            componentFlags &= ~MaterialFlags.Ephemeral;

            flags |= componentFlags;
        }

        if (flags.Is(MaterialFlags.ElectricallyConductive)
            && flags.Is(MaterialFlags.ElectricallyInsulating))
        {
            // If an alloy is both conductive and insulating, it is neither
            flags &= ~(MaterialFlags.ElectricallyConductive | MaterialFlags.ElectricallyInsulating);
        }

        if (flags.Is(MaterialFlags.ThermallyConductive)
            && flags.Is(MaterialFlags.ThermallyInsulating))
        {
            // If an alloy is both thermally conductive and thermally insulating, it is neither
            flags &= ~(MaterialFlags.ThermallyConductive | MaterialFlags.ThermallyInsulating);
        }

        if (flags.Is(MaterialFlags.Brittle) && flags.Is(MaterialFlags.Ductile))
        {
            // If an alloy is both brittle and ductile, it is neither
            flags &= ~(MaterialFlags.Brittle | MaterialFlags.Ductile);
        }

        return flags;
    }

    public override StringBuilder GetDescription()
    {
        StringBuilder sb = base.GetDescription();
        sb.AppendLine("Alloy Components:");
        float totalAmount = this.Components.Sum(x => x.Amount);
        foreach (AlloyComponent component in this.Components.OrderByDescending(x => x.Amount))
        {
            float percentage = component.Amount / totalAmount * 100;
            sb.AppendLine($"  - {component.Value.Name} {percentage:0.0}%");
        }
        return sb;
    }

    public override IEnumerator Generate()
    {
        yield return base.Generate();

        yield return this.Deferred(() => Items
            .GetMaterial(this, MaterialType.Powder)
            .MixesFrom(
                this
                    .Components
                    .Select(x => Items
                        .GetMaterial(x.Value, MaterialType.Powder)
                        .Stack(x.Amount))
                    .ToList(),
                this.Components.Sum(x => x.Amount)));

        yield return this.Deferred(() => Items
            .GetMaterial(this, MaterialType.Ingot)
            .FusesFrom(
                this
                    .Components
                    .Select(x =>
                        (Items.TryGetMaterial(x.Value, MaterialType.Ingot)
                         ?? Items.GetMaterial(x.Value, MaterialType.Powder))
                        .Stack(x.Amount))
                    .ToList(),
                this.MeltingPoint / 3f,
                this.Components.Sum(x => x.Amount)));

        yield return this.Deferred(() => Items
            .GetMaterial(this, MaterialType.Ingot)
            .FusesFrom(
                this
                    .Components
                    .Select(x => Items
                        .GetMaterial(x.Value, MaterialType.Powder)
                        .Stack(x.Amount))
                    .ToList(),
                this.MeltingPoint / 3f,
                this.Components.Sum(x => x.Amount)));
    }
}