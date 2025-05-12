using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RouletteBallView : View, IIdentify
{
    public event Action<Vector3> OnBallStopped;
    public string GetID() => id;

    [SerializeField] private string id;
    [SerializeField] private Transform transformParent;
    [SerializeField] private Transform centerPoint;
    [SerializeField] private Transform ball;
    [SerializeField] private Transform transformStart;
    [SerializeField] private Transform transformEnd;
    private float startRadius;
    private float endRadius;
    [SerializeField] private float minDuration;
    [SerializeField] private float maxDuration;
    [SerializeField] private float startSpeed;
    [SerializeField] private float endSpeed = 0;

    [SerializeField] private Button spinButton;

    private float currentRadius;
    private float currentSpeed;
    private float angle;

    public void Initialize()
    {
        startRadius = Vector3.Distance(transformStart.position, centerPoint.position);
        endRadius = Vector3.Distance(transformEnd.position, centerPoint.position);
    }

    public void Dispose()
    {

    }

    public void StartSpin()
    {
        float value = UnityEngine.Random.Range(minDuration, maxDuration);

        Coroutines.Start(MoveBall());
        DOTween.To(() => currentRadius, x => currentRadius = x, endRadius, value);
        DOTween.To(() => currentSpeed, x => currentSpeed = x, endSpeed, value);
    }

    private IEnumerator MoveBall()
    {
        currentSpeed = startSpeed;
        currentRadius = startRadius;
        angle = 0f;

        ball.transform.SetParent(transformParent);

        while(currentRadius > endRadius)
        {
            angle += currentSpeed * Time.deltaTime;

            float x = centerPoint.position.x + Mathf.Cos(angle) * currentRadius;
            float y = centerPoint.position.y + Mathf.Sin(angle) * currentRadius;

            ball.transform.position = new Vector3(x, y, ball.transform.position.z);

            yield return null;
        }

        OnBallStopped?.Invoke(ball.transform.position);
    }
}
