namespace Sillago;

using System.Collections;
using Items;
using Materials;
using Recipes;
using Utils;

public class DataLoader
{
    public static void Initialize()
    {
        IEnumerable<Material> allMaterials = Registry.GetAllEntries<Material, Materials.Materials>().ToArray();
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
                        Items.Items.Register(item);
                    if (stack.Peek().Current is Recipe recipe)
                        Recipes.Recipes.Register(recipe);
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
     
        Materials.Materials.GenerateLinkingRecipes();
    }
}