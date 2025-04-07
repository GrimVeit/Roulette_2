using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipPunchModel
{
    public event Action<Transform, Transform, Vector2, float> OnAddPunch;

    private Dictionary<string, float> collisions = new Dictionary<string, float>();

    private float cooldown = 0.1f;

    public void AddPunch(Transform transformFirst, Transform transformSecond, Vector2 point, float force)
    {
        string key = GenerateKey(transformFirst, transformSecond);

        if(!collisions.ContainsKey(key) || Time.time - collisions[key] > cooldown)
        {
            OnAddPunch?.Invoke(transformFirst, transformSecond, point, force);

            collisions[key] = Time.time;

            Debug.Log("PUNCH");
        }
    }

    private string GenerateKey(Transform one, Transform two)
    {
        int firstId = one.GetInstanceID();
        int secondId = two.GetInstanceID();

        return firstId < secondId ? $"{firstId}_{secondId}" : $"{secondId}_{firstId}";
    }
}
