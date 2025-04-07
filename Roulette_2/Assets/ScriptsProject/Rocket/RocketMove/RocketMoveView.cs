using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RocketMoveView : View
{
    [SerializeField] private Rocket rocket;

    [Header("Play")]
    [SerializeField] private List<Transform> transforms = new List<Transform>();

    [Header("Start")]
    [SerializeField] private Transform transformStartPlay;

    [Header("Win")]
    [SerializeField] private Transform transformWinLeft, transformWinRight;

    [Header("Base")]
    [SerializeField] private Transform transformHide, transformUp, transformStart;

    [Header("Rotate")]
    [SerializeField] private Vector3 vectorRotateToLeft;
    [SerializeField] private Vector3 vectorRotateToRight;

    [Header("Shake")]
    [SerializeField] private float durationShake;
    [SerializeField] private float strengthShake;
    [SerializeField] private int vibratoShake;
    [SerializeField] private float randomnessShake;

    public void Initialize()
    {
        rocket.OnPauseMoveToBase += HandlePauseMoveToBase;
        rocket.OnEndMoveToBase += HandleEndMoveToBase;
        rocket.OnEndMoveToStart += HandleEndMoveToStart;
    }

    public void Dispose()
    {
        rocket.OnPauseMoveToBase -= HandlePauseMoveToBase;
        rocket.OnEndMoveToBase -= HandleEndMoveToBase;
        rocket.OnEndMoveToStart -= HandleEndMoveToStart;
    }

    public void SetCourseNumber(int routeNumber)
    {
        rocket.SetRoute(routeNumber);
    }

    public void MoveLeft(int routeNumber)
    {
        rocket.MoveTo(transforms[routeNumber].position + new Vector3(0, Random.Range(-0.2f, 0.2f)), 0.3f);
        rocket.RotateTo(vectorRotateToLeft, 0.3f);
        rocket.Shake(durationShake, strengthShake, vibratoShake, randomnessShake);
    }

    public void MoveRight(int routeNumber)
    {
        rocket.MoveTo(transforms[routeNumber].position + new Vector3(0, Random.Range(-0.2f, 0.2f)), 0.3f);
        rocket.RotateTo(vectorRotateToRight, 0.5f);
        rocket.Shake(durationShake, strengthShake, vibratoShake, randomnessShake);
    }

    public void MoveToWinLeft()
    {
        rocket.MoveTo(transformWinLeft.position + new Vector3(0, Random.Range(-0.2f, 0.2f)), 0.3f);
        rocket.RotateTo(vectorRotateToLeft, 0.3f);
        rocket.Shake(durationShake, strengthShake, vibratoShake, randomnessShake);

    }

    public void MoveToWinRight()
    {
        rocket.MoveTo(transformWinRight.position + new Vector3(0, Random.Range(-0.2f, 0.2f)), 0.3f);
        rocket.RotateTo(vectorRotateToRight, 0.5f);
        rocket.Shake(durationShake, strengthShake, vibratoShake, randomnessShake);
    }

    public void MoveToBase()
    {
        rocket.MoveToBase(transformHide.position, transformUp.position, transformStart.position);
    }

    public void MoveToStart()
    {
        rocket.MoveToStart(transformStartPlay.position);
    }

    #region Output

    public event Action OnPauseMoveToBase;
    public event Action OnEndMoveToBase;
    public event Action OnEndMoveToStart;

    private void HandlePauseMoveToBase()
    {
        OnPauseMoveToBase?.Invoke();
    }

    private void HandleEndMoveToBase()
    {
        OnEndMoveToBase?.Invoke();
    }

    private void HandleEndMoveToStart()
    {
        OnEndMoveToStart?.Invoke();
    }

    #endregion
}
