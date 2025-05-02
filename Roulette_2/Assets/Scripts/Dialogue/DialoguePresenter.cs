using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePresenter
{
    private readonly DialogueModel _model;
    private readonly DialogueView _view;

    public DialoguePresenter(DialogueModel model, DialogueView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();
    }

    public void Dispose()
    {
        DeactivateEvents();
    }

    private void ActivateEvents()
    {
        _model.OnActivate += _view.Activate;
        _model.OnDeactivate += _view.Deactivate;
        _model.OnChangeDialogue += _view.SetDialogue;
    }

    private void DeactivateEvents()
    {
        _model.OnActivate -= _view.Activate;
        _model.OnDeactivate -= _view.Deactivate;
        _model.OnChangeDialogue -= _view.SetDialogue;
    }

    #region Input

    public void Activate()
    {
        _model.Activate();
    }

    public void Deactivate()
    {
        _model.Deactivate();
    }

    public void Next()
    {
        _model.Next();
    }

    #endregion
}
