using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FenceView : View
{
    [SerializeField] private Transform transformSpawnParent;

    [SerializeField] private List<Fence> fences = new List<Fence>();

    private GameObject currentLeftFence;
    private GameObject currentRightFence;

    public void RandomFence()
    {
        var index = Random.Range(0, fences.Count);

        if(currentLeftFence != null) Destroy(currentLeftFence);
        if(currentRightFence != null) Destroy(currentRightFence);

        currentLeftFence = Instantiate(fences[index].LeftFence, transformSpawnParent);
        currentRightFence = Instantiate(fences[index].RightFence, transformSpawnParent);
    }
}

[Serializable]
public class Fence
{
    [SerializeField] private GameObject objectLeftFence;
    [SerializeField] private GameObject objectRightFence;

    public GameObject LeftFence => objectLeftFence;
    public GameObject RightFence => objectRightFence;
}
