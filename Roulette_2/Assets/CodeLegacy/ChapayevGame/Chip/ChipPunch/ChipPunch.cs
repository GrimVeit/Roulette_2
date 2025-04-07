using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ChipPunch : MonoBehaviour
{
    private float scaleSize = 1;

    private Tween tweenScale;

    public void SetData(float scale)
    {
        scaleSize = scale;
    }

    public void Initialize()
    {
        Coroutines.Start(Timer());
    }

    private IEnumerator Timer()
    {
        tweenScale = transform.DOScale(scaleSize, 0.0f);

        yield return new WaitForSeconds(0.1f);

        tweenScale?.Kill();

        Destroy(gameObject);
    }
}
