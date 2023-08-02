namespace Data.Test;

public class PositiveIntegerTest
{
    [Test]
    public void GetValue_WhenThresholdIsNotSetAndDesiredIsNotNull_ReturnsDesiredValue()
    {
        var desired = 100;
        var obj = new PositiveThresholdedInteger(5);
        var results = obj.GetValue(desired);
        Assert.That(results, Is.EqualTo(desired));
    }
    [Test]
    public void GetValue_WhenThresholdIsNotSetAndDesiredIsNull_ReturnsDefaultValue()
    {
        var defaultValue = 100;
        var obj = new PositiveThresholdedInteger(defaultValue);
        var results = obj.GetValue();
        Assert.That(results, Is.EqualTo(defaultValue));
    }
    [Test]
    public void GetValue_WhenThresholdIsGreaterThanDesired_ReturnsDesiredValue()
    {
        var defaultValue = 100;
        var desired = 50;
        var max = 150;
        var obj = new PositiveThresholdedInteger(defaultValue);
        obj.SetMaxThreshold(max);
        var results = obj.GetValue(desired);
        Assert.That(results, Is.EqualTo(desired));
    }
    [Test]
    public void GetValue_WhenThresholdIsSmallerThanDesired_ReturnsDefaultValue()
    {
        var defaultValue = 100;
        var desired = 500;
        var max = 105;
        var obj = new PositiveThresholdedInteger(defaultValue);
        obj.SetMaxThreshold(max);
        var results = obj.GetValue(desired);
        Assert.That(results, Is.EqualTo(defaultValue));
    }
    [Test]
    public void GetValue_WhenThresholdIsSetAndDesiredIsNull_ReturnsDefaultValue()
    {
        var defaultValue = 100;
        var obj = new PositiveThresholdedInteger(defaultValue);
        obj.SetMaxThreshold(155);
        var results = obj.GetValue();
        Assert.That(results, Is.EqualTo(defaultValue));
    }
    [Test]
    public void GetValue_MaxIsSmallerThanDefault_ReturnsMax()
    {
        var defaultValue = 100;
        var max = 90;
        var obj = new PositiveThresholdedInteger(defaultValue);
        obj.SetMaxThreshold(max);
        var results = obj.GetValue();
        Assert.That(results, Is.EqualTo(max));
    }
    [Test]
    public void AnyMethod_NegativeValuePasses_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            new PositiveThresholdedInteger(-10);
        });
    }
}