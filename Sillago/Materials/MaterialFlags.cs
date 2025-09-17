namespace Sillago
{
    using System;

    [Flags]
    public enum MaterialFlags : ulong
    {
        None = 0,
        Radioactive = 1            << 5,
        Toxic = 1                  << 6,
        Flammable = 1              << 7,
        ElectricallyConductive = 1 << 8,  // Conducts electricity, e.g. metals
        ThermallyConductive = 1    << 9,  // Conducts heat, e.g. metals, water
        ElectricallyInsulating = 1 << 10, // Insulates against electricity, e.g. plastics, rubber
        ThermallyInsulating = 1    << 11, // Insulates against heat, e.g. plastics, rubber
        Ferromagnetic = 1          << 12, // Exhibits ferromagnetism, e.g. can be magnetized
        CorrosionResistant = 1     << 13, // Resistant to chemical reactions, e.g. rusting or corrosion
        Ore = 1                    << 14,
        Brittle = 1                << 15, // No yield from hammering, can't be used in lathe
        Ductile = 1                << 16, // Very malleable and forges easily
        Flexible = 1               << 17, // Can be bent and shaped without breaking
        Viscous = 1                << 18, // Flows slowly, e.g. honey, tar
        Explosive = 1              << 19, // Can detonate under certain conditions
        Corrosive = 1              << 20, // Can corrode other materials on contact
        Acidic = 1                 << 21, // Has a low pH and can cause burns

        Recessive = MaterialFlags.Brittle
                    | MaterialFlags.Ductile
                    | MaterialFlags.Flexible
                    | MaterialFlags
                        .ElectricallyConductive, // Only alloys of more than 50% makeup may inherit this flag
        Ephemeral = MaterialFlags.Ore            // Can not be inherited
    }
}