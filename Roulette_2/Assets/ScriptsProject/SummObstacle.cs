using UnityEngine;
using UnityEngine.UI;

public class SummObstacle : Obstacle
{
    [SerializeField] private int summValue;
    [SerializeField] private Image imageObstacle;
    [SerializeField] private Transform transformMove;
    [SerializeField] private string idEffect;
    [SerializeField] private string idSound;

    private void Awake()
    {
        transformMove.position = Vector3.Lerp(transformRandomLeft.position, transformRandomRight.position, Random.Range(0, 1f));
    }

    public override void AddScoreMultiply()
    {
        _scoreMultiply = new SummScoreMultiply(summValue);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Rocket rocket))
        {
            Debug.Log("BONK!");
            Debug.Log($"Obstacle - {_pathRouteData.CourseRoute} // Rocket - {rocket.CourseRoute}");

            if (rocket.CourseRoute == _pathRouteData.CourseRoute)
            {
                ApplyScoreMultiply();
                ApplyObstacleEffect(idEffect, transformMove);
                ApplyRocketMove(this);
                ApplyObstacleSound(idSound);

                imageObstacle.enabled = false;
                colliderObstacle.enabled = false;
            }
        }
    }
}
