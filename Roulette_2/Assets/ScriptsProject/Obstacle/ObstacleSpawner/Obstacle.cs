using System;
using DG.Tweening;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour, IScoreMultiplyProvider, IObstacleEffectProvider, IObstacleRocketControlProvider, IObstacleKnockProvider, IObstacleSoundProvider
{

    [SerializeField] private protected Collider2D colliderObstacle;
    [SerializeField] private protected Transform transformObstacle;
    [SerializeField] private protected Transform transformRandomLeft;
    [SerializeField] private protected Transform transformRandomRight;
    [SerializeField] private ObstacleType type;

    private protected PathRouteData _pathRouteData;
    private protected Tween _tweenMove;
    private protected IScoreMultiply _scoreMultiply;

    public abstract void AddScoreMultiply();
    public virtual void KnockLeft() { }
    public virtual void KnockRight() { }

    public void SetData(PathRouteData data)
    {
        _pathRouteData = data;
    }

    public void MoveToEnd()
    {
        _tweenMove = transformObstacle.DOMove(_pathRouteData.EndPoint.position, 4f).SetEase(Ease.Linear).OnComplete(() => OnEndMove?.Invoke(this));
    }

    public void MoveToClear(Vector3 target, Action OnComplete = null)
    {
        _tweenMove?.Kill();

        _tweenMove = transformObstacle.DOMove(target, 1f).OnComplete(() => OnComplete?.Invoke());
    }

    public void Stop()
    {
        _tweenMove?.Kill();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private protected void ApplyScoreMultiply()
    {
        OnApplyScoreMultiply?.Invoke( _scoreMultiply);
    }

    private protected void ApplyRocketMove(IObstacleKnockProvider provider)
    {
        OnApplyObstacleRocketControl?.Invoke(type, _pathRouteData.PathZone, provider);
    }

    private protected void ApplyObstacleEffect(string id, Transform vector)
    {
        OnApplyObstacleEffect?.Invoke(id, vector);
    }

    private protected void ApplyObstacleSound(string id)
    {
        OnApplyObstacleSound?.Invoke(id);
    }

    #region Input

    public event Action<Obstacle> OnEndMove;
    public event Action<IScoreMultiply> OnApplyScoreMultiply;
    public event Action<string, Transform> OnApplyObstacleEffect;
    public event Action<ObstacleType, PathZone, IObstacleKnockProvider> OnApplyObstacleRocketControl;
    public event Action<string> OnApplyObstacleSound;

    #endregion
}
