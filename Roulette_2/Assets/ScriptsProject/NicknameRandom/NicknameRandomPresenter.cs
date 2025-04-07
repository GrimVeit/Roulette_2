using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NicknameRandomPresenter
{
    private readonly NicknameRandomModel _model;

    public NicknameRandomPresenter(NicknameRandomModel model)
    {
        _model = model;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {

    }

    #region Input

    public void CreateRandomNickname(int minLength, int maxLength)
    {
        _model.CreateRandomNickname(minLength, maxLength);
    }

    #endregion

    #region Output

    public event Action OnSuccess
    {
        add => _model.OnSuccess += value;
        remove => _model.OnSuccess -= value;
    }

    public event Action OnFailure
    {
        add => _model.OnFailure += value;
        remove => _model.OnFailure -= value;
    }

    public event Action<string> OnCreateNickname
    {
        add => _model.OnCreateNickname += value;
        remove => _model.OnCreateNickname -= value;
    }

    #endregion
}
