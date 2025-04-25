using System;
using System.Collections.Generic;
using UnityEngine;

public class BetCellView : View
{
    [SerializeField] private List<Cell> cells = new();

    public void Initialize()
    {
        cells.ForEach(cell => cell.OnAddBet += HandleAddBet);
    }

    public void Dispose()
    {
        cells.ForEach(cell => cell.OnAddBet -= HandleAddBet);
    }

    #region Output

    public event Action<int, Chip, Transform, List<int>> OnAddBet;
    private void HandleAddBet(int index, Chip chip, Transform transformParent, List<int> betCellsIndexes)
    {
        Debug.Log("Chip id: " + index);
        Debug.Log("bet cells indexes: " + string.Join(", ", betCellsIndexes));
        OnAddBet?.Invoke(index, chip, transformParent, betCellsIndexes);
    }

    #endregion
}
