namespace SillagoGenerator;

using System.Text;
using Sillago.Symbols;
using Sillago.Utils;

class Program
{
    private static SymbolFormatter _symbolFormatter = new CodeSymbolFormatter();
    static void Main(string[] args)
    {
        StringBuilder sb = new();
        void ForeachRow(Page page, Action<Row, StringBuilder> action)
        {
            foreach (Row row in page)
            {
                try
                {
                    action(row, sb);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing row: {ex.Message}");
                }
            }
        }

        string sheetUrl =
            "https://docs.google.com/spreadsheets/d/e/2PACX-1vRwlJnemAVQvnasfytRMozXREXuvsC5cKhbudh4I7uMKbRSjubAqGnDXfMGrToP8hg7NLmK8MOZYlWr/pubhtml";

        Sheet sheet = new(sheetUrl);
        Page substancesPage = sheet["Substances and Metals"];
        Page powdersPage = sheet["Powders and Crystals"];
        Page fluidsPage = sheet["Fluids and Gases"];
        Page bioMaterialsPage = sheet["Bio Materials"];
        Page alloysPage = sheet["Alloys"];

        ForeachRow(substancesPage, GenerateSubstance);
        ForeachRow(powdersPage, GeneratePowder);
        ForeachRow(fluidsPage, GenerateFluid);
        ForeachRow(bioMaterialsPage, GenerateBioMaterial);
        ForeachRow(alloysPage, GenerateAlloy);

        File.WriteAllText("Sillago/Materials/Materials.Generated.cs", GenerateClass("Materials", sb.ToString()));
    }
    
    private static string GenerateClass(string className, string body)
    {
        string indentBody = string.Join(Environment.NewLine, body.Split(Environment.NewLine).Select(line => "    " + line));
        return $$"""
               // This file is auto-generated. Do not edit manually.
               namespace Sillago.Materials;
               
               using Types;

               [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
               public partial class {{className}}
               {
               {{indentBody}}
               }
               
               """;
    }

    private static void GenerateSubstance(Row row, StringBuilder sb)
    {
        string name = row["Name"];

        if (string.IsNullOrWhiteSpace(name) || name.StartsWith("#"))
            return;

        string safeName = Program.GenerateSafeName(name);
        string color = row["Color"];
        string visualSet = Program.ParseVisualSet(row["Visual Set"]);
        string symbol = Program.ParseSymbol(row["Symbol"]);
        bool isMetal = Program.ParseBoolean(row["Metal"]);
        string flags = Program.ParseFlags(row["Flags"]);
        string density = row["Density"];
        string meltingPoint = Program.ParseTemperature(row["Melting Point"]);
        var (forms, extra) = Program.ParseNotes(row["Notes"]);
  
        string line = $"""
                       public static Material {safeName} = new {(isMetal ? "MetalMaterial" : "IngotMaterial")}(
                           "{name}",
                           0x{color},
                           {visualSet},
                           {symbol},
                           {flags},
                           density: {density}f,
                           meltingPoint: {meltingPoint}){GenerateNameOverrides(forms)};
                           
                       """;

        sb.AppendLine(line);
        Console.WriteLine($"Generated {name}");
    }

    private static void GeneratePowder(Row row, StringBuilder sb)
    {
        string name = row["Name"];

        if (string.IsNullOrWhiteSpace(name) || name.StartsWith("#"))
            return;

        string safeName = Program.GenerateSafeName(name);
        string color = row["Color"];
        string visualSet = Program.ParseVisualSet(row["Visual Set"]);
        string symbol = Program.ParseSymbol(row["Symbol"]);
        bool isCrystalline = Program.ParseBoolean(row["Crystalline"]);
        string flags = Program.ParseFlags(row["Flags"]);
        string density = row["Density"];
        string liqueficationPoint = Program.ParseTemperature(row["Liquefication Point"]);
        var (forms, extra) = Program.ParseNotes(row["Notes"]);

        string line = $"""
                       public static Material {safeName} = new {(isCrystalline ? "CrystallineMaterial" : "PowderMaterial")}(
                           "{name}",
                           0x{color},
                           {visualSet},
                           {symbol},
                           {flags},
                           density: {density}f,
                           liquificationPoint: {liqueficationPoint}){GenerateNameOverrides(forms)};

                       """;

        sb.AppendLine(line);
        Console.WriteLine($"Generated {name}");
    }

    private static void GenerateFluid(Row row, StringBuilder sb)
    {
        string name = row["Name"];

        if (string.IsNullOrWhiteSpace(name) || name.StartsWith("#"))
            return;

        string safeName = Program.GenerateSafeName(name);
        string color = row["Color"];
        string visualSet = Program.ParseVisualSet(row["Visual Set"]);
        string symbol = Program.ParseSymbol(row["Symbol"]);
        string flags = Program.ParseFlags(row["Flags"]);
        string density = row["Density"];
        string freezingPoint = Program.ParseTemperature(row["Freezing Point"]);
        string vaporisationPoint = Program.ParseTemperature(row["Vaporisation Point"]);
        var (forms, extra) = Program.ParseNotes(row["Notes"]);

        string line = $"""
                       public static Material {safeName} = new FluidMaterial(
                           "{name}",
                           0x{color},
                           {visualSet},
                           {symbol},
                           {flags},
                           density: {density}f,
                           freezingPoint: {freezingPoint},
                           vaporisationPoint: {vaporisationPoint}){GenerateNameOverrides(forms)};

                       """;

        sb.AppendLine(line);
        Console.WriteLine($"Generated {name}");
    }
    
    private static void GenerateBioMaterial(Row row, StringBuilder sb)
    {
        string name = row["Name"];

        if (string.IsNullOrWhiteSpace(name) || name.StartsWith("#"))
            return;

        string safeName = Program.GenerateSafeName(name);
        string variation = row["Variation"];
        string flags = Program.ParseFlags(row["Flags"]);
        string minTemp = Program.ParseTemperature(row["Min Temp"]);
        string maxTemp = Program.ParseTemperature(row["Max Temp"]);
        string feedsOn = Program.GenerateSafeName(row["Feeds On"]);
        var (forms, extra) = Program.ParseNotes(row["Notes"]);

        string line = $"""
                       public static Material {safeName} = new BioMaterial(
                            "{name}",
                            {variation},
                            {flags},
                            minTemp: {minTemp},
                            maxTemp: {maxTemp},
                            feedsOn: Materials.{feedsOn}){GenerateNameOverrides(forms)};

                       """;

        sb.AppendLine(line);
        Console.WriteLine($"Generated {name}");
    }
    
    private static void GenerateAlloy(Row row, StringBuilder sb)
    {
        string name = row["Name"];

        if (string.IsNullOrWhiteSpace(name) || name.StartsWith("#"))
            return;

        string safeName = Program.GenerateSafeName(name);
        List<string> components = new();
        for (int i = 1; i <= 5; i++)
        {
            string component = row[$"Component {i}"];
            if (!Program.IsBlank(component))
            {
                // Copper (2)
                int openParen = component.LastIndexOf('(');
                int closeParen = component.LastIndexOf(')');
                if (openParen > 0 && closeParen > openParen)
                {
                    string symbol = component[..openParen].Trim();
                    string amountStr = component[(openParen + 1)..closeParen].Trim();
                    if (int.TryParse(amountStr, out int amount) && amount > 0)
                    {
                        string parsedSymbol = Program.GenerateSafeName(symbol);
                        components.Add($"Materials.{parsedSymbol} * {amount}");
                    }
                }
                else
                {
                    string parsedSymbol = Program.GenerateSafeName(component.Trim());
                    components.Add($"Materials.{parsedSymbol} * 1");
                }
            }
        }
        
        string line = $"""
                       public static Alloy {safeName} = new Alloy(
                           "{name}",
                           {string.Join(", ", components)});
                           
                       """;
        
        sb.AppendLine(line);
        Console.WriteLine($"Generated {name}");
    }

    private static string GenerateSafeName(string name)
    {
        // remove dashes, uppercamelcase
        string[] parts = name.Split(new char[] { ' ', '-' }, StringSplitOptions.RemoveEmptyEntries);
        return string.Concat(parts.Select(part => char.ToUpper(part[0]) + part[1..].ToLower()));
    }

    private static string ParseVisualSet(string visualSet)
    {
        if (Program.IsBlank(visualSet))
            return "VisualSet.Default";

        return "VisualSet." + visualSet;
    }
    private static bool IsBlank(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return true;

        if (text.Equals("n/a", StringComparison.InvariantCultureIgnoreCase))
            return true;

        return false;
    }

    private static bool ParseBoolean(string boolean)
    {
        return boolean.ToLower() == "true";
    }

    private static string ParseTemperature(string temperature)
    {
        temperature = temperature.Replace("°C", "", StringComparison.InvariantCultureIgnoreCase);
        temperature = temperature.TrimEnd('.');
        if (Program.IsBlank(temperature))
            return "null";

        return temperature + "f";
    }

    private static string ParseFlags(string flags)
    {
        if (Program.IsBlank(flags))
            return "MaterialFlags.None";

        return string.Join(" | ", flags.Split(", ").Select(f => $"MaterialFlags.{f}"));
    }

    private static (Dictionary<string, string> forms, string extra) ParseNotes(string notes)
    {
        //Forms [Distilled Water (Liquid), Distilled Ice (Ice), Distilled Steam (Gas)]
        if (Program.IsBlank(notes))
            return (new Dictionary<string, string>(), string.Empty);
        
        // find Forms [...]
        Dictionary<string, string> forms = new();
        string extra = string.Empty;
        int formsStart = notes.IndexOf("Forms [", StringComparison.InvariantCultureIgnoreCase);
        int formsEnd = notes.IndexOf(']', formsStart);
        
        if (formsStart >= 0 && formsEnd > formsStart)
        {
            string formsContent = notes[(formsStart + 7)..formsEnd].Trim();
            string[] formParts = formsContent.Split(',', StringSplitOptions.RemoveEmptyEntries);
            foreach (string formPart in formParts)
            {
                string[] keyValue = formPart.Split('(', StringSplitOptions.RemoveEmptyEntries);
                if (keyValue.Length == 2)
                {
                    string formName = keyValue[0].Trim();
                    string formType = keyValue[1].TrimEnd(')').Trim();
                    forms[formType] = formName;
                }
            }

            extra = notes[(formsEnd + 1)..].Trim();
        }
        else
            extra = notes.Trim();
        
        return (forms, extra);
    }

    private static string GenerateNameOverrides(Dictionary<string, string> forms)
    {
        if (forms.Count == 0)
            return string.Empty;

        StringBuilder sb = new();
        foreach (var kvp in forms)
        {
            sb.AppendLine();
            sb.Append($"        .OverrideFormName(MaterialType.{kvp.Key}, \"{kvp.Value}\")");
        }

        return sb.ToString();
    }
    
    private static string ParseSymbol(string symbol)
    {
        Symbol parsedSymbol = SymbolParser.Parse(symbol);
        return Program._symbolFormatter.Format(parsedSymbol);
    }
}