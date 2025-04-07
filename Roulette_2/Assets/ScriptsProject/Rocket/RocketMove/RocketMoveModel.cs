using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class RocketMoveModel
{
    public event Action<int> OnSetCourseRoute;
    public event Action<int> OnMoveToLeft;
    public event Action<int> OnMoveToRight;
    public event Action OnMoveToWinLeft;
    public event Action OnMoveToWinRight;

    public event Action OnMoveToBase;
    public event Action OnMoveToStart;

    private const int minRouteNumber = 0;
    private const int maxRouteNumber = 8;
    private int currentRouteNumber = 4;

    public void MoveLeft()
    {
        Left(1);
    }

    public void MoveRight()
    {
        Right(1);
    }

    public void MoveLeftDouble()
    {
        Left(2);
    }

    public void MoveRightDouble()
    {
        Right(2);
    }

    private void Left(int steps)
    {
        currentRouteNumber -= 1;

        if (currentRouteNumber <= minRouteNumber)
        {
            Debug.Log("FAIL: LEFT ROUTE OUT");
            currentRouteNumber = minRouteNumber;
            OnMoveToWinLeft?.Invoke();
            return;
        }

        OnSetCourseRoute?.Invoke(currentRouteNumber);
        OnMoveToLeft?.Invoke(currentRouteNumber);
    }

    private void Right(int steps)
    {
        currentRouteNumber += steps;

        if (currentRouteNumber >= maxRouteNumber)
        {
            Debug.Log("FAIL: RIGHT ROUTE OUT");
            currentRouteNumber = maxRouteNumber;
            OnMoveToWinRight?.Invoke();
            return;
        }

        OnSetCourseRoute?.Invoke(currentRouteNumber);
        OnMoveToRight?.Invoke(currentRouteNumber);
    }

    public void Restart()
    {
        currentRouteNumber = 4;
        OnSetCourseRoute?.Invoke(currentRouteNumber);
    }

    public void MoveToBase()
    {
        OnMoveToBase?.Invoke();
    }

    public void MoveToStart()
    {
        OnMoveToStart?.Invoke();
    }
}
