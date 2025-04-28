using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnimationFrameView : View
{
    [SerializeField] private List<AnimationFrame> animationFrames = new List<AnimationFrame>();

    public void ActivateAnimation(string id, int cycles)
    {
        var animationFrame = animationFrames.FirstOrDefault(af => af.GetID() == id);

        Debug.Log(id);

        if(animationFrame == null) return;

        Debug.Log(id);

        animationFrame.Activate(cycles);
    }

    public void DeactivateAnimation(string id)
    {
        var animationFrame = animationFrames.FirstOrDefault(af => af.GetID() == id);

        if (animationFrame == null) return;

        animationFrame.Deactivate();
    }
}
