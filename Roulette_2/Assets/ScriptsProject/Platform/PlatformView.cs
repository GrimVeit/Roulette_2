using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlatformView : View
{
    [SerializeField] private Transform transformPlatform;

    [SerializeField] private Transform transformActivate;
    [SerializeField] private Transform transformDeactivate;

    public void ActivatePlatform()
    {
        transformPlatform.DOMove(transformActivate.position, 0.4f);
        transformPlatform.DOLocalRotate(Vector3.zero, 0.4f);
    }

    public void DeactivatePlatform()
    {
        transformPlatform.DOMove(transformDeactivate.position, 0.4f);
        transformPlatform.DOLocalRotate(new Vector3(0, 0, -180), 0.4f);
    }
}
