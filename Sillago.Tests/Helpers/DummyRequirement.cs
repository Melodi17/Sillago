namespace Sillago.Tests.Helpers;

using System.Diagnostics.CodeAnalysis;
using Requirements;

[ExcludeFromCodeCoverage]
public class DummyRequirement(bool result) : IRecipeRequirement
{
    public string GetInfo() => "Dummy Requirement";
    public bool IsMet(IMachine machine) => result;
}