namespace SillagoGenerator;

using System.Text;

class Program
{
    static void Main(string[] args)
    {
        StringBuilder sb = new();
        
        string sheetUrl =
            "https://docs.google.com/spreadsheets/d/e/2PACX-1vRwlJnemAVQvnasfytRMozXREXuvsC5cKhbudh4I7uMKbRSjubAqGnDXfMGrToP8hg7NLmK8MOZYlWr/pubhtml";
        
        Sheet sheet = new(sheetUrl);
        Page substancesPage = sheet["Substances and Metals"];
        Page powdersPage = sheet["Powders and Crystals"];
        Page fluidsPage = sheet["Fluids and Gases"];
        Page bioMaterialsPage = sheet["Bio Materials"];
        Page alloysPage = sheet["Alloys"];
        
        Program.GenerateSubstances(substancesPage, sb);
        Program.GeneratePowders(powdersPage, sb);
        Program.GenerateFluids(fluidsPage, sb);
        
        File.WriteAllText("output.txt", sb.ToString());
    }
    
    private static void GenerateSubstances(Page substancesPage, StringBuilder sb)
    {
        foreach (var row in substancesPage)
        {
            string name = row["Name"];
            
            if (string.IsNullOrWhiteSpace(name) || name.StartsWith("#"))
                continue;
            
            string safeName = Program.GenerateSafeName(name);
            string color = row["Color"];
            string visualSet = row["Visual Set"];
            string symbol = row["Symbol"];
            bool isMetal = row["Metal"].ToLower() == "true";
            string flags = row["Flags"];
            string density = row["Density"];
            string meltingPoint = row["Melting Point"].Replace("°C", "", StringComparison.InvariantCultureIgnoreCase);
            /*
              public static MetalMaterial Aluminium = new MetalMaterial(
                      "Aluminium",
                      0x80C8F0,
                      VisualSet.Dull,
                      Element.Al,
                      MaterialFlags.Ore | MaterialFlags.ElectricallyConductive | MaterialFlags.Ductile,
                      2700,
                      meltingPoint: 660.32f);
             */
            string line = $"""
                           public static Material {safeName} = new {(isMetal ? "MetalMaterial" : "IngotMaterial")}(
                               "{name}",
                               0x{color},
                               VisualSet.{visualSet},
                               Element.{symbol},
                               {string.Join(" | ", flags.Split(", ").Select(f => $"MaterialFlags.{f}"))},
                               density: {density}f,
                               meltingPoint: {meltingPoint}f);
                               
                           """;
            
            sb.AppendLine(line);
            Console.WriteLine($"Generated {name}");
        }
    }
    
    private static void GeneratePowders(Page powdersPage, StringBuilder sb)
    {
        foreach (var row in powdersPage)
        {
            string name = row["Name"];
            
            if (string.IsNullOrWhiteSpace(name) || name.StartsWith("#"))
                continue;
            
            string safeName = Program.GenerateSafeName(name);
            string color = row["Color"];
            string visualSet = row["Visual Set"];
            string symbol = row["Symbol"];
            bool isCrystalline = row["Crystalline"].ToLower() == "true";
            string flags = row["Flags"];
            string density = row["Density"];
            string liqueficationPoint = row["Liquefication Point"].Replace("°C", "", StringComparison.InvariantCultureIgnoreCase);
            
            string line = $"""
                           public static Material {safeName} = new {(isCrystalline ? "CrystallineMaterial" : "PowderMaterial")}(
                               "{name}",
                               0x{color},
                               VisualSet.{visualSet},
                               Element.{symbol},
                               {string.Join(" | ", flags.Split(", ").Select(f => $"MaterialFlags.{f}"))},
                               density: {density}f,
                               liquificationPoint: {(IsBlank(liqueficationPoint) ? "null" : liqueficationPoint)}f);
                           
                           """;
            
            sb.AppendLine(line);
            Console.WriteLine($"Generated {name}");
        }
    }
    private static bool IsBlank(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return true;
        
        if (text.Equals("N/A", StringComparison.InvariantCultureIgnoreCase))
            return true;
        
        return false;
    }

    private static void GenerateFluids(Page fluidsPage, StringBuilder sb)
    {
        foreach (var row in fluidsPage)
        {
            string name = row["Name"];
            
            if (string.IsNullOrWhiteSpace(name) || name.StartsWith("#"))
                continue;
            
            string safeName = Program.GenerateSafeName(name);
            string color = row["Color"];
            string visualSet = row["Visual Set"];
            string symbol = row["Symbol"];
            string flags = row["Flags"];
            string density = row["Density"];
            string freezingPoint = row["Freezing Point"].Replace("°C", "", StringComparison.InvariantCultureIgnoreCase);
            string vaporisationPoint = row["Vaporisation Point"].Replace("°C", "", StringComparison.InvariantCultureIgnoreCase);
            
            string line = $"""
                           public static Material {safeName} = new FluidMaterial(
                               "{name}",
                               0x{color},
                               VisualSet.{visualSet},
                               Element.{symbol},
                               {string.Join(" | ", flags.Split(", ").Select(f => $"MaterialFlags.{f}"))},
                               density: {density}f,
                               freezingPoint: {freezingPoint}f,
                               vaporisationPoint: {vaporisationPoint}f);

                           """;
            
            sb.AppendLine(line);
            Console.WriteLine($"Generated {name}");
        }
    }
    
    private static string GenerateSafeName(string name)
    {
        // remove dashes, uppercamelcase
        string[] parts = name.Split([' ', '-'], StringSplitOptions.RemoveEmptyEntries);
        return string.Concat(parts.Select(part => char.ToUpper(part[0]) + part[1..].ToLower()));
    }
}