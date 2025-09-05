namespace Sillago.Materials;

using Types;

public class Materials
{
    public static MetalMaterial Aluminium = new MetalMaterial("Aluminium", 0x80C8F0, VisualSet.Dull,     Element.Al, MaterialFlags.Ore | MaterialFlags.ElectricallyConductive | MaterialFlags.Ductile,                                    2700,  meltingPoint: 660.32f);
    public static MetalMaterial Copper    = new MetalMaterial("Copper",    0xFF8000, VisualSet.Shiny,    Element.Cu, MaterialFlags.Ore | MaterialFlags.ElectricallyConductive | MaterialFlags.Ductile,                                    8960,  meltingPoint: 1084.62f);
    public static MetalMaterial Gold      = new MetalMaterial("Gold",      0xFFFF00, VisualSet.Shiny,    Element.Au, MaterialFlags.Ore | MaterialFlags.Ductile                | MaterialFlags.ElectricallyConductive,                                 19300, meltingPoint: 1064.18f);
    public static MetalMaterial Iron      = new MetalMaterial("Iron",      0xAAAAAA, VisualSet.Metallic, Element.Fe, MaterialFlags.Ore | MaterialFlags.ElectricallyConductive | MaterialFlags.Ferromagnetic,                              7874,  meltingPoint: 1538f);
    public static MetalMaterial Silver    = new MetalMaterial("Silver",    0xDCDCFF, VisualSet.Shiny,    Element.Ag, MaterialFlags.Ore | MaterialFlags.Ductile                | MaterialFlags.ElectricallyConductive,                                 10500, meltingPoint: 961.78f);
    public static MetalMaterial Tin       = new MetalMaterial("Tin",       0xD0D0D0, VisualSet.Metallic, Element.Sn, MaterialFlags.Ore | MaterialFlags.ElectricallyConductive | MaterialFlags.Ductile | MaterialFlags.CorrosionResistant, 7310,  meltingPoint: 231.93f);
    public static MetalMaterial Chromium  = new MetalMaterial("Chromium",  0xB0B0B0, VisualSet.Metallic, Element.Cr, MaterialFlags.Ore | MaterialFlags.ElectricallyConductive | MaterialFlags.CorrosionResistant,                         7190,  meltingPoint: 1907f);
    public static MetalMaterial Manganese = new MetalMaterial("Manganese", 0x69785c, VisualSet.Metallic, Element.Mn, MaterialFlags.Ore | MaterialFlags.ElectricallyConductive | MaterialFlags.Brittle,                                    7440,  meltingPoint: 1246f);
    public static MetalMaterial Zinc      = new MetalMaterial("Zinc",      0x7F7F7F, VisualSet.Metallic, Element.Zn, MaterialFlags.Ore | MaterialFlags.ElectricallyConductive | MaterialFlags.Ductile | MaterialFlags.Toxic,              7135,  meltingPoint: 419.58f);

    public static CrystallineMaterial Sulphur = new CrystallineMaterial("Sulphur", 0xFFFF00, VisualSet.Rough, Element.S, MaterialFlags.Ore | MaterialFlags.Brittle | MaterialFlags.Flammable, 2070, liquificationPoint: 115.21f);
    public static PowderMaterial      Coal    = new PowderMaterial("Coal", 0x333333, VisualSet.Rough, Element.C, MaterialFlags.Ore         | MaterialFlags.Brittle | MaterialFlags.Flammable, 1500, liquificationPoint: 1200f);

    public static PowderMaterial Ash = new PowderMaterial(
        "Ash",
        0xB0B0B0,
        VisualSet.Rough,
        new Compound("Ash", Element.C * 1, Element.O * 2, Element.H * 1),
        MaterialFlags.Brittle,
        500f
    );

    public static IngotMaterial Latex = new IngotMaterial(
        "Latex",
        0xFFB6C1,
        VisualSet.Rough,
        new Compound("Polyisoprene", Element.C * 5, Element.H * 8).Polymer(),
        MaterialFlags.Flexible | MaterialFlags.Flammable,
        920f,
        300f
    );

    public static FluidMaterial CrudeOil = new FluidMaterial(
        "Crude Oil",
        0x000000,
        VisualSet.Rough,
        new Compound("Petroleum", Element.C * 10, Element.H * 22),
        MaterialFlags.Flammable | MaterialFlags.Toxic,
        870f,
        -60f,
        350f
    );

    public static IngotMaterial Glass = new IngotMaterial(
        "Glass",
        0xC0C0C0,
        VisualSet.Shiny,
        new Compound("Silica", Element.Si * 1, Element.O * 2),
        MaterialFlags.Brittle | MaterialFlags.ElectricallyInsulating,
        2500f,
        1700f
    );

    public static BioMaterial Lactobacillus = new BioMaterial(
        "Lactobacillus",
        1,
        MaterialFlags.Toxic,
        10f,
        70f,
        Materials.Sulphur
    );
    
    public static Material Water = new FluidMaterial(
            "Water",
            0x0000FF,
            VisualSet.Shiny,
            new Compound("Impure Water", Element.H * 2, Element.O * 1, Element.Ca * 1, Element.Cl * 1),
            MaterialFlags.ElectricallyConductive | MaterialFlags.ThermallyConductive,
            1000f,
            0f,
            100f
        ).OverrideFormName(MaterialType.Liquid, "Water")
        .OverrideFormName(MaterialType.Ice, "Ice")
        .OverrideFormName(MaterialType.Gas, "Steam");
    
    public static Material DistilledWater = new FluidMaterial(
            "Distilled Water",
            0x0099FF,
            VisualSet.Shiny,
            new Compound("Pure Water", Element.H * 2, Element.O * 1),
            MaterialFlags.ThermallyConductive,
            1000f,
            0f,
            100f
        ).OverrideFormName(MaterialType.Liquid, "Distilled Water")
        .OverrideFormName(MaterialType.Ice, "Distilled Ice")
        .OverrideFormName(MaterialType.Gas, "Distilled Steam");

    public static Alloy Steel          = new Alloy("High-Carbon Steel", Materials.Iron  * 3, Materials.Coal      * 1);
    public static Alloy Bronze         = new Alloy("Bronze",            Materials.Tin   * 1, Materials.Copper    * 3);
    public static Alloy Brass          = new Alloy("Brass",             Materials.Zinc  * 2, Materials.Copper    * 3);
    public static Alloy StainlessSteel = new Alloy("Stainless Steel",   Materials.Steel * 7, Materials.Manganese * 1, Materials.Chromium * 2);
    public static Alloy Electrum       = new Alloy("Electrum",          Materials.Gold  * 3, Materials.Silver    * 2, Materials.Copper   * 1);
}