using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnimationFrameView : View
{
    [SerializeField] private List<AnimationFrame> animationFramesPrefabs = new List<AnimationFrame>();
    [SerializeField] private Transform parentAnmations;

    public void ActivateAnimation(string id, Transform target, int cycles)
    {
        var animationFramePrefab = animationFramesPrefabs.FirstOrDefault(af => af.GetID() == id);

        if(animationFramePrefab == null) return;

        var animationFrame = Instantiate(animationFramePrefab, parentAnmations);
        animationFrame.transform.SetPositionAndRotation(target.position, animationFramePrefab.transform.rotation);
        animationFrame.transform.SetParent(target);
        animationFrame.Activate(cycles);
    }
}
