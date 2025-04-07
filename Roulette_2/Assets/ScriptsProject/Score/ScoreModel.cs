using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreModel
{
    public event Action<float> OnChangeCurrentGame;
    public event Action<float> OnChangeGameLast;

    private IMoneyProvider _moneyProvider;

    private float _currentBet;
    private float _currentMultiplier;

    private float _currentScore;

    private readonly string KEY_GAME_RECORD;
    private readonly string KEY_GAME_LAST;

    private float _gameRecord;
    private float _gameLast;

    public ScoreModel(IMoneyProvider provider, string kEY_GAME_RECORD, string kEY_GAME_LAST)
    {
        _moneyProvider = provider;
        KEY_GAME_RECORD = kEY_GAME_RECORD;
        KEY_GAME_LAST = kEY_GAME_LAST;
    }

    public void Initialize()
    {
        _gameRecord = PlayerPrefs.GetFloat(KEY_GAME_RECORD, 0);
        _gameLast = PlayerPrefs.GetFloat(KEY_GAME_LAST, 0);

        OnChangeGameLast?.Invoke(_gameLast);

        Debug.Log(_gameRecord);
    }

    public void Dispose()
    {
        PlayerPrefs.SetFloat(KEY_GAME_RECORD , _gameRecord);
        PlayerPrefs.SetFloat(KEY_GAME_LAST , _gameLast);

        Debug.Log("KKKKKKKKKKKKKKKKKKK");
    }

    public void SetBet(float bet)
    {
        _currentBet = bet;
    }

    public void SetMultiplier(float multiplier)
    {
        _currentMultiplier = multiplier;
    }

    public void Win()
    {
        _currentScore = _currentBet * _currentMultiplier;
        _currentScore = Mathf.Round(_currentScore * 10f) / 10f;
        OnChangeCurrentGame?.Invoke(_currentScore);

        _gameLast = _currentScore;
        OnChangeGameLast?.Invoke(_gameLast);

        if(_gameLast > _gameRecord)
        {
            _gameRecord = _gameLast;
        }

        Debug.Log(_gameRecord);

        _moneyProvider.SendMoney(_currentScore);
    }
}
