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
        Console.WriteLine("Repl mode.");
        while (true)
        {
            Console.Write("> ");
            string? input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
                continue;

            else if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                break;

            else if (input.Equals("help", StringComparison.OrdinalIgnoreCase))
                AnsiConsole.Write(DisplayHelp());

            else if (input.StartsWith("items"))
            {
                string searchTerm = input[5..].Trim();
                List<Item> results = SearchItems(searchTerm);
                if (results.Count == 0)
                {
                    NoResultsFound();
                    continue;
                }

                DisplayItemResults(searchTerm, results);
            }

            else if (input.StartsWith("recipes"))
            {
                string searchTerm = input[7..].Trim();
                List<Recipe> results = SearchRecipes(searchTerm);
                if (results.Count == 0)
                {
                    NoResultsFound();
                    continue;
                }

                DisplayRecipeResults(searchTerm, results);
            }

            else if (Items.Entries.Any(i => i.Id.Equals(input, StringComparison.OrdinalIgnoreCase)))
            {
                Item item = Items.Get(input);
                DisplayItem(item);
            }
            else if (Recipes.Entries.Any(r
                         => r.Id.Equals(input, StringComparison.OrdinalIgnoreCase)))
            {
                Recipe recipe = Recipes.Get(input);
                DisplayRecipe(recipe);
            }
            else
            {
                AnsiConsole.MarkupLine(
                    "[red]Unknown command or ID. Type 'help' for a list of commands.[/]");
            }
        }
    }
    private static void DisplayItemResults(string searchTerm, List<Item> results)
    {
        AnsiConsole.MarkupLine($"[green]{results.Count} result(s) found for '{searchTerm}':[/]");
        Table table = new();
        table.AddColumn("Color");
        table.AddColumn("ID");
        table.AddColumn("Name");
        table.AddColumn("Type");
        table.AddColumn("Material");
        foreach (Item item in results)
        {
            if (item is ItemMaterial im)
            {
                string colorBox = $"[#{im.Material.Color:X6}]████[/]";
                table.AddRow(
                    colorBox, item.Id, item.Name, im.Type.ToString(),
                    im.Material.Name);
            }
            else
                table.AddRow(
                    "", item.Id, item.Name, "-",
                    "-");
        }

        AnsiConsole.Write(table);
    }

    private static void DisplayRecipeResults(string searchTerm, List<Recipe> results)
    {
        AnsiConsole.MarkupLine($"[green]{results.Count} result(s) found for '{searchTerm}':[/]");
        Table table = new();
        table.AddColumn("ID");
        table.AddColumn("Name");
        table.AddColumn("Type");
        table.AddColumn("Duration");
        foreach (Recipe recipe in results)
            table.AddRow(
                recipe.Id, recipe.Name, recipe.Type.ToString(),
                $"{recipe.Duration.TotalSeconds:0.##}s");

        AnsiConsole.Write(table);
    }

    private static void NoResultsFound()
    {
        AnsiConsole.MarkupLine("[red]No results found.[/]");
    }

    private static List<Item> SearchItems(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return Items.Entries.ToList();

        return Items
            .Entries
            .Where(i => i.Id.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                        || i.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                        || (i is ItemMaterial im
                            && im.Material.Name.Contains(
                                searchTerm, StringComparison.OrdinalIgnoreCase)))
            .ToList();
    }

    private static List<Recipe> SearchRecipes(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return Recipes.Entries.ToList();

        return Recipes
            .Entries
            .Where(r => r.Id.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                        || r.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                        || r.Inputs.Any(i => i.Item.Name.Contains(
                            searchTerm, StringComparison.OrdinalIgnoreCase))
                        || r.Outputs.Any(o => o.Item.Name.Contains(
                            searchTerm, StringComparison.OrdinalIgnoreCase)))
            .ToList();
    }

    private static void DisplayItem(Item item)
    {
        Table usageTable = new();
        usageTable.AddColumn("Recipe ID");
        usageTable.AddColumn("Recipe Name");
        usageTable.AddColumn("Type");
        usageTable.AddColumn("Role");

        IEnumerable<Recipe> producing = Recipes.Producing(item);
        IEnumerable<Recipe> consuming = Recipes.Consuming(item);

        foreach (Recipe recipe in producing)
            usageTable.AddRow(recipe.Id, recipe.Name, recipe.Type.ToString(), "Result");

        foreach (Recipe recipe in consuming)
            usageTable.AddRow(recipe.Id, recipe.Name, recipe.Type.ToString(), "Ingredient");

        Renderable usageWidget = usageTable.Rows.Count > 0
            ? usageTable
            : new Markup("[grey]No recipes found.[/]");

        string descriptionText = string.IsNullOrWhiteSpace(item.Description)
            ? "[grey]No description available.[/]"
            : item.Description;

        if (item is ItemMaterial im)
        {
            SymbolFormatter formatter = new AsciiSymbolFormatter();
            string symbol = formatter.Format(im.Material.Symbol);
            descriptionText = $"[bold]{symbol}[/]\n" + descriptionText;
        }

        Panel panel = new(
            new Rows(
                new Markup(descriptionText),
                usageWidget
            ));
        panel.Header = new PanelHeader(item.Name);
        AnsiConsole.Write(panel);
    }

    private static void DisplayRecipe(Recipe recipe)
    {
        Table inputsTable = new();
        inputsTable.AddColumn("Item ID");
        inputsTable.AddColumn("Item Name");
        inputsTable.AddColumn("Quantity");

        foreach (ItemStack input in recipe.Inputs)
            inputsTable.AddRow(input.Item.Id, input.Item.Name, input.Amount.ToString());

        Table outputsTable = new();
        outputsTable.AddColumn("Item ID");
        outputsTable.AddColumn("Item Name");
        outputsTable.AddColumn("Quantity");

        foreach (ItemStack output in recipe.Outputs)
            outputsTable.AddRow(output.Item.Id, output.Item.Name, output.Amount.ToString());

        string requirementsText = recipe.Requirements.Count > 0
            ? string.Join("\n", recipe.Requirements.Select(x => x.GetInfo()))
            : "[grey]None[/]";

        Panel panel = new(
            new Rows(
                new Markup($"[bold]Type:[/] {recipe.Type}"),
                new Markup($"[bold]Duration:[/] {recipe.Duration.TotalSeconds:0.##} seconds"),
                new Markup("[bold]Inputs:[/]"),
                inputsTable,
                new Markup("[bold]Outputs:[/]"),
                outputsTable,
                new Markup("[bold]Requirements:[/]"),
                new Markup(requirementsText)
            ));
        panel.Header = new PanelHeader(recipe.Name);
        AnsiConsole.Write(panel);
    }

    private static IRenderable DisplayHelp()
    {
        Table table = new();
        table.AddColumn("Command");
        table.AddColumn("Description");
        table.AddRow("[bold]help[/]", "Display this help message.");
        table.AddRow("[bold]exit[/]", "Exit the REPL.");
        table.AddRow(
            "[bold]items <search term>[/]",
            "Search for items. If no search term is provided, all items are listed.");
        table.AddRow(
            "[bold]recipes <search term>[/]",
            "Search for recipes. If no search term is provided, all recipes are listed.");
        table.AddRow(
            "[bold]<item_id>[/]",
            "Display detailed information about the item with the given ID.");
        table.AddRow(
            "[bold]<recipe_id>[/]",
            "Display detailed information about the recipe with the given ID.");
        return table;
    }
}