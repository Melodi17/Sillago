namespace Sillago
{
    public class SulfuricAcidLine
    {
        public static void Register()
        {
            var sulfur = Items.GetMaterialForm(Materials.Sulfur, MaterialType.Powder);
            var oxygen = Items.GetMaterialForm(Materials.Oxygen, MaterialType.Gas);
            var water = Items.GetMaterialForm(Materials.Water, MaterialType.Liquid);
            var sulfuricAcid = Items.GetMaterialForm(Materials.SulfuricAcid, MaterialType.Liquid);
            var sulfurTrioxide = Items.GetMaterialForm(Materials.SulfurTrioxide, MaterialType.Gas);
            var oleum = Items.GetMaterialForm(Materials.Oleum, MaterialType.Liquid);
            // var 
        }
    }
}