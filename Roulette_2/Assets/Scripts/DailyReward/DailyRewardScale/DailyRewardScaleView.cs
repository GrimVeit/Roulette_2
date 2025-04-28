using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class DailyRewardScaleView : View
{
    [SerializeField] private List<DailyRewardScale> dailyRewardScales = new List<DailyRewardScale>();
    [SerializeField] private Transform transformScale;
    [SerializeField] private float speedMove;

    private Tween moveTween;

    public void SetIndex(int index)
    {
        Debug.Log(index);

        var drScale = dailyRewardScales.FirstOrDefault(dsScale => dsScale.Day == index);

        moveTween?.Kill();
        moveTween = transformScale.DOMove(drScale.GetPosition(), speedMove);
    }
}

[System.Serializable]
public class DailyRewardScale
{
    [SerializeField] private int day;
    [SerializeField] private Transform transformScale;

    public int Day => day;

    public Vector3 GetPosition()
    {
        return transformScale.position;
    }
}
