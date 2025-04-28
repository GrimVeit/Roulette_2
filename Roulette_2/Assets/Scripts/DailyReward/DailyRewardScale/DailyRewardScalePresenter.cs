using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyRewardScalePresenter
{
    private readonly DailyRewardScaleModel _model;
    private readonly DailyRewardScaleView _view;

    public DailyRewardScalePresenter(DailyRewardScaleModel model, DailyRewardScaleView view)
    {
        _model = model;
        _view = view;

        ActivateEvents();
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {
        DeactivateEvents();
    }

    private void ActivateEvents()
    {
        _model.OnSetIndex += _view.SetIndex;
    }

    private void DeactivateEvents()
    {
        _model.OnSetIndex -= _view.SetIndex;
    }

    #region Input

    public void SetIndex(int index)
    {
        _model.SetIndex(index);
    }

    #endregion
}
