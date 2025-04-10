using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteNumber : MonoBehaviour
{
    [SerializeField] private int number;
    [SerializeField] private ParityNumber parity;
    [SerializeField] private ColorNumber color;
    [SerializeField] private RowNumber row;
    [SerializeField] private ColumnNumber column;
    [SerializeField] private List<SixNumberRange> sixNumberRanges;

    public int Number => number;
    public ParityNumber Parity => parity;
    public ColorNumber Color => color;
    public RowNumber Row => row;
    public ColumnNumber Column => column;
    public List<SixNumberRange> SixNumberRanges => sixNumberRanges;

}

public enum ParityNumber
{
    Even, Odd
}

public enum ColorNumber
{
    Red, Black, Green
}

public enum RowNumber
{
    None, First, Second, Third
}

public enum ColumnNumber
{
    None, First, Second, Third
}

public enum SixNumberRange
{
    None, Range1to6, Range4to9,  Range7to12
}
