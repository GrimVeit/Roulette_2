using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FencePresenter
{
    private readonly FenceModel model;
    private readonly FenceView view;

    public FencePresenter(FenceModel model, FenceView view)
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
        model.OnRandomFence += view.RandomFence;
    }

    private void DeactivateEvents()
    {
        model.OnRandomFence -= view.RandomFence;
    }

    #region Inout

    public void RandomFence()
    {
        model.RandomFence();
    }

    #endregion
}
