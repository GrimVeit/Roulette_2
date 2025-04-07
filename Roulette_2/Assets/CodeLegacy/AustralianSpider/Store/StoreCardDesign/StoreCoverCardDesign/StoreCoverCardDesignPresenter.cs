using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreCoverCardDesignPresenter
{
    private StoreCoverCardDesignModel model;

    public StoreCoverCardDesignPresenter(StoreCoverCardDesignModel model)
    {
        this.model = model;
    }

    public void Initialize()
    {
        model.Initialize();
    }

    public void Dispose()
    {
        model.Dispose();
    }

    #region Input

    public event Action<CoverCardDesign> OnOpenCoverCardDesign
    {
        add { model.OnOpenCoverCardDesign += value; }
        remove { model.OnOpenCoverCardDesign -= value; }
    }

    public event Action<CoverCardDesign> OnCloseCoverCardDesign
    {
        add { model.OnCloseCoverCardDesign += value; }
        remove { model.OnCloseCoverCardDesign -= value; }
    }



    public event Action<CoverCardDesign> OnDeselectCoverCardDesign
    {
        add { model.OnDeselectCoverCardDesign += value; }
        remove { model.OnDeselectCoverCardDesign -= value; }
    }

    public event Action<CoverCardDesign> OnSelectCoverCardDesign
    {
        add { model.OnSelectCoverCardDesign += value; }
        remove { model.OnSelectCoverCardDesign -= value; }
    }


    public void SelectCoverCardDesign(int id)
    {
        model.SelectCoverCardDesign(id);
    }

    public void BuyCoverCardDesign(int id)
    {
        model.BuyCoverCardDesign(id);
    }

    #endregion
}
