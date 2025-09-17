namespace Sillago.Requirements
{
    using System;
    using System.Diagnostics;
    using Capabilities;

    public class TemperatureRequirement : IRecipeRequirement
    {
        public float? Min { get; }
        public float? Max { get; }
    
        protected TemperatureRequirement(float? min = null, float? max = null)
        {
            this.Min = min;
            this.Max = max;
        
            if (min == null && max == null)
                throw new ArgumentException("At least one of atLeast or atMax must be specified.");
        }
    
        public static TemperatureRequirement Above(float temperature) => new(min: temperature);
        public static TemperatureRequirement Below(float temperature) => new(max: temperature);
        public static TemperatureRequirement Between(float minTemperature, float maxTemperature)
        {
            if (minTemperature > maxTemperature)
                throw new ArgumentException("minTemperature cannot be greater than maxTemperature.");
        
            return new(min: minTemperature, max: maxTemperature);
        }
    
        public string GetInfo()
        {
            if (this.Min != null && this.Max != null)
                return $"Temperature between {this.Min:0.##}°C and {this.Max:0.##}°C";
            if (this.Min != null)
                return $"Temperature at least {this.Min:0.##}°C";
            if (this.Max != null)
                return $"Temperature less than or equal to {this.Max:0.##}°C";
        
            throw new UnreachableException("Invalid state: both Min and Max are null.");
        }
    
        public bool IsMet(IMachine machine)
        {
            ITemperatureCapability capability = machine.Get<ITemperatureCapability>();
        
            if (this.Min != null && capability.Temperature < this.Min)
                return false;
        
            if (this.Max != null && capability.Temperature > this.Max)
                return false;
        
            return true;
        }
    }
}