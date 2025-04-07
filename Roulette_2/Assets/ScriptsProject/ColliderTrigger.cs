using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ColliderTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Rocket rocket))
        {
            OnHit?.Invoke(rocket);
        }
    }

    #region Input

    public event Action<Rocket> OnHit;

    #endregion
}
