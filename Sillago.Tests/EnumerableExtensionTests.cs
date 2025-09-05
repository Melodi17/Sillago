namespace Sillago.Tests;

using Utils;

[TestFixture]
public class EnumerableExtensionTests
{
    [Test]
    public void WeightedColorwiseSum_Succeeds()
    {
        var items = new[]
        {
            new { Weight = 0.2f, Color = 0xFF0000 }, // Red
            new { Weight = 0.3f, Color = 0x00FF00 }, // Green
            new { Weight = 0.5f, Color = 0x0000FF }  // Blue
        };
        
        int expectedColor = 0x334C7F;
        
        int givenColor = items.WeightedColorwiseSum(
            item => item.Weight,
            item => item.Color
        );
        
        Assert.That(givenColor, Is.EqualTo(expectedColor), "Weighted colorwise sum did not match expected color");
    }
    
    [Test]
    public void WeightedColorwiseSum_ZeroTotalWeight_ReturnsBlack()
    {
        var items = new[]
        {
            new { Weight = 0.0f, Color = 0xFF0000 }, // Red
            new { Weight = 0.0f, Color = 0x00FF00 }, // Green
            new { Weight = 0.0f, Color = 0x0000FF }  // Blue
        };
        
        int expectedColor = 0x000000; // Black
        
        int givenColor = items.WeightedColorwiseSum(
            item => item.Weight,
            item => item.Color
        );
        
        Assert.That(givenColor, Is.EqualTo(expectedColor), "Weighted colorwise sum with zero total weight did not return black");
    }
    
    [Test]
    public void WeightedSum_Succeeds()
    {
        var items = new[]
        {
            new { Weight = 1.0f, Value = 10.0f },
            new { Weight = 2.0f, Value = 20.0f },
            new { Weight = 3.0f, Value = 30.0f }
        };
        
        float expectedWeightedSum = (1*10f + 2*20f + 3*30f) / (1 + 2 + 3); // 23.3333...
        
        float givenWeightedSum = items.WeightedSum(
            item => item.Weight,
            item => item.Value
        );
        
        Assert.That(givenWeightedSum, Is.EqualTo(expectedWeightedSum).Within(0.0001), "Weighted sum did not match expected value");
    }
    
    [Test]
    public void WeightedSum_ZeroTotalWeight_ReturnsZero()
    {
        var items = new[]
        {
            new { Weight = 0.0f, Value = 10.0f },
            new { Weight = 0.0f, Value = 20.0f },
            new { Weight = 0.0f, Value = 30.0f }
        };
        
        float expectedWeightedSum = 0.0f;
        
        float givenWeightedSum = items.WeightedSum(
            item => item.Weight,
            item => item.Value
        );
        
        Assert.That(givenWeightedSum, Is.EqualTo(expectedWeightedSum), "Weighted sum with zero total weight did not return zero");
    }
}