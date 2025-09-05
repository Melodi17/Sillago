namespace Sillago.Recipes;

using Utils;

public enum EquipmentStatus
{
    [Friendly("Working has been disabled")]         Disabled,
    [Friendly("Idle")]                              Idle,
    [Friendly("Working")]                           Working,
    [Friendly("Waiting for required input items")]  WaitingForInput,
    [Friendly("Waiting for space in output")]       WaitingForOutput,
    [Friendly("Recipe not set")]                    RecipeNotSet,
    [Friendly("Heating to required temp")]          Heating,
    [Friendly("Set temperature is not sufficient")] RequiredTemperatureNotMet
}