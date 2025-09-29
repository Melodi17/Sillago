namespace Sillago
{
    public enum LightType
    {
        UV,
        DUV,
        EUV
    }

    public class Dopant
    {
        public Material Material { get; }
        public int Concentration { get; } // 0-100 (%)

        public Dopant(Material material, int concentration)
        {
            this.Material = material;
            this.Concentration = concentration;
        }
    }

    public class Exposure
    {
        public LightType Type { get; }
        public float Dose { get; }      // mJ/cm²
        public int FocalLength { get; } // nm

        public Exposure(LightType type, float dose, int focalLength)
        {
            this.Type = type;
            this.Dose = dose;
            this.FocalLength = focalLength;
        }
    }
}