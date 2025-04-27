using System;
using System.Collections.Generic;
using System.Numerics;

public class BetPresenter : IBetProvider, IBetChipEventsProvider
{
    private readonly BetModel _model;

    public BetPresenter(BetModel model)
    {
        _model = model;
    }

    public void Initialize()
    {
        _model.Initialize();
    }

    public void Dispose()
    {
        _model?.Dispose();
    }

    #region Input

    public void AddChip(int id, Chip chip, List<int> positionIndexes, TypeCell typeCell, Vector3 vector)
    {
        _model.AddChip(id, chip, positionIndexes, typeCell, vector);
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

public interface IBetProvider
{
    public void AddChip(int id, Chip chip, List<int> positionIndexes, TypeCell typeCell, Vector3 vector);

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
