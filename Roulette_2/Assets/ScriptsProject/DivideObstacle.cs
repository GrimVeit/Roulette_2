using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DivideObstacle : Obstacle
{
    [SerializeField] private int divideValue;
    [SerializeField] private Transform transformMove;
    [SerializeField] private Transform transformSprite;
    [SerializeField] private Transform leftKnock;
    [SerializeField] private Transform rightKnock;
    [SerializeField] private float knockbackDistance = 0.2f;
    [SerializeField] private float knockbackDuration = 0.2f;
    [SerializeField] private string idEffect;
    [SerializeField] private string idSound;
    [SerializeField] private ColliderTrigger trigger;

    private void Awake()
    {
        trigger.OnHit += HitRocket;

        transformMove.position = Vector3.Lerp(transformRandomLeft.position, transformRandomRight.position, Random.Range(0f, 1f));
    }

    private void OnDestroy()
    {
        trigger.OnHit -= HitRocket;
    }

    public override void AddScoreMultiply()
    {
        _scoreMultiply = new DivideScoreMultiply(divideValue);
    }

    public override void KnockLeft()
    {
        transformSprite.DOMove(leftKnock.position, knockbackDuration).SetEase(Ease.OutQuad);
    }

    public override void KnockRight()
    {
        transformSprite.DOMove(rightKnock.position, knockbackDuration).SetEase(Ease.OutQuad);
    }

    private void HitRocket(Rocket rocket)
    {
        Debug.Log("BONK!");
        Debug.Log($"Obstacle - {_pathRouteData.CourseRoute} // Rocket - {rocket.CourseRoute}");

        if (rocket.CourseRoute == _pathRouteData.CourseRoute)
        {
            ApplyScoreMultiply();
            ApplyObstacleEffect(idEffect, transform);
            ApplyRocketMove(this);
            ApplyObstacleSound(idSound);

            colliderObstacle.enabled = false;
        }
    }
}
