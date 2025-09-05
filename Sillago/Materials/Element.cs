namespace Sillago.Materials;

using Utils;

public class Element : ISymbolizable
{
    public static readonly Element H = new("Hydrogen", "H", 1);
    public static readonly Element He = new("Helium", "He", 2);
    public static readonly Element Li = new("Lithium", "Li", 3);
    public static readonly Element Be = new("Beryllium", "Be", 4);
    public static readonly Element B = new("Boron", "B", 5);
    public static readonly Element C = new("Carbon", "C", 6);
    public static readonly Element N = new("Nitrogen", "N", 7);
    public static readonly Element O = new("Oxygen", "O", 8);
    public static readonly Element F = new("Fluorine", "F", 9);
    public static readonly Element Ne = new("Neon", "Ne", 10);
    public static readonly Element Na = new("Sodium", "Na", 11);
    public static readonly Element Mg = new("Magnesium", "Mg", 12);
    public static readonly Element Al = new("Aluminum", "Al", 13);
    public static readonly Element Si = new("Silicon", "Si", 14);
    public static readonly Element P = new("Phosphorus", "P", 15);
    public static readonly Element S = new("Sulfur", "S", 16);
    public static readonly Element Cl = new("Chlorine", "Cl", 17);
    public static readonly Element Ar = new("Argon", "Ar", 18);
    public static readonly Element K = new("Potassium", "K", 19);
    public static readonly Element Ca = new("Calcium", "Ca", 20);
    public static readonly Element Sc = new("Scandium", "Sc", 21);
    public static readonly Element Ti = new("Titanium", "Ti", 22);
    public static readonly Element V = new("Vanadium", "V", 23);
    public static readonly Element Cr = new("Chromium", "Cr", 24);
    public static readonly Element Mn = new("Manganese", "Mn", 25);
    public static readonly Element Fe = new("Iron", "Fe", 26);
    public static readonly Element Co = new("Cobalt", "Co", 27);
    public static readonly Element Ni = new("Nickel", "Ni", 28);
    public static readonly Element Cu = new("Copper", "Cu", 29);
    public static readonly Element Zn = new("Zinc", "Zn", 30);
    public static readonly Element Ga = new("Gallium", "Ga", 31);
    public static readonly Element Ge = new("Germanium", "Ge", 32);
    public static readonly Element As = new("Arsenic", "As", 33);
    public static readonly Element Se = new("Selenium", "Se", 34);
    public static readonly Element Br = new("Bromine", "Br", 35);
    public static readonly Element Kr = new("Krypton", "Kr", 36);
    public static readonly Element Rb = new("Rubidium", "Rb", 37);
    public static readonly Element Sr = new("Strontium", "Sr", 38);
    public static readonly Element Y = new("Yttrium", "Y", 39);
    public static readonly Element Zr = new("Zirconium", "Zr", 40);
    public static readonly Element Nb = new("Niobium", "Nb", 41);
    public static readonly Element Mo = new("Molybdenum", "Mo", 42);
    public static readonly Element Tc = new("Technetium", "Tc", 43);
    public static readonly Element Ru = new("Ruthenium", "Ru", 44);
    public static readonly Element Rh = new("Rhodium", "Rh", 45);
    public static readonly Element Pd = new("Palladium", "Pd", 46);
    public static readonly Element Ag = new("Silver", "Ag", 47);
    public static readonly Element Cd = new("Cadmium", "Cd", 48);
    public static readonly Element In = new("Indium", "In", 49);
    public static readonly Element Sn = new("Tin", "Sn", 50);
    public static readonly Element Sb = new("Antimony", "Sb", 51);
    public static readonly Element Te = new("Tellurium", "Te", 52);
    public static readonly Element I = new("Iodine", "I", 53);
    public static readonly Element Xe = new("Xenon", "Xe", 54);
    public static readonly Element Cs = new("Cesium", "Cs", 55);
    public static readonly Element Ba = new("Barium", "Ba", 56);
    public static readonly Element La = new("Lanthanum", "La", 57);
    public static readonly Element Ce = new("Cerium", "Ce", 58);
    public static readonly Element Pr = new("Praseodymium", "Pr", 59);
    public static readonly Element Nd = new("Neodymium", "Nd", 60);
    public static readonly Element Pm = new("Promethium", "Pm", 61);
    public static readonly Element Sm = new("Samarium", "Sm", 62);
    public static readonly Element Eu = new("Europium", "Eu", 63);
    public static readonly Element Gd = new("Gadolinium", "Gd", 64);
    public static readonly Element Tb = new("Terbium", "Tb", 65);
    public static readonly Element Dy = new("Dysprosium", "Dy", 66);
    public static readonly Element Ho = new("Holmium", "Ho", 67);
    public static readonly Element Er = new("Erbium", "Er", 68);
    public static readonly Element Tm = new("Thulium", "Tm", 69);
    public static readonly Element Yb = new("Ytterbium", "Yb", 70);
    public static readonly Element Lu = new("Lutetium", "Lu", 71);
    public static readonly Element Hf = new("Hafnium", "Hf", 72);
    public static readonly Element Ta = new("Tantalum", "Ta", 73);
    public static readonly Element W = new("Tungsten", "W", 74);
    public static readonly Element Re = new("Rhenium", "Re", 75);
    public static readonly Element Os = new("Osmium", "Os", 76);
    public static readonly Element Ir = new("Iridium", "Ir", 77);
    public static readonly Element Pt = new("Platinum", "Pt", 78);
    public static readonly Element Au = new("Gold", "Au", 79);
    public static readonly Element Hg = new("Mercury", "Hg", 80);
    public static readonly Element Tl = new("Thallium", "Tl", 81);
    public static readonly Element Pb = new("Lead", "Pb", 82);
    public static readonly Element Bi = new("Bismuth", "Bi", 83);
    public static readonly Element Po = new("Polonium", "Po", 84);
    public static readonly Element At = new("Astatine", "At", 85);
    public static readonly Element Rn = new("Radon", "Rn", 86);
    public static readonly Element Fr = new("Francium", "Fr", 87);
    public static readonly Element Ra = new("Radium", "Ra", 88);
    public static readonly Element Ac = new("Actinium", "Ac", 89);
    public static readonly Element Th = new("Thorium", "Th", 90);
    public static readonly Element Pa = new("Protactinium", "Pa", 91);
    public static readonly Element U = new("Uranium", "U", 92);
    public static readonly Element Np = new("Neptunium", "Np", 93);
    public static readonly Element Pu = new("Plutonium", "Pu", 94);
    public static readonly Element Am = new("Americium", "Am", 95);
    public static readonly Element Cm = new("Curium", "Cm", 96);
    public static readonly Element Bk = new("Berkelium", "Bk", 97);
    public static readonly Element Cf = new("Californium", "Cf", 98);
    public static readonly Element Es = new("Einsteinium", "Es", 99);
    public static readonly Element Fm = new("Fermium", "Fm", 100);
    public static readonly Element Md = new("Mendelevium", "Md", 101);
    public static readonly Element No = new("Nobelium", "No", 102);
    public static readonly Element Lr = new("Lawrencium", "Lr", 103);
    public static readonly Element Rf = new("Rutherfordium", "Rf", 104);
    public static readonly Element Db = new("Dubnium", "Db", 105);
    public static readonly Element Sg = new("Seaborgium", "Sg", 106);
    public static readonly Element Bh = new("Bohrium", "Bh", 107);
    public static readonly Element Hs = new("Hassium", "Hs", 108);
    public static readonly Element Mt = new("Meitnerium", "Mt", 109);
    public static readonly Element Ds = new("Darmstadtium", "Ds", 110);
    public static readonly Element Rg = new("Roentgenium", "Rg", 111);
    public static readonly Element Cn = new("Copernicium", "Cn", 112);
    public static readonly Element Nh = new("Nihonium", "Nh", 113);
    public static readonly Element Fl = new("Flerovium", "Fl", 114);
    public static readonly Element Mc = new("Moscovium", "Mc", 115);
    public static readonly Element Lv = new("Livermorium", "Lv", 116);
    public static readonly Element Ts = new("Tennessine", "Ts", 117);
    public static readonly Element Og = new("Oganesson", "Og", 118);

    public static readonly Element[] Elements =
    {
        Element.H, Element.He, Element.Li, Element.Be,
        Element.B, Element.C, Element.N, Element.O,
        Element.F, Element.Ne, Element.Na, Element.Mg,
        Element.Al, Element.Si, Element.P, Element.S,
        Element.Cl, Element.Ar, Element.K, Element.Ca,
        Element.Sc, Element.Ti, Element.V, Element.Cr,
        Element.Mn, Element.Fe, Element.Co, Element.Ni,
        Element.Cu, Element.Zn, Element.Ga, Element.Ge,
        Element.As, Element.Se, Element.Br, Element.Kr,
        Element.Rb, Element.Sr, Element.Y, Element.Zr,
        Element.Nb, Element.Mo, Element.Tc, Element.Ru,
        Element.Rh, Element.Pd, Element.Ag, Element.Cd,
        Element.In, Element.Sn, Element.Sb, Element.Te,
        Element.I, Element.Xe, Element.Cs, Element.Ba,
        Element.La, Element.Ce, Element.Pr, Element.Nd,
        Element.Pm, Element.Sm, Element.Eu, Element.Gd,
        Element.Tb, Element.Dy, Element.Ho, Element.Er,
        Element.Tm, Element.Yb, Element.Lu, Element.Hf,
        Element.Ta, Element.W, Element.Re, Element.Os,
        Element.Ir, Element.Pt, Element.Au, Element.Hg,
        Element.Tl, Element.Pb, Element.Bi, Element.Po,
        Element.At, Element.Rn, Element.Fr, Element.Ra,
        Element.Ac, Element.Th, Element.Pa, Element.U,
        Element.Np, Element.Pu, Element.Am, Element.Cm,
        Element.Bk, Element.Cf, Element.Es, Element.Fm,
        Element.Md, Element.No, Element.Lr, Element.Rf,
        Element.Db, Element.Sg, Element.Bh, Element.Hs,
        Element.Mt, Element.Ds, Element.Rg, Element.Cn,
        Element.Nh, Element.Fl, Element.Mc, Element.Lv,
        Element.Ts, Element.Og
    };

    public float AtomicNumber { get; }
    public string Name { get; }
    public string Symbol { set; get; }

    public Element(string name, string symbol, float atomicNumber)
    {
        this.Name = name;
        this.Symbol = symbol;
        this.AtomicNumber = atomicNumber;
    }

    public static Element? FindBySymbol(string symbol)
    {
        return Element.Elements.FirstOrDefault(e => e.Symbol.Equals(
            symbol, StringComparison.OrdinalIgnoreCase));
    }

    public static CompoundComponent operator *(Element element, int amount) => new(element, amount);
}