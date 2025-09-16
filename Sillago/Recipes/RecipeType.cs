namespace Sillago;

using System.Diagnostics.CodeAnalysis;

public struct RecipeType : IEquatable<RecipeType>
{
    public static RecipeType Smelting = new("Smelting", "Smelting", "Furnace");
    public static RecipeType ArcSmelting = new("Arc Smelting", "Arc Smelting", "Arc Furnace");
    public static RecipeType Fusing = new("Fusing", "Fusing", "Foundry");
    public static RecipeType ArcFusing = new("Arc Fusing", "Arc Fusing", "Arc Foundry");
    public static RecipeType Mixing = new("Mixing", "Mixing", "Mixer");
    public static RecipeType Pressing = new("Pressing", "Pressing", "Press");
    public static RecipeType Lathing = new("Lathing", "Lathing", "Lathe");
    public static RecipeType Cutting = new("Cutting", "Cutting", "Cutter");
    public static RecipeType Freezing = new("Freezing", "Freezing", "Freezer");
    public static RecipeType BlastFreezing = new("Blast Freezing", "Blast Freezing", "Blast Freezer");
    public static RecipeType Thawing = new("Thawing", "Thawing", "Thawer");
    public static RecipeType Condensing = new("Condensing", "Condensing", "Condensation Chamber");
    public static RecipeType Vaporising = new("Vaporisation", "Vaporising", "Vaporisation Chamber");
    public static RecipeType Liquification = new("Liquefaction", "Liquefying", "Liquefaction Unit");
    public static RecipeType Incubating = new("Incubation", "Incubating", "Incubator");
    public static RecipeType Macerating = new("Maceration", "Macerating", "Macerator");
    public static RecipeType Distilling = new("Distillation", "Distilling", "Distillation Column");
    public static RecipeType Electrolyzing = new("Electrolysis", "Electrolyzing", "Electrolyzer");
    public static RecipeType Reacting = new("Reaction", "Reacting", "Chemical Reactor");
    public static RecipeType AlloyReacting = new("Alloy Reaction", "Reacting", "Alloy Reactor");
    public static RecipeType Compacting = new("Compacting", "Compacting", "Compactor");
    
    public static RecipeType ChemicalBathing = new("Chemical Bathing", "Bathing", "Chemical Bath");
    public static RecipeType Welding = new("Welding", "Welding", "Computerized Welder");
    public static RecipeType Assembling = new("Assembling", "Assembling", "Assembler");
    public static RecipeType Wiremilling = new("Wiremilling", "Wiremilling", "Wiremill");
    public static RecipeType Casting = new("Casting", "Casting", "Liquid Casting Unit");
    
    public string Noun { get; }
    public string Verb { get; }
    public string Machinery { get; }
    public RecipeType(string noun, string verb, string machinery)
    {
        this.Noun = noun;
        this.Verb = verb;
        this.Machinery = machinery;
    }

    public override string ToString() => this.Noun;
    public bool Equals(RecipeType other) => this.Noun == other.Noun;
    public override bool Equals(object? obj) => obj is RecipeType other && this.Equals(other);
    public override int GetHashCode() => this.Noun.GetHashCode();
    public static bool operator ==(RecipeType left, RecipeType right) => left.Equals(right);
    public static bool operator !=(RecipeType left, RecipeType right) => !left.Equals(right);
}