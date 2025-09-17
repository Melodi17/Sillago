namespace Sillago.Tests
{
    using Helpers;
    using NUnit.Framework;
    using Utils;

    [TestFixture]
    public class ExtensionTests
    {

        [Test]
        public void UnpackColor_Succeeds()
        {
            const int color = 0x1FDADB;
            (int r, int g, int b) expected = (31, 218, 219);

            (int r, int g, int b) given = Extensions.UnpackColor(color);
        
            Assert.Multiple(() =>
            {
                Assert.That(given.r, Is.EqualTo(expected.r), "Resulting color's r channel did not match expected's r channel");
                Assert.That(given.g, Is.EqualTo(expected.g), "Resulting color's g channel did not match expected's g channel");
                Assert.That(given.b, Is.EqualTo(expected.b), "Resulting color's b channel did not match expected's b channel");
            });
        }
    
        [Test]
        public void PackColor_Succeeds()
        {
            (int r, int g, int b) color = (31, 218, 219);
            const int expected = 0x1FDADB;

            int given = Extensions.PackColor(color.r, color.g, color.b);
        
            Assert.That(given, Is.EqualTo(expected), "Packed color did not match expected color");
        }
    }
}