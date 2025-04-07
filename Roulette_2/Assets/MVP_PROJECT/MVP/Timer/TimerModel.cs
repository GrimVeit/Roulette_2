using System;
using System.Collections;
using UnityEngine;

public class TimerModel
{
    public event Action OnActivateTimer;
    public event Action OnDeactivateTimer;
    public event Action OnStartTimer;
    public event Action OnStopTimer;
    public event Action<int> OnItterationTimer;

    private IEnumerator spawnEggs_ienumerator;
    private bool isPaused = false;

    private int time = 0;

    public void ActivateTimer()
    {
        OnActivateTimer?.Invoke();

        time = 0;

        if (spawnEggs_ienumerator != null)
            Coroutines.Stop(spawnEggs_ienumerator);

        spawnEggs_ienumerator = timer_Coroutine();
        Coroutines.Start(spawnEggs_ienumerator);

        Debug.Log("����� �������");
    }

    public void DeactivateTimer()
    {
        OnDeactivateTimer?.Invoke();

        if (spawnEggs_ienumerator != null)
            Coroutines.Stop(spawnEggs_ienumerator);

        Debug.Log("����� �������");
    }

    public void PauseTimer()
    {
        isPaused = true;

        Debug.Log("����� �������");
    }

    public void ResumeTimer()
    {
        if (spawnEggs_ienumerator == null)
            ActivateTimer();

        if (!isPaused) return; 

        isPaused = false;

        Debug.Log("����������� �������");
    }

    private IEnumerator timer_Coroutine()
    {
        while (true)
        {
            yield return new WaitUntil(() => !isPaused);

            time += 1;
            OnItterationTimer?.Invoke(time);

            yield return new WaitForSeconds(1);

        }
    }
}
