// This file is auto-generated. Do not edit manually.
namespace Sillago;

using Sillago.Symbols;
using Types;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public partial class Materials
{
    public static Material Aluminium = new MetalMaterial(
        "Aluminium",
        0x80C8F0,
        VisualSet.Dull,
        Element.Al,
        MaterialFlags.Ore | MaterialFlags.ElectricallyConductive | MaterialFlags.Ductile,
        density: 2700f,
        meltingPoint: 660.32f);
        
    public static Material Copper = new MetalMaterial(
        "Copper",
        0xFF8000,
        VisualSet.Shiny,
        Element.Cu,
        MaterialFlags.Ore | MaterialFlags.ElectricallyConductive | MaterialFlags.Ductile,
        density: 8960f,
        meltingPoint: 1084.62f);
        
    public static Material Gold = new MetalMaterial(
        "Gold",
        0xFFFF00,
        VisualSet.Shiny,
        Element.Au,
        MaterialFlags.Ore | MaterialFlags.ElectricallyConductive | MaterialFlags.Ductile,
        density: 19300f,
        meltingPoint: 1064.18f);
        
    public static Material Iron = new MetalMaterial(
        "Iron",
        0xAAAAAA,
        VisualSet.Metallic,
        Element.Fe,
        MaterialFlags.Ore | MaterialFlags.ElectricallyConductive | MaterialFlags.Ferromagnetic,
        density: 7874f,
        meltingPoint: 1538f);
        
    public static Material Silver = new MetalMaterial(
        "Silver",
        0xDCDCFF,
        VisualSet.Shiny,
        Element.Ag,
        MaterialFlags.Ore | MaterialFlags.ElectricallyConductive | MaterialFlags.Ductile,
        density: 10500f,
        meltingPoint: 961.78f);
        
    public static Material Tin = new MetalMaterial(
        "Tin",
        0xD0D0D0,
        VisualSet.Metallic,
        Element.Sn,
        MaterialFlags.Ore | MaterialFlags.ElectricallyConductive | MaterialFlags.Ductile | MaterialFlags.CorrosionResistant,
        density: 7310f,
        meltingPoint: 231.93f);
        
    public static Material Chromium = new MetalMaterial(
        "Chromium",
        0xB0B0B0,
        VisualSet.Metallic,
        Element.Cr,
        MaterialFlags.Ore | MaterialFlags.ElectricallyConductive | MaterialFlags.CorrosionResistant,
        density: 7190f,
        meltingPoint: 1907f);
        
    public static Material Manganese = new MetalMaterial(
        "Manganese",
        0x69785C,
        VisualSet.Metallic,
        Element.Mn,
        MaterialFlags.Ore | MaterialFlags.ElectricallyConductive | MaterialFlags.Brittle,
        density: 7440f,
        meltingPoint: 1246f);
        
    public static Material Zinc = new MetalMaterial(
        "Zinc",
        0x7F7F7F,
        VisualSet.Metallic,
        Element.Zn,
        MaterialFlags.Ore | MaterialFlags.ElectricallyConductive | MaterialFlags.Ductile | MaterialFlags.Toxic,
        density: 7135f,
        meltingPoint: 419.58f);
        
    public static Material Cobalt = new MetalMaterial(
        "Cobalt",
        0x0047AB,
        VisualSet.Metallic,
        Element.Co,
        MaterialFlags.Ore | MaterialFlags.ElectricallyConductive | MaterialFlags.Ferromagnetic,
        density: 8900f,
        meltingPoint: 1495f);
        
    public static Material Tungsten = new MetalMaterial(
        "Tungsten",
        0x2D3631,
        VisualSet.Metallic,
        Element.W,
        MaterialFlags.Ore | MaterialFlags.ElectricallyConductive | MaterialFlags.Brittle,
        density: 19300f,
        meltingPoint: 3422f);
        
    public static Material NaturalLatex = new IngotMaterial(
        "Natural Latex",
        0xFFB6C1,
        VisualSet.Rough,
        new Compound(Element.C * 5, Element.H * 8).Polymer(),
        MaterialFlags.Flexible | MaterialFlags.Flammable,
        density: 920f,
        meltingPoint: 300f);
        
    public static Material Rubber = new IngotMaterial(
        "Rubber",
        0x008000,
        VisualSet.Rough,
        new Compound(Element.C * 5, Element.H * 8, Element.S * 1).Polymer(),
        MaterialFlags.Flexible | MaterialFlags.Flammable,
        density: 1100f,
        meltingPoint: 400f)
            .OverrideFormName(MaterialType.Ingot, "Rubber Block")
            .OverrideFormName(MaterialType.Plate, "Rubber Sheet")
            .OverrideFormName(MaterialType.Rod, "Rubber Rod");
        
    public static Material Glass = new IngotMaterial(
        "Glass",
        0xC0C0C0,
        VisualSet.Shiny,
        new Compound(Element.Si * 1, Element.O * 2),
        MaterialFlags.Brittle | MaterialFlags.ElectricallyInsulating,
        density: 2500f,
        meltingPoint: 1700f)
            .OverrideFormName(MaterialType.Ingot, "Glass Billet")
            .OverrideFormName(MaterialType.Plate, "Glass Sheet")
            .OverrideFormName(MaterialType.Rod, "Glass Cane");
        
    public static Material Asphalt = new IngotMaterial(
        "Asphalt",
        0x3F3939,
        VisualSet.Rough,
        new Compound(Element.C * 70, Element.H * 100),
        MaterialFlags.Flammable | MaterialFlags.Viscous,
        density: 1050f,
        meltingPoint: 120f)
            .OverrideFormName(MaterialType.Ingot, "Asphalt Brick");
        
    public static Material Platinum = new MetalMaterial(
        "Platinum",
        0xE5E4E2,
        VisualSet.Shiny,
        Element.Pt,
        MaterialFlags.Ore | MaterialFlags.CorrosionResistant | MaterialFlags.ThermallyConductive | MaterialFlags.ElectricallyConductive | MaterialFlags.Ductile,
        density: 21450f,
        meltingPoint: 1768f);
        
    public static Material Polystyrene = new IngotMaterial(
        "Polystyrene",
        0xFF9999,
        VisualSet.Dull,
        new Compound(Element.C * 8, Element.H * 8).Polymer(),
        MaterialFlags.ThermallyInsulating,
        density: 1050f,
        meltingPoint: 90f);
        
    public static Material Polyethylene = new IngotMaterial(
        "Polyethylene",
        0x77AA99,
        VisualSet.Dull,
        new Compound(Element.C * 2, Element.H * 4).Polymer(),
        MaterialFlags.ThermallyInsulating,
        density: 950f,
        meltingPoint: 300f);
        
    public static Material Sulphur = new CrystallineMaterial(
        "Sulphur",
        0xFFFF00,
        VisualSet.Rough,
        Element.S,
        MaterialFlags.Ore | MaterialFlags.Brittle | MaterialFlags.Flammable,
        density: 2070f,
        liquificationPoint: 115.21f);
    
    public static Material Coal = new PowderMaterial(
        "Coal",
        0x333333,
        VisualSet.Rough,
        Element.C,
        MaterialFlags.Ore | MaterialFlags.Brittle | MaterialFlags.Flammable,
        density: 1500f,
        liquificationPoint: 1200f);
    
    public static Material Ash = new PowderMaterial(
        "Ash",
        0xB0B0B0,
        VisualSet.Rough,
        new Compound(Element.C * 1, Element.H * 1, Element.O * 2),
        MaterialFlags.Brittle,
        density: 500f,
        liquificationPoint: null);
    
    public static Material Water = new FluidMaterial(
        "Water",
        0x0000FF,
        VisualSet.Shiny,
        new Compound(Element.H * 2, Element.O * 1, Element.Ca * 1, Element.Cl * 1),
        MaterialFlags.ElectricallyConductive | MaterialFlags.ThermallyConductive,
        density: 1000f,
        freezingPoint: 0f,
        vaporisationPoint: 100f)
            .OverrideFormName(MaterialType.Liquid, "Water")
            .OverrideFormName(MaterialType.Ice, "Ice")
            .OverrideFormName(MaterialType.Gas, "Steam");
    
    public static Material DistilledWater = new FluidMaterial(
        "Distilled Water",
        0x0099FF,
        VisualSet.Dull,
        new Compound(Element.H * 2, Element.O * 1),
        MaterialFlags.ThermallyConductive,
        density: 1000f,
        freezingPoint: 0f,
        vaporisationPoint: 100f)
            .OverrideFormName(MaterialType.Liquid, "Distilled Water")
            .OverrideFormName(MaterialType.Ice, "Distilled Ice")
            .OverrideFormName(MaterialType.Gas, "Distilled Steam");
    
    public static Material CrudeOil = new FluidMaterial(
        "Crude Oil",
        0x000000,
        VisualSet.Rough,
        new Compound(Element.C * 10, Element.H * 22),
        MaterialFlags.Flammable | MaterialFlags.Toxic,
        density: 870f,
        freezingPoint: -60f,
        vaporisationPoint: 350f);
    
    public static Material Petroleum = new FluidMaterial(
        "Petroleum",
        0x333333,
        VisualSet.Rough,
        new Compound(Element.C * 8, Element.H * 9),
        MaterialFlags.Flammable | MaterialFlags.Toxic,
        density: 800f,
        freezingPoint: -50f,
        vaporisationPoint: 300f);
    
    public static Material Oxygen = new FluidMaterial(
        "Oxygen",
        0xD3F0FF,
        VisualSet.Default,
        Element.O,
        MaterialFlags.Flammable | MaterialFlags.Toxic,
        density: 1429f,
        freezingPoint: -218.79f,
        vaporisationPoint: -182.96f);
    
    public static Material Hydrogen = new FluidMaterial(
        "Hydrogen",
        0x8EA8B4,
        VisualSet.Default,
        Element.H,
        MaterialFlags.Flammable | MaterialFlags.Toxic,
        density: 89f,
        freezingPoint: -259.16f,
        vaporisationPoint: -252.16f);
    
    public static Material Nitrogen = new FluidMaterial(
        "Nitrogen",
        0x69B3B8,
        VisualSet.Default,
        Element.N,
        MaterialFlags.Toxic,
        density: 1250f,
        freezingPoint: -209.86f,
        vaporisationPoint: -195.8f);
    
    public static Material Propane = new FluidMaterial(
        "Propane",
        0xC67D46,
        VisualSet.Default,
        new Compound(Element.C * 3, Element.H * 8),
        MaterialFlags.Flammable | MaterialFlags.Toxic,
        density: 493f,
        freezingPoint: -187.7f,
        vaporisationPoint: -42.1f);
    
    public static Material Naphtha = new FluidMaterial(
        "Naphtha",
        0x666633,
        VisualSet.Rough,
        new Compound(Element.C * 7, Element.H * 16),
        MaterialFlags.Flammable | MaterialFlags.Toxic,
        density: 700f,
        freezingPoint: -60f,
        vaporisationPoint: 200f);
    
    public static Material GasOil = new FluidMaterial(
        "Gas Oil",
        0x444422,
        VisualSet.Rough,
        new Compound(Element.C * 16, Element.H * 34),
        MaterialFlags.Flammable | MaterialFlags.Toxic,
        density: 830f,
        freezingPoint: -20f,
        vaporisationPoint: 350f);
    
    public static Material Kerosene = new FluidMaterial(
        "Kerosene",
        0x555522,
        VisualSet.Rough,
        new Compound(Element.C * 12, Element.H * 26),
        MaterialFlags.Flammable | MaterialFlags.Toxic,
        density: 820f,
        freezingPoint: -47f,
        vaporisationPoint: 300f);
    
    public static Material HeavyOil = new FluidMaterial(
        "Heavy Oil",
        0x222211,
        VisualSet.Rough,
        new Compound(Element.C * 20, Element.H * 42),
        MaterialFlags.Flammable | MaterialFlags.Toxic,
        density: 900f,
        freezingPoint: -20f,
        vaporisationPoint: 500f);
    
    public static Material Benzene = new FluidMaterial(
        "Benzene",
        0x993333,
        VisualSet.Shiny,
        new Compound(Element.C * 6, Element.H * 6),
        MaterialFlags.Flammable | MaterialFlags.Toxic,
        density: 876f,
        freezingPoint: 5.5f,
        vaporisationPoint: 80.1f);
    
    public static Material Methane = new FluidMaterial(
        "Methane",
        0xBBDDEE,
        VisualSet.Default,
        new Compound(Element.C * 1, Element.H * 4),
        MaterialFlags.Flammable | MaterialFlags.Toxic,
        density: 422f,
        freezingPoint: -182.5f,
        vaporisationPoint: -161.5f);
    
    public static Material Ethylene = new FluidMaterial(
        "Ethylene",
        0x88CCFF,
        VisualSet.Default,
        new Compound(Element.C * 2, Element.H * 4),
        MaterialFlags.Flammable | MaterialFlags.Toxic,
        density: 570f,
        freezingPoint: -169.2f,
        vaporisationPoint: -103.7f);
    
    public static Material Propylene = new FluidMaterial(
        "Propylene",
        0x88AAFF,
        VisualSet.Default,
        new Compound(Element.C * 3, Element.H * 6),
        MaterialFlags.Flammable | MaterialFlags.Toxic,
        density: 610f,
        freezingPoint: -185.2f,
        vaporisationPoint: -47.6f);
    
    public static Material Butadiene = new FluidMaterial(
        "Butadiene",
        0xCC8844,
        VisualSet.Default,
        new Compound(Element.C * 4, Element.H * 6),
        MaterialFlags.Flammable | MaterialFlags.Toxic,
        density: 621f,
        freezingPoint: -108.9f,
        vaporisationPoint: -4.4f);
    
    public static Material Lubricant = new FluidMaterial(
        "Lubricant",
        0x888866,
        VisualSet.Shiny,
        new Compound(Element.C * 25, Element.H * 52),
        MaterialFlags.Flammable | MaterialFlags.Viscous,
        density: 860f,
        freezingPoint: -20f,
        vaporisationPoint: 550f);
    
    public static Material Cumene = new FluidMaterial(
        "Cumene",
        0x996644,
        VisualSet.Shiny,
        new Compound(Element.C * 9, Element.H * 12),
        MaterialFlags.Flammable | MaterialFlags.Toxic,
        density: 862f,
        freezingPoint: -96f,
        vaporisationPoint: 152f);
    
    public static Material CumeneHydroperoxide = new FluidMaterial(
        "Cumene Hydroperoxide",
        0x669966,
        VisualSet.Shiny,
        new Compound(Element.C * 9, Element.H * 12, Element.O * 2),
        MaterialFlags.Flammable | MaterialFlags.Explosive | MaterialFlags.Toxic,
        density: 930f,
        freezingPoint: -10f,
        vaporisationPoint: 153f);
    
    public static Material Ethylbenzene = new FluidMaterial(
        "Ethylbenzene",
        0xCC9966,
        VisualSet.Shiny,
        new Compound(Element.C * 8, Element.H * 10),
        MaterialFlags.Flammable | MaterialFlags.Toxic,
        density: 867f,
        freezingPoint: -95f,
        vaporisationPoint: 136f);
    
    public static Material Styrene = new FluidMaterial(
        "Styrene",
        0xCC6666,
        VisualSet.Shiny,
        new Compound(Element.C * 8, Element.H * 8),
        MaterialFlags.Flammable | MaterialFlags.Toxic,
        density: 909f,
        freezingPoint: -30.6f,
        vaporisationPoint: 145f);
    
    public static Material SulphurDioxide = new FluidMaterial(
        "Sulphur Dioxide",
        0xF1EC4D,
        VisualSet.Default,
        new Compound(Element.S * 1, Element.O * 2),
        MaterialFlags.Toxic | MaterialFlags.Corrosive,
        density: 2640f,
        freezingPoint: -72.7f,
        vaporisationPoint: -10f);
    
    public static Material SulphurTrioxide = new FluidMaterial(
        "Sulphur Trioxide",
        0xC3BF29,
        VisualSet.Default,
        new Compound(Element.S * 1, Element.O * 3),
        MaterialFlags.Toxic | MaterialFlags.Corrosive,
        density: 1960f,
        freezingPoint: 16.9f,
        vaporisationPoint: 45f);
    
    public static Material Oleum = new FluidMaterial(
        "Oleum",
        0xC3BF29,
        VisualSet.Shiny,
        new Compound(Element.S * 1, Element.O * 3, Element.H * 1),
        MaterialFlags.Corrosive | MaterialFlags.Acidic | MaterialFlags.Toxic,
        density: 1900f,
        freezingPoint: 0f,
        vaporisationPoint: 300f);
    
    public static Material SulfuricAcid = new FluidMaterial(
        "Sulfuric Acid",
        0x888637,
        VisualSet.Shiny,
        new Compound(Element.H * 2, Element.S * 1, Element.O * 4),
        MaterialFlags.Corrosive | MaterialFlags.Toxic | MaterialFlags.ThermallyConductive,
        density: 1840f,
        freezingPoint: 10f,
        vaporisationPoint: 337f);
    
    public static Material Ammonia = new FluidMaterial(
        "Ammonia",
        0x99CCFF,
        VisualSet.Default,
        new Compound(Element.N * 1, Element.H * 3),
        MaterialFlags.Toxic | MaterialFlags.Corrosive | MaterialFlags.Flammable,
        density: 681f,
        freezingPoint: -77.7f,
        vaporisationPoint: -33.3f);
    
    public static Material Methanol = new FluidMaterial(
        "Methanol",
        0x66CC99,
        VisualSet.Shiny,
        new Compound(Element.C * 1, Element.H * 3, Element.O * 1, Element.H * 1),
        MaterialFlags.Flammable | MaterialFlags.Toxic,
        density: 792f,
        freezingPoint: -97.6f,
        vaporisationPoint: 64.7f);
    
    public static Material Syngas = new FluidMaterial(
        "Syngas",
        0xAAAAAA,
        VisualSet.Default,
        new Compound(Element.C * 1, Element.O * 1, Element.H * 2),
        MaterialFlags.Flammable | MaterialFlags.Toxic,
        density: 900f,
        freezingPoint: -200f,
        vaporisationPoint: -100f);
    
    public static Material Lactobacillus = new BioMaterial(
         "Lactobacillus",
         1,
         MaterialFlags.Toxic,
         minTemp: 10f,
         maxTemp: 70f,
         feedsOn: Materials.Sulphur);
    
    public static Alloy Bronze = new Alloy(
        "Bronze",
        Materials.Tin * 1, Materials.Copper * 3);
        
    public static Alloy Brass = new Alloy(
        "Brass",
        Materials.Zinc * 2, Materials.Copper * 3);
        
    public static Alloy HighCarbonSteel = new Alloy(
        "High-Carbon Steel",
        Materials.Iron * 3, Materials.Coal * 1);
        
    public static Alloy ToolSteel = new Alloy(
        "Tool Steel",
        Materials.HighCarbonSteel * 7, Materials.Tungsten * 2, Materials.Cobalt * 1);
        
    public static Alloy StainlessSteel = new Alloy(
        "Stainless Steel",
        Materials.HighCarbonSteel * 7, Materials.Chromium * 2, Materials.Manganese * 1);
        
    public static Alloy Electrum = new Alloy(
        "Electrum",
        Materials.Gold * 3, Materials.Silver * 2, Materials.Copper * 1);
        
    
}
