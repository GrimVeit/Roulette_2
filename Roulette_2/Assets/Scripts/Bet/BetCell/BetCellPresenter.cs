using System;
using System.Numerics;

public class BetCellPresenter : IBetCellActivatorProvider
{
    private readonly BetCellModel _model;
    private readonly BetCellView _view;

    public BetCellPresenter(BetCellModel model, BetCellView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        _view.Initialize();

        ActivatewEvents();
    }

    public void Dispose()
    {
        _view.Dispose();

        DeactivateEvents();
    }

    private void ActivatewEvents()
    {
        _view.OnAddBet += _model.AddChip;
        _view.OnReturnAllChips += _model.ReturnAllChips;
        _view.OnReturnLastChip += _model.ReturnLastChip;
        _view.OnReturnAllBets += _model.ReturnAllBets;
    }

    private void DeactivateEvents()
    {
        _view.OnAddBet -= _model.AddChip;
        _view.OnReturnAllChips -= _model.ReturnAllChips;
        _view.OnReturnLastChip -= _model.ReturnLastChip;
        _view.OnReturnAllBets -= _model.ReturnAllBets;
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

    #endregion
}

public interface IBetCellActivatorProvider
{
    void Activate();
    void Deactivate();
}
