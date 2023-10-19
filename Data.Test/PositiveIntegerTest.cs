namespace Data.Test;

public class PositiveIntegerTest
{
    [Theory]
    [TestCase(15, 10, 15)]
    [TestCase(null, 10, 10)]
    public void GetValue_WhenThresholdIsNotSet
        (int? desiredValue, int defaultValue, int expectedValue)
    {
        var obj = new PositiveThresholdedInteger(defaultValue);
        var results = obj.GetValue(desiredValue);
        Assert.That(results, Is.EqualTo(expectedValue));
    }
    [Theory]
    [TestCase(5, 10, 15, 5)]
    [TestCase(20, 10, 15, 10)]
    [TestCase(15, 10, 5, 5)]
    [TestCase(null, 10, 15, 10)]
    [TestCase(null, 10, 5, 5)]
    public void GetValue_WhenThresholdIsSet
        (int? desiredValue, int defaultValue, int threshold, int expectedValue)

    {
        var obj = new PositiveThresholdedInteger(defaultValue);
        obj.SetMaxThreshold(threshold);
        var results = obj.GetValue(desiredValue);
        Assert.That(results, Is.EqualTo(expectedValue));
    }
    [Test]
    public void AnyMethod_NegativeValuePasses_ThrowsArgumentOutOfRangeException()
    {
        var negativeValue = -15;
        Assert.Throws<ArgumentOutOfRangeException>(() => { _ = new PositiveThresholdedInteger(negativeValue); });
    }
}