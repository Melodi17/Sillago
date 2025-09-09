using System.Collections;

namespace Sillago.Materials.Types;

using Items;

public class BioMaterial : Material
{
    public Material? FeedsOn; // The material this bio material feeds on
    public float MinTemp;
    public float MaxTemp;

    public BioMaterial(
        string name,
        int variation,
        MaterialFlags flags,
        float minTemp,
        float maxTemp,
        Material feedsOn)
        : base(name)
    {
        this.Name = name;
        Compound culture = BioMaterial.CreateMicrobialCulture(variation);
        this.Symbol = culture;
        this.Color = BioMaterial.CreateColor(variation);
        this.VisualSet = VisualSet.Default;
        this.Flags = flags;

        this.Density = 1000f; // Arbitrary density for microbial culture
        this.FeedsOn = feedsOn;

        this.MinTemp = minTemp;
        this.MaxTemp = maxTemp;
    }

    private static Compound CreateMicrobialCulture(int variation)
    {
        const int scale = 5;

        int deltaC = ((variation         % 5) - 2) * scale; // -2 to +2
        int deltaH = (((variation / 5)   % 7) - 3) * scale; // -3 to +3
        int deltaO = (((variation / 35)  % 3) - 1) * scale; // -1 to +1
        int deltaN = (((variation / 105) % 3) - 1) * scale;

        int c = 1000 + deltaC; // base value of 1000
        int h = 2000 + deltaH; // base value of 2000
        int o = 1000 + deltaO; // base value of 1000
        int n = 300  + deltaN; // base value of 300

        return new Compound(
            "MicrobialCulture",
            Element.C * c, Element.H * h, Element.O * o, Element.N * n);
    }

    private static int CreateColor(int variation)
    {
        return 0xA3C1AD * variation;
    }

    public override IEnumerator Generate()
    {
        ItemMaterial culture = new ItemMaterial(this, MaterialType.Culture);
        yield return culture;

        if (this.FeedsOn != null)
            yield return this.Deferred(() =>
                culture.Incubates(this.MinTemp, this.MaxTemp, this.FeedsOn));
    }
}