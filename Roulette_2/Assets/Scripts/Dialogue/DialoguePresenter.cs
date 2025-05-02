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

    }

    private void DeactivateEvents()
    {

    }

    #region Input

    public void Next()
    {

    }

    #endregion
}
