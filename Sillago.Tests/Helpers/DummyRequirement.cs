namespace Sillago.Tests.Helpers;

using System.Diagnostics.CodeAnalysis;
using Sillago.Recipes;

[ExcludeFromCodeCoverage]
public class DummyRequirement(bool result) : IRecipeRequirement
{
    public string GetInfo() => "Dummy Requirement";
    public bool IsMet(MachineState state) => result;
}