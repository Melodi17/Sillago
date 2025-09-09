namespace Sillago.Recipes;

using System.Diagnostics;

public interface IRecipeRequirement
{
    string GetInfo();
    bool IsMet(MachineState state);
}

public class TemperatureRequirement : IRecipeRequirement
{
    public float? AtLeast { get; }
    public float? AtMax { get; }
    
    public TemperatureRequirement(float? atLeast = null, float? atMax = null)
    {
        this.AtLeast = atLeast;
        this.AtMax = atMax;
        
        if (atLeast == null && atMax == null)
            throw new ArgumentException("At least one of atLeast or atMax must be specified.");
    }
    
    public string GetInfo()
    {
        if (this.AtLeast != null && this.AtMax != null)
            return $"Temperature between {this.AtLeast:0.##}°C and {this.AtMax:0.##}°C";
        if (this.AtLeast != null)
            return $"Temperature at least {this.AtLeast:0.##}°C";
        if (this.AtMax != null)
            return $"Temperature less than or equal to {this.AtMax:0.##}°C";
        
        throw new UnreachableException("Invalid state: both AtLeast and AtMax are null.");
    }
    
    public bool IsMet(MachineState state)
    {
        TemperatureCapability capability = state.Get<TemperatureCapability>();
        
        if (this.AtLeast != null && capability.Temperature < this.AtLeast)
            return false;
        
        if (this.AtMax != null && capability.Temperature > this.AtMax)
            return false;
        
        return true;
    }
}