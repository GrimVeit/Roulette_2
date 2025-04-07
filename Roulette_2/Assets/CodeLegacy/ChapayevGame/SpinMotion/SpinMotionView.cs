using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class SpinMotionView : View
{
    [SerializeField] private Transform transformBot;
    [SerializeField] private Transform transformPlayer;

    public event Action<float> OnSpin;
    public event Action<bool> OnEndSpin;

    [SerializeField] private Vector3 spinVector;
    [SerializeField] private Transform spinTransform;
    [SerializeField] private Transform centerPoint;
    [SerializeField] private float minSpinSpeed;
    [SerializeField] private float maxSpinSpeed;
    [SerializeField] private float minDuration;
    [SerializeField] private float maxDuration;

    private IEnumerator rotateSpin_Coroutine;

    public void Initialize()
    {

    }

    public void Dispose()
    {

    }


    public void StartSpin()
    {
        if (rotateSpin_Coroutine != null)
            Coroutines.Stop(rotateSpin_Coroutine);

        rotateSpin_Coroutine = RotateSpin_Coroutine();
        Coroutines.Start(rotateSpin_Coroutine);
    }

    private IEnumerator RotateSpin_Coroutine()
    {
        float elapsedTime = 0f;
        float startSpeed = UnityEngine.Random.Range(minSpinSpeed, maxSpinSpeed);
        float duration = UnityEngine.Random.Range(minDuration, maxDuration);
        float endSpeed = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float currentSpeed = Mathf.Lerp(startSpeed, endSpeed, elapsedTime / duration);
            OnSpin?.Invoke(currentSpeed);

            spinTransform.Rotate(currentSpeed * Time.deltaTime * spinVector);

            yield return null;
        }

        yield return RotateToClosest_Coroutine(IsPlayer());

        OnEndSpin?.Invoke(IsPlayer());
    }

    private IEnumerator RotateToClosest_Coroutine(bool isPlayer)
    {
        var target = isPlayer == true ? transformPlayer : transformBot;

        Vector3 directionToTarget = target.position - spinTransform.position;
        float targetAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

        spinTransform.DORotate(new Vector3(0, 0, targetAngle + 90), 0.4f);

        yield return new WaitForSeconds(0.8f);
    }

    private bool IsPlayer()
    {
        return Vector2.Distance(transformPlayer.position, centerPoint.position) < Vector2.Distance(transformBot.position, centerPoint.position);
    }
}