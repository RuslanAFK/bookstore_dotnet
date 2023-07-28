namespace Data;

public class PositiveInteger
{
    private readonly int _minValue = 0;
    private int _maxValue = int.MaxValue;
    private readonly int _defaultValue;

    public PositiveInteger(int defaultValue)
    {
        _defaultValue = defaultValue;
    }

    public void SetMaxThreshold(int maxValue)
    {
        _maxValue = maxValue;
    }

    public int GetValue(int? desiredValue)
    {
        if (desiredValue.HasValue && IsInRange(_defaultValue))
        {
            return desiredValue.Value;
        }

        return _defaultValue;
    }
    private bool IsInRange(int value)
    {
        return value > _minValue && value < _maxValue;
    }
}