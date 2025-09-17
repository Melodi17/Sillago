namespace Sillago.Tests
{
    using Helpers;
    using Utils;

    [TestFixture]
    public class EnumExtensionsTests
    {
        [Test]
        public void EnumIs_MultipleFlagSet_ReturnsTrue()
        {
            DummyFlagEnum flags = DummyFlagEnum.FlagA | DummyFlagEnum.FlagC;
        
            Assert.That(flags.Is(DummyFlagEnum.FlagA), Is.True, "FlagA should be set");
            Assert.That(flags.Is(DummyFlagEnum.FlagC), Is.True, "FlagC should be set");
            Assert.That(flags.Is(DummyFlagEnum.FlagB), Is.False, "FlagB should not be set");
        }
    
        [Test]
        public void EnumIs_NoFlagsSet_ReturnsFalse()
        {
            DummyFlagEnum flags = DummyFlagEnum.None;
        
            Assert.That(flags.Is(DummyFlagEnum.FlagA), Is.False, "FlagA should not be set");
            Assert.That(flags.Is(DummyFlagEnum.FlagB), Is.False, "FlagB should not be set");
            Assert.That(flags.Is(DummyFlagEnum.FlagC), Is.False, "FlagC should not be set");
        }
    
        [Test]
        public void EnumIs_SingleFlagSet_ReturnsTrue()
        {
            DummyFlagEnum flags = DummyFlagEnum.FlagA;
        
            Assert.That(flags.Is(DummyFlagEnum.FlagA), Is.True, "FlagA should be set");
            Assert.That(flags.Is(DummyFlagEnum.FlagB), Is.False, "FlagB should not be set");
            Assert.That(flags.Is(DummyFlagEnum.FlagC), Is.False, "FlagC should not be set");
        }
    }
}