namespace Sillago.Tests.Helpers;

using Sillago.Utils;

public class DummySymbolizable : ISymbolizable
{
    public string Symbol { get; set; }

    public DummySymbolizable(string symbol)
    {
        this.Symbol = symbol;
    }
}