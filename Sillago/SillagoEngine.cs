namespace Sillago;

using System.Collections;
using Utils;

public class SillagoEngine
{
    public static void Initialize()
    {
        Materials.GenerateItemsAndRecipes();
        Materials.GenerateLinkingRecipes();
    }
}