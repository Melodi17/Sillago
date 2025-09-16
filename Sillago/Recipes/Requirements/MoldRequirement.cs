namespace Sillago.Requirements;

using Capabilities;

public class MoldRequirement : IRecipeRequirement
{
    public MaterialType MoldType { get; }
    protected MoldRequirement(MaterialType moldType)
    {
        this.MoldType = moldType;
    }
    
    private static readonly Dictionary<MaterialType, MoldRequirement> _cache = new();
    public static MoldRequirement Of(MaterialType moldType)
    {
        if (!MoldRequirement._cache.ContainsKey(moldType))
            MoldRequirement._cache[moldType] = new MoldRequirement(moldType);
        return MoldRequirement._cache[moldType];
    }
    
    public string GetInfo() => $"Requires {this.MoldType} mold";
    public bool IsMet(IMachine machine)
    {
        IMoldCapability capability = machine.Get<IMoldCapability>();
        return capability.MoldType == this.MoldType;
    }
}