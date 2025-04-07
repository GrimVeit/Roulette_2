using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameArrowModel
{
    public event Action<int> OnRotateArrow;

    public void RotateDown()
    {
        RotateArrow(0);
    }

    public void RotateUp()
    {
        RotateArrow(-180);
    }

    private void RotateArrow(int angle)
    {
        OnRotateArrow(angle);
    }
}
