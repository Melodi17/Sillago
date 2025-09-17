namespace Sillago
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Utils;

    public partial class Materials
    {
        internal static void GenerateLinkingRecipes()
        {
            var waterFluid = Items.GetMaterialForm(Materials.Water, MaterialType.Liquid);
            var distilledFluid = Items.GetMaterialForm(Materials.DistilledWater, MaterialType.Liquid);
        
            // Water -> Distilled Water
            new RecipeBuilder(RecipeType.Distilling)
                .NamePatterned($"<input> <verb>")
                .AddInput(waterFluid.Stack(250))
                .AddOutput(distilledFluid.Stack(200))
                .SetDuration(TimeSpan.FromSeconds(2))
                // .AddRequirement(HasRequirement.Filter)
                .BuildAndRegister();
        }

        public static void GenerateItemsAndRecipes()
        {
            IEnumerable<Material> allMaterials = Registry.GetAllEntries<Material, Materials>().ToArray();
            List<Action> tasks = new();
            foreach (Material material in allMaterials)
            {
                Stack<IEnumerator> stack = new();
                stack.Push(material.Generate());
                // Recipes need to be registered after items, so we can use them in recipes
                while (stack.Count > 0)
                {
                    while (stack.Peek().MoveNext())
                    {
                        if (stack.Peek().Current is Item item)
                            Items.Register(item);
                        if (stack.Peek().Current is Recipe recipe)
                            Recipes.Register(recipe);
                        if (stack.Peek().Current is Action action)
                            tasks.Add(action);
                        if (stack.Peek().Current is IEnumerator subEnumerator)
                            stack.Push(subEnumerator);
                    }
                    stack.Pop();
                }
            }
        
            foreach (Action action in tasks)
                action.Invoke();
        }
    }
}