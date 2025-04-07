using System;

public class InternetPresenter
{
    private InternetModel internetModel;
    private InternetView internetView;

    public InternetPresenter(InternetModel internetModel, InternetView internetView)
    {
        this.internetModel = internetModel;
        this.internetView = internetView;
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

    public void CheckConnection()
    {
        internetModel.CheckConnection();
    }

    #endregion

    #region Output

    public event Action OnInternetUnavailable
    {
        add { internetModel.OnConnectionUnvailable += value; }
        remove { internetModel.OnConnectionUnvailable -= value; }
    }

    public event Action OnInternetAvailable
    {
        add { internetModel.OnConnectionAvailable += value;}
        remove { internetModel.OnConnectionAvailable += value; }
    }

    #endregion
}
