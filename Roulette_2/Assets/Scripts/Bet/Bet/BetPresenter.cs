using System;
using System.Collections.Generic;
using System.Numerics;

public class BetPresenter : IBetProvider, IBetProviderCallBack, IBetChipEventsProvider
{
    private readonly BetModel _model;
    private readonly BetView _view;

    public BetPresenter(BetModel model, BetView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        _model.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        _model?.Dispose();
    }

    private void ActivateEvents()
    {
        _model.OnGetWin += _view.SetWin;
    }

    private void DeactivateEvents()
    {
        _model.OnGetWin -= _view.SetWin;
    }

    #region Input

    public void AddChip(int id, Chip chip, List<int> positionIndexes, TypeCell typeCell, bool isNumber, Vector3 vector)
    {
        _model.AddChip(id, chip, positionIndexes, typeCell, isNumber, vector);
    }

    public void ReturnAllChips()
    {
        _model.ReturnAllChips();
    }

    public void ReturnLastChip()
    {
        _model.ReturnLastChip();
    }

    public void ReturnAllBets()
    {
        _model.ReturnAllBets();
    }

    public void SearchWin()
    {
        _model.SearchWin();
    }

    public void ClearTable()
    {
        _model.ClearTable();
    }

    #endregion

    #region Output

    public event Action OnAddBet
    {
        add => _model.OnAddBet += value;
        remove => _model.OnAddBet -= value;
    }

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

    public event Action<int, int> OnFallenChip
    {
        add => _model.OnFallenChip += value;
        remove => _model.OnFallenChip -= value;
    }

    #endregion
}

public interface IBetProviderCallBack
{
    public event Action OnAddBet;
}

public interface IBetProvider
{
    public void AddChip(int id, Chip chip, List<int> positionIndexes, TypeCell typeCell, bool isNumber, Vector3 vector);

    public void ReturnAllChips();
    public void ReturnLastChip();
    public void ReturnAllBets();
}

public interface IBetChipEventsProvider
{
    public event Action<int, Chip, int, TypeCell, Vector3> OnAddChip;
    public event Action<int, int> OnReturnChip;
    public event Action<int, int> OnFallenChip;
}
