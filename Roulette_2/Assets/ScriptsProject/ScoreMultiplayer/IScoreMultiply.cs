using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScoreMultiply
{
    public float ApplyMultiply(float score);
}

public class SummScoreMultiply : IScoreMultiply
{
    public int SumValue => _sumValue;

    private int _sumValue;

    public SummScoreMultiply(int sumValue)
    {
        _sumValue = sumValue;
    }

    public float ApplyMultiply(float score)
    {
        return _sumValue + score;
    }
}

public class MultiplyScoreMultiply : IScoreMultiply
{
    public int MultiplyValue => _multiplyValue;

    private int _multiplyValue;

    public MultiplyScoreMultiply(int multiplyValue)
    {
        _multiplyValue = multiplyValue;
    }

    public float ApplyMultiply(float score)
    {
        return _multiplyValue * score;
    }
}

public class DivideScoreMultiply : IScoreMultiply
{
    public int DivideValue => _divideValue;

    private int _divideValue;

    public DivideScoreMultiply(int divideValue)
    {
        _divideValue = divideValue;
    }

    public float ApplyMultiply(float score)
    {
        return score / _divideValue;
    }
}
