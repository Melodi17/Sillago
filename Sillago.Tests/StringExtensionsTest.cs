namespace Sillago.Tests;

using Utils;

[TestFixture]
public class StringExtensionsTest
{

    [Test]
    public void TitleCase_Works()
    {
        Assert.That("hello world".TitleCase(), Is.EqualTo("Hello World"));
        Assert.That("HELLO WORLD".TitleCase(), Is.EqualTo("Hello World"));
        Assert.That("hElLo WoRlD".TitleCase(), Is.EqualTo("Hello World"));
        Assert.That("hello-world".TitleCase(), Is.EqualTo("Hello-World"));
        Assert.That("hello_world".TitleCase(), Is.EqualTo("Hello_World"));
        Assert.That("hello.world".TitleCase(), Is.EqualTo("Hello.World"));
        Assert.That("helloWorld".TitleCase(), Is.EqualTo("Helloworld")); // Note: camel case is not handled
        Assert.That("hello  world".TitleCase(), Is.EqualTo("Hello  World")); // Note: multiple spaces are preserved
    }
    
    [Test]
    public void TitleCase_EmptyString_Works()
    {
        Assert.That("".TitleCase(), Is.EqualTo(""));
    }
}