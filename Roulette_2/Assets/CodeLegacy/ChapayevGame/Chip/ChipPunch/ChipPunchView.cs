using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipPunchView : View
{
    [SerializeField] private ChipPunch chipPunchPrefab;
    [SerializeField] private Transform transformParent;

    public void AddPunch(Transform first, Transform second, Vector2 point, float force)
    {
        Debug.Log(force);

        Vector2 direction = (first.position - second.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;

        var punch = Instantiate(chipPunchPrefab);
        punch.transform.SetParent(first);
        punch.transform.localScale = Vector3.one;
        punch.transform.position = point;
        punch.transform.rotation = Quaternion.Euler(0, 0, angle);
        Debug.Log(punch.transform.position);
        punch.transform.SetParent(transformParent);

        if(force < 1f)
        {
            force = 1f;
        }
        else if(force > 10)
        {
            force = 10;
        }

        punch.SetData(force/5);

        punch.Initialize();
    }
}
