using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GameArrowView : View
{
    [SerializeField] private Transform arrowTransform;
    [SerializeField] private float speedRotate;

    private Tween tweenMove;

    public void RotateArrow(int angle)
    {
        tweenMove?.Kill();

        tweenMove = arrowTransform.DOLocalRotate(new Vector3(0, 0, angle), speedRotate);
    }
}
