using System;

public class BetSelectModel
{
    public event Action OnIncreaseBet;
    public event Action OnDecreaseBet;

    public event Action<float> OnSetBet;

    private ISoundProvider _soundProvider;

    public BetSelectModel(ISoundProvider soundProvider)
    {
        _soundProvider = soundProvider;
    }

    public void IncreaseBet()
    {
        _soundProvider.PlayOneShot("Click");

        OnIncreaseBet?.Invoke();
    }

    public void DecreaseBet()
    {
        _soundProvider.PlayOneShot("Click");

        OnDecreaseBet?.Invoke();
    }

    public void SetBet(float value)
    {
        OnSetBet?.Invoke(value);
    }
}
