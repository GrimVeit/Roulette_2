using System;

public class TimerDailyPresenter : ITimerDailyChangeDay, ITimerDailyTickable
{
    private TimerDailyModel _model;

    public TimerDailyPresenter(TimerDailyModel model)
    {
        _model = model;
    }

    public void Initialize()
    {
        _model.Initialize();
    }

    public void Dispose()
    {
        _model.Dispose();
    }

    #region Input

    public event Action<string> OnTimerTick
    {
        add => _model.OnTimerTick += value;
        remove => _model.OnTimerTick -= value;
    }

    public event Action OnChangeDay
    {
        add => _model.OnChangeDay += value;
        remove => _model.OnChangeDay -= value;
    }

    #endregion
}

public interface ITimerDailyTickable
{
    public event Action<string> OnTimerTick;
}

public interface ITimerDailyChangeDay
{
    public event Action OnChangeDay;
}
