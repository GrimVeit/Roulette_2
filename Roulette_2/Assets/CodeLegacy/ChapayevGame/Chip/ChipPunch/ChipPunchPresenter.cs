using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipPunchPresenter
{
    private readonly ChipPunchModel model;
    private readonly ChipPunchView view;

    public ChipPunchPresenter(ChipPunchModel model, ChipPunchView view)
    {
        this.model = model;
        this.view = view;
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
        model.OnAddPunch += view.AddPunch;
    }

    private void DeactivateEvents()
    {
        model.OnAddPunch -= view.AddPunch;
    }

    #region Input

    public void AddPunchChip(Transform transformFirstChip, Transform transformSecondChip, Vector2 point, float force)
    {
        model.AddPunch(transformFirstChip, transformSecondChip, point, force);
    }

    #endregion
}
