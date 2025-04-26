using System;
using System.Numerics;

public class BetCellPresenter : IBettCellProvider
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
        _view.OnAddBet += _model.AddBet;
        _view.OnReturnAllBets += _model.ReturnAllBets;
        _view.OnReturnLastBet += _model.ReturnLastBet;
    }

    private void DeactivateEvents()
    {
        _view.OnAddBet -= _model.AddBet;
        _view.OnReturnAllBets -= _model.ReturnAllBets;
        _view.OnReturnLastBet -= _model.ReturnLastBet;
    }

    #region Output

    public event Action<int, Chip, int, TypeCell, Vector3> OnAddChip
    {
        add => _model.OnAddChip += value;
        remove => _model.OnAddChip -= value;
    }

    public event Action<int, int> OnReturnChip
    {
        add => _model.OnReturnChip += value;
        remove => _model.OnReturnChip -= value;
    }

    public event Action<int> OnFallenChips;
    public event Action<int> OnReturnChips;

    #endregion
}

public interface IBettCellProvider
{
    public event Action<int, Chip, int, TypeCell, Vector3> OnAddChip;
    public event Action<int, int> OnReturnChip;
    public event Action<int> OnFallenChips;
    public event Action<int> OnReturnChips;
}
