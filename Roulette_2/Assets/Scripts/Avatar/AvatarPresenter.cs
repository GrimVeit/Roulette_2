using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarPresenter
{
    private readonly AvatarModel _model;
    private readonly AvatarView _view;

    public AvatarPresenter(AvatarModel model, AvatarView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        _model.Initialize();
        _view.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        _model.Dispose();
        _view.Dispose();
    }

    private void ActivateEvents()
    {
        _view.OnChooseAvatar += _model.Select;

        _model.OnSelectAvatar += _view.Select;
        _model.OnDeselectAvatar += _view.Deselect;
    }

    private void DeactivateEvents()
    {
        _view.OnChooseAvatar -= _model.Select;

        _model.OnSelectAvatar -= _view.Select;
        _model.OnDeselectAvatar -= _view.Deselect;
    }

    #region Output

    public event Action<int> OnChooseAvatar
    {
        add => _model.OnSelectAvatar += value;
        remove => _model.OnSelectAvatar -= value;
    }

    #endregion
}
