namespace Sillago.Tests
{
    using System;
    using NUnit.Framework;
    using Utils;

    [TestFixture]
    public class IdentifierTests
    {

        [Test]
        public void Create_NullInput_Fails()
        {
            Assert.Throws<ArgumentException>(() => Identifier.Create(null!));
        }
    
        [TestCase("")]
        [TestCase("   ")]
        public void Identifier_WhitespaceOnly_Throws(string input)
        {
            Assert.Throws<ArgumentException>(() => Identifier.Create(input));
        }

        [Test]
        public void Identifier_ValidString_ReturnsLowercaseTrimmedAndUnderscored()
        {
            string result = Identifier.Create("  Hello World  ");
            Assert.That(result, Is.EqualTo("hello_world"));
        }

        [Test]
        public void Identifier_RemovesUnsupportedCharacters()
        {
            string result = Identifier.Create("Hello@World!");
            Assert.That(result, Is.EqualTo("helloworld"));
        }
    
        [Test]
        public void Identifier_ConvertsDashesToUnderscores()
        {
            string result = Identifier.Create("Hello-World");
            Assert.That(result, Is.EqualTo("hello_world"));
        }

        [Test]
        public void Identifier_OnlyUnsupportedCharacters_Throws()
        {
            Assert.Throws<ArgumentException>(() => Identifier.Create("$$$"));
        }

        [Test]
        public void Identifier_AlreadyValidIdentifier_RemainsSameExceptLowercase()
        {
            string result = Identifier.Create("My_Var123");
            Assert.That(result, Is.EqualTo("my_var123"));
        }
    }
}