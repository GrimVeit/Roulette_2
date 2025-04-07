using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackgroundPresenter
{
    private readonly ScrollBackgroundModel model;
    private readonly ScrollBackgroundView view;

    public ScrollBackgroundPresenter(ScrollBackgroundModel model, ScrollBackgroundView view)
    {
        this.model = model;
        this.view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        view.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        view.Dispose();
    }

    private void ActivateEvents()
    {
        model.OnScroll += view.SetScrollValue;
    }

    private void DeactivateEvents()
    {
        model.OnScroll -= view.SetScrollValue;
    }

    #region Input

    public void ActivateScroll()
    {
        model.ActivateScroll();
    }

    public void DeactivateScroll()
    {
        model.DeactivateScroll();
    }

    #endregion
}
