using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightPresenter
{
    private readonly HighlightModel _model;
    private readonly HighlightView _view;

    public void Initialize()
    {

    }

    public void Dispose()
    {

    }

    private void ActivateEvents()
    {

    }

    private void DeactivateEvents()
    {

    }

    #region Input

    public void ActivateHighlight(int id)
    {

    }

    public void DeactivateHighlight(int id)
    {

    }

    public void DeactivateAllHighlights()
    {

    }

    #endregion
}

public interface IHighlightProvider
{
    void ActivateHighlight(int id);
    void DeactivateHighlight(int id);
    void DeactivateAllHighlights();
}