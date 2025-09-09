namespace Sillago.Materials;

using Items;
using Types;

public class Materials
{
    public static MetalMaterial Aluminium = new MetalMaterial(
        "Aluminium",
        0x80C8F0,
        VisualSet.Dull,
        Element.Al,
        MaterialFlags.Ore | MaterialFlags.ElectricallyConductive | MaterialFlags.Ductile,
        2700,
        meltingPoint: 660.32f);

    public static MetalMaterial Copper = new MetalMaterial(
        "Copper",
        0xFF8000,
        VisualSet.Shiny,
        Element.Cu,
        MaterialFlags.Ore | MaterialFlags.ElectricallyConductive | MaterialFlags.Ductile,
        8960,
        meltingPoint: 1084.62f);

    public static MetalMaterial Gold = new MetalMaterial(
        "Gold",
        0xFFFF00,
        VisualSet.Shiny,
        Element.Au,
        MaterialFlags.Ore | MaterialFlags.Ductile | MaterialFlags.ElectricallyConductive,
        19300,
        meltingPoint: 1064.18f);

    public static MetalMaterial Iron = new MetalMaterial(
        "Iron",
        0xAAAAAA,
        VisualSet.Metallic,
        Element.Fe,
        MaterialFlags.Ore | MaterialFlags.ElectricallyConductive | MaterialFlags.Ferromagnetic,
        7874,
        meltingPoint: 1538f);

    public static MetalMaterial Silver = new MetalMaterial(
        "Silver",
        0xDCDCFF,
        VisualSet.Shiny,
        Element.Ag,
        MaterialFlags.Ore | MaterialFlags.Ductile | MaterialFlags.ElectricallyConductive,
        10500,
        meltingPoint: 961.78f);

    public static MetalMaterial Tin = new MetalMaterial(
        "Tin",
        0xD0D0D0,
        VisualSet.Metallic,
        Element.Sn,
        MaterialFlags.Ore
        | MaterialFlags.ElectricallyConductive
        | MaterialFlags.Ductile
        | MaterialFlags.CorrosionResistant,
        7310,
        meltingPoint: 231.93f);

    public static MetalMaterial Chromium = new MetalMaterial(
        "Chromium",
        0xB0B0B0,
        VisualSet.Metallic,
        Element.Cr,
        MaterialFlags.Ore | MaterialFlags.ElectricallyConductive | MaterialFlags.CorrosionResistant,
        7190,
        meltingPoint: 1907f);

    public static MetalMaterial Manganese = new MetalMaterial(
        "Manganese",
        0x69785c,
        VisualSet.Metallic,
        Element.Mn,
        MaterialFlags.Ore | MaterialFlags.ElectricallyConductive | MaterialFlags.Brittle,
        7440,
        meltingPoint: 1246f);

    public static MetalMaterial Zinc = new MetalMaterial(
        "Zinc",
        0x7F7F7F,
        VisualSet.Metallic,
        Element.Zn,
        MaterialFlags.Ore
        | MaterialFlags.ElectricallyConductive
        | MaterialFlags.Ductile
        | MaterialFlags.Toxic,
        7135,
        meltingPoint: 419.58f);


    public static MetalMaterial Cobalt = new MetalMaterial(
        "Cobalt",
        0x0047AB,
        VisualSet.Metallic,
        Element.Co,
        MaterialFlags.Ore | MaterialFlags.ElectricallyConductive | MaterialFlags.Ferromagnetic,
        8900,
        meltingPoint: 1495f);

    public static MetalMaterial Tungsten = new MetalMaterial(
        "Tungsten",
        0x2D3631,
        VisualSet.Metallic,
        Element.W,
        MaterialFlags.Ore | MaterialFlags.ElectricallyConductive | MaterialFlags.Brittle,
        19300,
        meltingPoint: 3422f);

    public static CrystallineMaterial Sulphur = new CrystallineMaterial(
        "Sulphur",
        0xFFFF00,
        VisualSet.Rough,
        Element.S,
        MaterialFlags.Ore | MaterialFlags.Brittle | MaterialFlags.Flammable,
        2070,
        liquificationPoint: 115.21f);

    public static PowderMaterial Coal = new PowderMaterial(
        "Coal",
        0x333333,
        VisualSet.Rough,
        Element.C,
        MaterialFlags.Ore | MaterialFlags.Brittle | MaterialFlags.Flammable,
        1500,
        liquificationPoint: 1200f);

    public static PowderMaterial Ash = new PowderMaterial(
        "Ash",
        0xB0B0B0,
        VisualSet.Rough,
        new Compound("Ash", Element.C * 1, Element.O * 2, Element.H * 1),
        MaterialFlags.Brittle,
        500f
    );

    public static Material Latex = new IngotMaterial(
        "Latex",
        0xFFB6C1,
        VisualSet.Rough,
        new Compound("Polyisoprene", Element.C * 5, Element.H * 8).Polymer(),
        MaterialFlags.Flexible | MaterialFlags.Flammable,
        density: 920f,
        meltingPoint: 300f
    ).OverrideFormName(MaterialType.Liquid, "Liquid Latex");

    public static Material Rubber = new IngotMaterial(
            "Rubber",
            0x008000,
            VisualSet.Rough,
            new Compound(
                "Vulcanized Rubber",
                Element.C * 5,
                Element.H * 8,
                Element.S * 1).Polymer(),
            MaterialFlags.Flexible | MaterialFlags.Flammable,
            density: 1100f,
            meltingPoint: 400f
        )
        .OverrideFormName(MaterialType.Ingot, "Rubber Block")
        .OverrideFormName(MaterialType.Plate, "Rubber Sheet")
        .OverrideFormName(MaterialType.Rod, "Rubber Rod");

    public static FluidMaterial CrudeOil = new FluidMaterial(
        "Crude Oil",
        0x000000,
        VisualSet.Rough,
        new Compound("Petroleum", Element.C * 10, Element.H * 22),
        MaterialFlags.Flammable | MaterialFlags.Toxic,
        density: 870f,
        freezingPoint: -60f,
        vaporisationPoint: 350f
    );

    public static FluidMaterial Petroleum = new FluidMaterial(
        "Petroleum",
        0x333333,
        VisualSet.Rough,
        new Compound("Refined Petroleum", Element.C * 10, Element.H * 22),
        MaterialFlags.Flammable | MaterialFlags.Toxic,
        density: 800f,
        freezingPoint: -50f,
        vaporisationPoint: 300f
    );

    public static Material Glass = new IngotMaterial(
            "Glass",
            0xC0C0C0,
            VisualSet.Shiny,
            new Compound("Silica", Element.Si * 1, Element.O * 2),
            MaterialFlags.Brittle | MaterialFlags.ElectricallyInsulating,
            density: 2500f,
            meltingPoint: 1700f
        )
        .OverrideFormName(MaterialType.Ingot, "Glass Billet")
        .OverrideFormName(MaterialType.Plate, "Glass Sheet")
        .OverrideFormName(MaterialType.Rod, "Glass Cane");

    public static BioMaterial Lactobacillus = new BioMaterial(
        "Lactobacillus",
        variation: 1,
        MaterialFlags.Toxic,
        minTemp: 10f,
        maxTemp: 70f,
        Materials.Sulphur
    );

    public static Material Water = new FluidMaterial(
            "Water",
            0x0000FF,
            VisualSet.Shiny,
            new Compound(
                "Impure Water",
                Element.H  * 2,
                Element.O  * 1,
                Element.Ca * 1,
                Element.Cl * 1),
            MaterialFlags.ElectricallyConductive | MaterialFlags.ThermallyConductive,
            density: 1000f,
            freezingPoint: 0f,
            vaporisationPoint: 100f
        )
        .OverrideFormName(MaterialType.Liquid, "Water")
        .OverrideFormName(MaterialType.Ice, "Ice")
        .OverrideFormName(MaterialType.Gas, "Steam");

    public static Material DistilledWater = new FluidMaterial(
            "Distilled Water",
            0x0099FF,
            VisualSet.Shiny,
            new Compound("Pure Water", Element.H * 2, Element.O * 1),
            MaterialFlags.ThermallyConductive,
            density: 1000f,
            freezingPoint: 0f,
            vaporisationPoint: 100f
        )
        .OverrideFormName(MaterialType.Liquid, "Distilled Water")
        .OverrideFormName(MaterialType.Ice, "Distilled Ice")
        .OverrideFormName(MaterialType.Gas, "Distilled Steam");


    public static Alloy Bronze = new Alloy("Bronze", Materials.Tin * 1, Materials.Copper * 3);
    public static Alloy Brass = new Alloy("Brass", Materials.Zinc  * 2, Materials.Copper * 3);

    public static Alloy HCSteel = new Alloy(
        "High-Carbon Steel",
        Materials.Iron * 3,
        Materials.Coal * 1);

    public static Alloy ToolSteel = new Alloy(
        "Tool Steel",
        Materials.HCSteel  * 7,
        Materials.Tungsten * 2,
        Materials.Cobalt   * 1);

    public static Alloy StainlessSteel = new Alloy(
        "Stainless Steel",
        Materials.HCSteel   * 7,
        Materials.Manganese * 1,
        Materials.Chromium  * 2);

    public static Alloy Electrum = new Alloy(
        "Electrum",
        Materials.Gold   * 3,
        Materials.Silver * 2,
        Materials.Copper * 1);

    internal static void GenerateLinkingRecipes()
    {
        var waterFluid = Items.GetMaterial(Materials.Water, MaterialType.Liquid);
        var distilledFluid = Items.GetMaterial(Materials.DistilledWater, MaterialType.Liquid);

        waterFluid.DistillsInto(100, [distilledFluid.Stack(90)]);

        var crudeOilFluid = Items.GetMaterial(Materials.CrudeOil, MaterialType.Liquid);
        var petroleumFluid = Items.GetMaterial(Materials.Petroleum, MaterialType.Liquid);
        var latexFluid = Items.GetMaterial(Materials.Latex, MaterialType.Liquid);
        var sulfurPowder = Items.GetMaterial(Materials.Sulphur, MaterialType.Powder);
        var coalPowder = Items.GetMaterial(Materials.Coal, MaterialType.Powder);
        var rubberIngot = Items.GetMaterial(Materials.Rubber, MaterialType.Ingot);
    }
}