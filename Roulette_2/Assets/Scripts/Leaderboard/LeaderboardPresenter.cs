using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardPresenter
{
    private readonly LeaderboardModel _model;
    private readonly LeaderboardView _view;

    public LeaderboardPresenter(LeaderboardModel model, LeaderboardView view)
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
        _view.OnClickToLeft += _model.PreviousPage;
        _view.OnClickToRight += _model.NextPage;
        _view.OnEndShow += _model.Activate;

        _model.OnGetTopPlayers += _view.GetTopPlayers;
        _model.OnGetPagePlayers += _view.GetPagedPlayers;
    }

    private void DeactivateEvents()
    {
        _view.OnClickToLeft -= _model.PreviousPage;
        _view.OnClickToRight -= _model.NextPage;
        _view.OnEndShow -= _model.Activate;

        _model.OnGetTopPlayers -= _view.GetTopPlayers;
        _model.OnGetPagePlayers -= _view.GetPagedPlayers;
    }
}
