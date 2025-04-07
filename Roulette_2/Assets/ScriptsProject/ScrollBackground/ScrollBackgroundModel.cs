using System;
using System.Collections;
using UnityEngine;

public class ScrollBackgroundModel
{
    public event Action<float> OnScroll;

    private IEnumerator coroutineScroll;

    private bool isActive = false;
    private float valueScroll = 0;
    private float duration = 20f;

    public void ActivateScroll()
    {
        isActive = true;

        if(coroutineScroll != null)
            Coroutines.Stop(coroutineScroll);

        coroutineScroll = Scroll();
        Coroutines.Start(coroutineScroll);
    }

    public void DeactivateScroll()
    {
        isActive = false;

        if (coroutineScroll != null)
            Coroutines.Stop(coroutineScroll);
    }

    private IEnumerator Scroll()
    {
        while (isActive)
        {
            valueScroll += Time.deltaTime / duration;

            if(valueScroll >= 1)
            {
                valueScroll = 0;
            }

            OnScroll?.Invoke(valueScroll);

            yield return null;
        }
    }
}
