namespace Sillago.Recipes;

public interface IRecipeRequirement
{
    string GetInfo();
    bool IsMet(IMachine machine);
}