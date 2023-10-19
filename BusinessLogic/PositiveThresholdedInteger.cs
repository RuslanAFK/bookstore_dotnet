namespace Services;

public class PositiveThresholdedInteger
{
    private readonly uint _minValue = 0;
    private uint _maxValue = int.MaxValue;
    private readonly uint _defaultValue;

    public PositiveThresholdedInteger(int defaultValue)
    {
        var uintDefault = ReturnUintOrThrow(defaultValue);
        _defaultValue = uintDefault;
    }

    public void SetMaxThreshold(int maxValue)
    {
        var max = ReturnUintOrThrow(maxValue);
        _maxValue = max;
    }

    public int GetValue(int? desiredValue=null)
    {
        uint? uintDesired = null;
        if (desiredValue.HasValue)
        {
            uintDesired = ReturnUintOrThrow(desiredValue.Value);
        }
        if (uintDesired.HasValue && IsInRange(uintDesired.Value))
        {
            return (int)uintDesired.Value;
        }
        if (IsInRange(_defaultValue))
        {
            return (int)_defaultValue;
        }

        return (int)_maxValue;
    }
    private bool IsInRange(uint value)
    {
        return value > _minValue && value < _maxValue;
    }

    private uint ReturnUintOrThrow(int value)
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException();
        }

        return (uint)value;
    }
}