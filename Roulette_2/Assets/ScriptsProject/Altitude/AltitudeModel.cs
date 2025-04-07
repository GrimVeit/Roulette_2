using System;
using System.Collections;
using UnityEngine;

public class AltitudeModel
{
    public event Action<int> OnChangeAltitude;

    private IEnumerator currentCoroutine;

    private bool isActive = false;
    private float currentSpeed = 0.05f;
    private int currentAltitude;


    public void Clear()
    {
        currentAltitude = 0;
        OnChangeAltitude?.Invoke(currentAltitude);
    }

    public void ActivateAltitude()
    {
        isActive = true;

        if (currentCoroutine != null) 
        {
            Coroutines.Stop(currentCoroutine);

            Clear();
        }

        currentCoroutine = Altitude();
        Coroutines.Start(currentCoroutine);
    }

    public void DeactivateAltitude()
    {
        isActive = false;

        if (currentCoroutine != null) Coroutines.Stop(currentCoroutine);
    }

    private IEnumerator Altitude()
    {
        while (isActive)
        {
            currentAltitude += 1;
            OnChangeAltitude.Invoke(currentAltitude);

            yield return new WaitForSeconds(currentSpeed);
        }
    }
}
