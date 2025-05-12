using System;
using UnityEngine;

public class RouletteModel
{
    public event Action OnStopSpin;

    public event Action<RouletteNumber> OnGetRouletteSlotValue;
    public event Action<Vector3> OnRollBallToSlot;
    public event Action OnStartSpin;

    private ISoundProvider _soundProvider;

    public RouletteModel(ISoundProvider soundProvider)
    {
        this._soundProvider = soundProvider;
    }

    public void StartSpin()
    {
        _soundProvider.PlayOneShot("RouletteSpin");
        OnStartSpin?.Invoke();
    }

    public void GetRouletteNumber(RouletteNumber rouletteNumber)
    {
        OnGetRouletteSlotValue?.Invoke(rouletteNumber);
    }

    public void RollBallToSlot(Vector3 vector)
    {
        OnRollBallToSlot?.Invoke(vector);
    }

    public void Stop()
    {
        OnStopSpin.Invoke();
    }
}
