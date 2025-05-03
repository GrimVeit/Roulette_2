using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HandPointer : MonoBehaviour
{
    [SerializeField] private Transform transformHand;

    private Tween tweenScale;
    private Tween tweenMove;
    private Tween tweenRotate;

    public void Activate()
    {
        tweenScale?.Kill();

        transformHand.gameObject.SetActive(true);

        tweenScale = transformHand.DOScale(1, 0.3f);
    }

    public void Deactivate()
    {
        tweenScale?.Kill();

        tweenScale = transformHand.DOScale(0, 0.3f).OnComplete(() => transformHand.gameObject.SetActive(false));
    }

    public void Move(Vector3 vectorPosition, Vector3 vectorRotate)
    {
        tweenMove?.Kill();
        tweenRotate?.Kill();

        tweenMove = transformHand.DOMove(vectorPosition, 0.3f);
        tweenRotate = transformHand.DORotate(vectorRotate, 0.3f);
    }
}