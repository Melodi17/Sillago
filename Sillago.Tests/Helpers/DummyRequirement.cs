namespace Sillago.Tests.Helpers
{
    using System.Diagnostics.CodeAnalysis;
    using Requirements;

    [ExcludeFromCodeCoverage]
    public class DummyRequirement : IRecipeRequirement
    {
        private readonly bool _result;
        public DummyRequirement(bool result) => this._result = result;
        public string GetInfo() => "Dummy Requirement";
        public bool IsMet(IMachine machine) => this._result;
    }
}