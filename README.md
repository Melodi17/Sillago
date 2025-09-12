# Sillago
A .NET "science engine" for games and simulations.

- Based on real-world interactions between different materials.
- Predictive alloy state determination.
- Extensive testing and validation.
- Designed for ease of integration into existing projects.

## Getting Started
To get started with Sillago, follow these steps:
1. Clone the repository:
   ```bash
   git clone https://github.com/Melodi17/Sillago.git
   ```
2. Compile the project:
    ```bash
    cd Sillago
    dotnet build --configuration Release
    ```
3. Copy the compiled DLLs into your project and add references to them.
4. Start using Sillago!

## Usage
```csharp
using Sillago;

// This loads all the recipes and materials into the registry.
SillagoEngine.Initialize();


// This retrieves a item by its name.
Item aluminium = Items.Get("aluminium_ingot");

// Retrieve a material by its name.
ItemMaterial aluminiumOre = Items.GetMaterial(Materials.Aluminium, MaterialType.Ore);

// This retrieves a recipe by its name.
Recipe aluminiumRecipe = Recipes.Get("aluminium_powder_smelting_into_aluminium_ingot");


// Retrieves all recipes that use aluminium ore as an input.
IEnumerable<Recipe> recipesUsingAluminiumOre = Recipes.Consuming(aluminiumOre);

// Retrieves all recipes that produce aluminium ingots as an output.
IEnumerable<Recipe> recipesProducingAluminiumIngot = Recipes.Producing(aluminium);


// Item.Entries and Recipe.Entries provide access to all registered items and recipes.
foreach (var item in Items.Entries)
{
    Console.WriteLine($"Item: {item.Name}");
}
```

## Documentation
See the full documentation [here](https://github.com/Melodi17/Sillago/wiki).