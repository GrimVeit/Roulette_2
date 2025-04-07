using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScoreMultiplyProvider
{
    public event Action<IScoreMultiply> OnApplyScoreMultiply;
}
