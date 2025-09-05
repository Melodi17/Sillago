namespace Sillago.Tests;

using Helpers;
using Materials;
using Utils;

[TestFixture]
public class SymbolHelperTests
{
    [TestCase("H", false)]
    [TestCase("Al", false)]
    [TestCase("H2O", true)]
    [TestCase("C6H12O6", true)]
    public void IsComplex_ShouldDetectCorrectly(string symbol, bool expected)
    {
        Assert.That(SymbolHelper.IsComplex(symbol), Is.EqualTo(expected));
    }
    
    [Test]
    public void IsComplex_ShouldThrowOnInvalidInput()
    {
        Assert.Throws<ArgumentException>(() => SymbolHelper.IsComplex(null));
        Assert.Throws<ArgumentException>(() => SymbolHelper.IsComplex(string.Empty));
    }
    
    [Test]
    public void Polymerize_ShouldThrowOnInvalidInput()
    {
        Assert.Throws<ArgumentException>(() => SymbolHelper.Polymerize(null));
        Assert.Throws<ArgumentException>(() => SymbolHelper.Polymerize(string.Empty));
    }
    
    [Test]
    public void Polymerize_ShouldReturnUnchanged_WhenAlreadyPolymer()
    {
        string symbol = "(H2O)n";
        string result = SymbolHelper.Polymerize(symbol);
        Assert.That(result, Is.EqualTo(symbol));
    }
    
    [TestCase("H", "(H)n")]
    [TestCase("H2O", "(H2O)n")]
    [TestCase("C6H12O6", "(C6H12O6)n")]
    public void Polymerize_ShouldWrapInPolymerFormat(string symbol, string expected)
    {
        string result = SymbolHelper.Polymerize(symbol);
        Assert.That(result, Is.EqualTo(expected));
    }
    
    [Test]
    public void GetSymbol_ShouldThrowOnEmptyComponents()
    {
        var emptyComponents = Array.Empty<ValueComponent<DummySymbolizable>>();
        Assert.Throws<ArgumentException>(() => SymbolHelper.GetSymbol(emptyComponents));
    }


    [Test]
    public void GetSymbol_ShouldReturn_ForBasicCompound()
    {
        ValueComponent<DummySymbolizable>[] components = [
            new(new DummySymbolizable("H"), 2),
            new(new DummySymbolizable("O"), 1)
        ];
        
        string result = SymbolHelper.GetSymbol(components);
        Assert.That(result, Is.EqualTo("H2O"));
    }
    
    [Test]
    public void GetSymbol_ShouldReturn_ForComplexCompound()
    {
        ValueComponent<DummySymbolizable>[] components = [
            new(new DummySymbolizable("C6H12O6"), 1),
            new(new DummySymbolizable("H2O"), 2)
        ];
        
        string result = SymbolHelper.GetSymbol(components);
        Assert.That(result, Is.EqualTo("(C6H12O6)(H2O)2"));
    }
}