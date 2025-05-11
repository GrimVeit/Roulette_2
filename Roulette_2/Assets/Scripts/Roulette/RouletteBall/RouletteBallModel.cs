using System;
using UnityEngine;

public class RouletteBallModel
{
    public event Action<Vector3> OnBallStopped;
    public event Action OnStartSpin;

    private ISoundProvider _soundProvider;
    private ISound _soundSpin;
    private ISound _soundFall;

    public RouletteBallModel(ISoundProvider soundProvider)
    {
        _soundProvider = soundProvider;
        _soundSpin = _soundProvider.GetSound("BallSpin");
        _soundFall = _soundProvider.GetSound("BallFall");
    }
    public void StartSpin()
    {
        OnStartSpin?.Invoke();

        _soundSpin.Play();
    }

    public void BallStopped(Vector3 vector)
    {
        OnBallStopped?.Invoke(vector);

        _soundSpin.Stop();
        _soundFall.Play();
    }
}
