namespace Sillago.Tests.Helpers
{
    using System;

    [Flags]
    public enum DummyFlagEnum
    {
        None = 0,
        FlagA = 1 << 0,
        FlagB = 1 << 1,
        FlagC = 1 << 2
    }
}