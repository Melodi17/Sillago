namespace Sillago.Tests;

using Helpers;
using Materials;
using Utils;

[TestFixture]
public class SymbolTests
{
    private static readonly SymbolFormatter _symbolFormatter = new DefaultSymbolFormatter();
    private string Stringify(Symbol symbol) => SymbolTests._symbolFormatter.Format(symbol);

    [TestCase("Na")]
    [TestCase("NaCl")]
    [TestCase("H2O")]
    [TestCase("C6H12O6")]
    [TestCase("(C6H10O5)n")]
    [TestCase("Fe2(SO4)3")]
    [TestCase("(NH4)2(S(SO4)2)3")]
    public void Parse_And_Stringify_Are_Inverses(string input)
    {
        var symbol = SymbolParser.Parse(input);
        Assert.That(Stringify(symbol), Is.EqualTo(input));
    }
    
    [Test]
    public void Parse_Invalid_Element_Throws()
    {
        Assert.Throws<ArgumentException>(() => SymbolParser.Parse("Xx"));
        Assert.Throws<ArgumentException>(() => SymbolParser.Parse("(a3"));
        Assert.Throws<ArgumentException>(() => SymbolParser.Parse(""));
        Assert.Throws<ArgumentException>(() => SymbolParser.Parse(" "));
    }
    
    [Test]
    public void Parse_Polymer_Works()
    {
        var symbol = SymbolParser.Parse("(C6H10O5)n");
        Assert.That(symbol, Is.TypeOf<Polymer>());
    }
    
    [Test]
    public void Parse_Compound_SupportsNoDigits()
    {
        var symbol = SymbolParser.Parse("H2O");
        Assert.That(symbol, Is.TypeOf<Compound>());
        
        Compound compound = (Compound)symbol;
        Assert.That(compound.Components.Length, Is.EqualTo(2));
        Assert.That(compound.Components[0].Value, Is.TypeOf<Element>());
        Assert.That(compound.Components[0].Amount, Is.EqualTo(2));
        Assert.That(compound.Components[1].Value, Is.TypeOf<Element>());
        Assert.That(compound.Components[1].Amount, Is.EqualTo(1));
    }
    
    [Test]
    public void Parse_Compound_SupportsDoubleDigits()
    {
        var symbol = SymbolParser.Parse("C6H12O6");
        Assert.That(symbol, Is.TypeOf<Compound>());
        
        Compound compound = (Compound)symbol;
        Assert.That(compound.Components.Length, Is.EqualTo(3));
        Assert.That(compound.Components[0].Value, Is.TypeOf<Element>());
        Assert.That(compound.Components[0].Amount, Is.EqualTo(6));
        Assert.That(compound.Components[1].Value, Is.TypeOf<Element>());
        Assert.That(compound.Components[1].Amount, Is.EqualTo(12));
        Assert.That(compound.Components[2].Value, Is.TypeOf<Element>());
        Assert.That(compound.Components[2].Amount, Is.EqualTo(6));
    }
    
    [Test]
    public void Parse_Compound_Works()
    {
        var symbol = SymbolParser.Parse("Fe2(SO4)3");
        Assert.That(symbol, Is.TypeOf<Compound>());
        
        Compound compound = (Compound)symbol;
        Assert.That(compound.Components.Length, Is.EqualTo(2));
        Assert.That(compound.Components[0].Value, Is.TypeOf<Element>());
        Assert.That(compound.Components[0].Amount, Is.EqualTo(2));
     
        Element fe = (Element)compound.Components[0].Value;
        Assert.That(fe.Symbol, Is.EqualTo("Fe"));
        Assert.That(fe.Name, Is.EqualTo("Iron"));
        Assert.That(compound.Components[1].Value, Is.TypeOf<Compound>());   
        Assert.That(compound.Components[1].Amount, Is.EqualTo(3));
        
        Compound so4 = (Compound)compound.Components[1].Value;
        Assert.That(so4.Components.Length, Is.EqualTo(2));
        Assert.That(so4.Components[0].Value, Is.TypeOf<Element>());
        Assert.That(so4.Components[0].Amount, Is.EqualTo(1));
        
        Element s = (Element)so4.Components[0].Value;
        Assert.That(s.Symbol, Is.EqualTo("S"));
        Assert.That(s.Name, Is.EqualTo("Sulfur"));
        Assert.That(so4.Components[1].Value, Is.TypeOf<Element>());
        Assert.That(so4.Components[1].Amount, Is.EqualTo(4));
        
        Element o = (Element)so4.Components[1].Value;
        Assert.That(o.Symbol, Is.EqualTo("O"));
        Assert.That(o.Name, Is.EqualTo("Oxygen"));
    }
    
    [Test]
    public void Parse_EmptyCompound_Throws()
    {
        Assert.Throws<ArgumentException>(() => SymbolParser.Parse("()"));
        Assert.Throws<ArgumentException>(() => SymbolParser.Parse("()3"));
    }
    
    [Test]
    public void Parse_Element_Works()
    {
        var symbol = SymbolParser.Parse("Na");
        Assert.That(symbol, Is.TypeOf<Element>());
        
        Element element = (Element)symbol;
        Assert.That(element.Symbol, Is.EqualTo("Na"));
        Assert.That(element.Name, Is.EqualTo("Sodium"));
    }
}