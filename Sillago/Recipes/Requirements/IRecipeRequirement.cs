namespace Sillago.Requirements
{
    public interface IRecipeRequirement
    {
        string GetInfo();
        bool IsMet(IMachine machine);
    }
}