namespace ConsoleInterface;

using Sillago;
using Sillago.Items;
using Sillago.Materials;
using Sillago.Recipes;
using Sillago.Utils;
using Spectre.Console;
using Spectre.Console.Rendering;

class Program
{
    static void Main(string[] args)
    {
        DataLoader.Initialize();
        Repl();
    }

    static void Repl()
    {
        Console.WriteLine("Search for item!");
        while (true)
        {
            Console.Write("> ");
            string? input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
                continue;
            
            if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                break;

            var results = Items.Entries
                .Where(item
                    => item.Name.Contains(input, StringComparison.OrdinalIgnoreCase)
                       || item.Id.Contains(input, StringComparison.OrdinalIgnoreCase));

            if (!results.Any())
            {
                AnsiConsole.MarkupLine("[red]No items found.[/]");
                continue;
            }

            if (results.Count() == 1 || input.Equals(results.First().Id, StringComparison.OrdinalIgnoreCase))
            {
                DisplayItem(results.First());
                continue;
            }
            
            var table = new Table();
            table.AddColumn("Color");
            table.AddColumn("ID");
            table.AddColumn("Name");
            table.AddColumn("Type");
            table.AddColumn("Material");
            foreach (var item in results)
            {
                if (item is ItemMaterial im)
                {
                    string colorBox = $"[#{im.Material.Color:X6}]████[/]";
                    table.AddRow(colorBox, item.Id, item.Name, im.Type.ToString(), im.Material.Name);
                }
                else
                    table.AddRow(item.Id, item.Name, "-", "-");
            }
            
            AnsiConsole.Write(table);
        }
    }
    private static void DisplayItem(Item item)
    {
        var usageTable = new Table();
        usageTable.AddColumn("Recipe ID");
        usageTable.AddColumn("Recipe Name");
        usageTable.AddColumn("Type");
        var usages = Recipes.Entries
            .Where(r => r.Inputs.Any(i => i.Item.Id == item.Id) || r.Outputs.Any(o => o.Item.Id == item.Id))
            .ToList();
        
        foreach (var recipe in usages)
            usageTable.AddRow(recipe.Id, recipe.Name, recipe.Type.ToString());
        
        Renderable usageWidget = usageTable.Rows.Count > 0
            ? usageTable
            : new Markup("[grey]No recipes found.[/]");
        
        string descriptionText = string.IsNullOrWhiteSpace(item.Description)
            ? "[grey]No description available.[/]"
            : item.Description;
        
        if (item is ItemMaterial im)
        {
            string symbol = SymbolHelper.FormatSymbol(im.Material.Symbol);
            descriptionText = $"[bold]{symbol}[/]\n" + descriptionText;
        }

        var panel = new Panel(
            new Rows(
                new Markup(descriptionText),
                usageWidget
            ));
        panel.Header = new PanelHeader(item.Name);
        AnsiConsole.Write(panel);
    }
}