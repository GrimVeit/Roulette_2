using System;
using UnityEngine;

public class DailyRewardModel
{
    public event Action OnResetDays;
    public event Action<int> OnChangeCurrentDay;
    public event Action<int> OnLastOpenDay;

    public event Action OnGetDailyReward;

    public event Action OnActivateButtonReward;
    public event Action OnDeactivateButtonReward;

    private int _currentDayReward;
    private DailyRewardValues _rewardValues;
    private readonly string _rewardDayKey;
    private readonly IMoneyProvider _moneyProvider;
    private readonly ISoundProvider _soundProvider;

    private readonly int _maxDay;

    public DailyRewardModel(string rewardDayKey, DailyRewardValues rewardValues, IMoneyProvider moneyProvider, ISoundProvider soundProvider)
    {
        _rewardDayKey = rewardDayKey;
        _rewardValues = rewardValues;
        _moneyProvider = moneyProvider;
        _maxDay = _rewardValues.GetCountDays() - 1;
        _soundProvider = soundProvider;
    }

    public void Initialize()
    {
        _currentDayReward = PlayerPrefs.GetInt(_rewardDayKey, 0);
        OnChangeCurrentDay?.Invoke(_currentDayReward);

        if(_currentDayReward != 0)
        {
            OnLastOpenDay?.Invoke(_currentDayReward - 1);
        }
    }

    public void Dispose()
    {
        PlayerPrefs.SetInt(_rewardDayKey, _currentDayReward);
    }

    public void GetDailyReward()
    {
        OnGetDailyReward?.Invoke();

        _moneyProvider.SendMoney(_rewardValues.GetRewardValueFromDay(_currentDayReward));
        _soundProvider.PlayOneShot("DailyBonus");

        if(_currentDayReward == _maxDay)
        {
            ResetDailyReward();
        }
        else
        {
            _currentDayReward += 1;
        }

        OnChangeCurrentDay?.Invoke(_currentDayReward);
    }

    public void ResetDailyReward()
    {
        _currentDayReward = 0;
        OnChangeCurrentDay?.Invoke(_currentDayReward);
        OnResetDays?.Invoke();
    }

    public void ActivateButtonReward()
    {
        OnActivateButtonReward?.Invoke();

        OnLastOpenDay?.Invoke(_currentDayReward);
    }

    public void DeactivateButtonReward()
    {
        OnDeactivateButtonReward?.Invoke();
    }
}
