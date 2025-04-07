using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Rocket : MonoBehaviour
{
    public int CourseRoute = 4;

    [SerializeField] private Transform transformSpriteRocket;
    [SerializeField] private Image imageRocketStatic;
    [SerializeField] private Image imageRocketFire;
    [SerializeField] private Sprite spriteFireOne;
    [SerializeField] private Sprite spriteFireTwo;

    private Tween moveTween;
    private Tween shakeTween;

    private Sequence rotateSequence;
    private Sequence sequenceMoveBase;
    private Sequence sequenceMoveStart;

    public void SetRoute(int route)
    {
        CourseRoute = route;
    }

    public void MoveTo(Vector3 target, float time)
    {
        moveTween?.Kill();

        moveTween = transform.DOMove(target, time);
    }

    public void RotateTo(Vector3 vectorRotate, float time)
    {
        rotateSequence?.Kill();

        rotateSequence = DOTween.Sequence();
        rotateSequence.Append(transform.DOLocalRotate(vectorRotate, time / 4));
        rotateSequence.AppendInterval(time/2);
        rotateSequence.Append(transform.DOLocalRotate(Vector3.zero, time / 4));
        rotateSequence.Play();
    }

    public void Shake(float duration, float strength, int vibrato, float randomness)
    {
        shakeTween?.Kill();
        transformSpriteRocket.localRotation = Quaternion.Euler(Vector3.zero);

        shakeTween = transformSpriteRocket.DOShakeRotation(duration, strength, vibrato, randomness);
    }

    public void MoveToBase(Vector3 start, Vector3 up, Vector3 play)
    {
        sequenceMoveBase?.Kill();

        transform.position = start;
        ActivateFireTwo();
        sequenceMoveBase = DOTween.Sequence();
        sequenceMoveBase.Append(transform.DOMove(up, 2f).SetEase(Ease.OutCubic).OnComplete(() => 
        {
            ActivateFireOne();
            Shake(0.7f, 10f, 200, 500);
            OnPauseMoveToBase?.Invoke();
        }));

        sequenceMoveBase.Append(transform.DOMove(play, 0.5f).SetEase(Ease.InOutCubic).OnComplete(() => 
        {
            ActivateZero();
            OnEndMoveToBase?.Invoke();
        }));

        sequenceMoveBase.Play();
    }

    public void MoveToStart(Vector3 target)
    {
        sequenceMoveStart?.Kill();

        Shake(1f, 10f, 200, 500);

        ActivateFireOne();

        sequenceMoveStart = DOTween.Sequence();
        sequenceMoveStart.AppendInterval(0.1f).OnComplete(() =>
        {

        });

        sequenceMoveStart.Append(transform.DOMove(target, 2f).SetEase(Ease.InCubic).OnComplete(() =>
        {
            ActivateFireTwo();
            OnEndMoveToStart?.Invoke();
        }));

        sequenceMoveStart.Play();
    }

    public void ActivateZero()
    {
        imageRocketFire.gameObject.SetActive(false);
        imageRocketStatic.gameObject.SetActive(true);
    }

    public void ActivateFireOne()
    {
        imageRocketFire.sprite = spriteFireOne;

        imageRocketFire.gameObject.SetActive(true);
        imageRocketStatic.gameObject.SetActive(false);
    }

    public void ActivateFireTwo()
    {
        imageRocketFire.sprite = spriteFireTwo;
        
        imageRocketFire.gameObject.SetActive(true);
        imageRocketStatic.gameObject.SetActive(false);
    }

    #region Output

    public event Action OnPauseMoveToBase;
    public event Action OnEndMoveToBase;

    public event Action OnEndMoveToStart;

    #endregion
}
