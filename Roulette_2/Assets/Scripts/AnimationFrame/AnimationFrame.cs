using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationFrame : MonoBehaviour, IIdentify
{
    public string GetID() => id;

    [SerializeField] private string id;
    [SerializeField] private List<Sprite> sprites = new List<Sprite>();
    [SerializeField] private Image imageAnimation;
    [SerializeField] private float frameRate;

    private IEnumerator coroutineAnimation;

    public void Activate(int cycles)
    {
        if(coroutineAnimation != null) 
            Coroutines.Stop(coroutineAnimation);

        coroutineAnimation = CoroutineAnimation(cycles);
        Coroutines.Start(coroutineAnimation);
    }

    public void Deactivate()
    {
        if (coroutineAnimation != null) 
            Coroutines.Stop(coroutineAnimation);
    }

    private IEnumerator CoroutineAnimation(int cycles)
    {
        if(sprites.Count == 0) yield break;
        if(cycles == 0) yield break;

        int currentCycle = 0;

        while(cycles == -1 || currentCycle < cycles)
        {
            for (int i = 0; i < sprites.Count; i++)
            {
                imageAnimation.sprite = sprites[i];
                yield return new WaitForSeconds(frameRate);
            }
            currentCycle += 1;
            Debug.Log(currentCycle);
        }
    }
}
