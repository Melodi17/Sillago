namespace ConsoleInterface
{
    using System.Collections.Generic;
    using System.Linq;
    using Sillago;
    using Sillago.Types;

    public class CraftingTree
    {
        public List<(string id, string name)> Nodes = new();
        public List<(string from, string to, string? label)> Edges = new();
        
        public CraftingTree(Item result)
        {
            GetCraftingTree(result);
        }
        private void GetCraftingTree(Item result)
        {
            if (Nodes.Any(n => n.id == result.Id))
                return;
            
            Nodes.Add((result.Id, result.Name));
            if (IsBase(result))
                return;
            
            var recipes = Recipes.Producing(result);
            foreach (var recipe in recipes)
            {
                foreach (RecipeIngredient input in recipe.Inputs)
                {
                    Edges.Add((input.Options.First().Item.Id, result.Id, recipe.Type.Verb));
                    GetCraftingTree(input.Options.First().Item);
                }
            }
        }

        public string RenderMermaid()
        {
            var lines = new List<string>();
            lines.Add("---\nconfig:\n  layout: elk\n  look: neo\n  theme: redux\n---");
            lines.Add("flowchart LR");
            foreach (var node in Nodes)
            {
                lines.Add($"    {node.id}[\"{node.name}\"]");
            }
            foreach (var edge in Edges)
            {
                if (edge.label != null)
                    lines.Add($"    {edge.from} -->|{edge.label}| {edge.to}");
                else
                    lines.Add($"    {edge.from} --> {edge.to}");
            }
            return string.Join("\n", lines);
        }

        private static bool IsBase(Item item)
        {
            if (item is ItemMaterial im)
            {
                if (im.Type == MaterialType.Ingot && im.Material is not Alloy)
                    return true;
            }

            if (!Recipes.Producing(item).Any())
                return true;
            
            return false;
        }
    }
}