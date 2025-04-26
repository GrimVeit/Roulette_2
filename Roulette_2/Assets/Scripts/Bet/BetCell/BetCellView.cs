using System;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class BetCellView : View
{
    [SerializeField] private List<Cell> cells = new();

    [SerializeField] private Button buttonReturnAllBets;
    [SerializeField] private Button buttonReturnLastBet;

    public void Initialize()
    {
        cells.ForEach(cell => cell.OnAddBet += HandleAddBet);

        buttonReturnAllBets.onClick.AddListener(() => OnReturnAllBets?.Invoke());
        buttonReturnLastBet.onClick.AddListener(() => OnReturnLastBet?.Invoke());
    }

    public void Dispose()
    {
        cells.ForEach(cell => cell.OnAddBet -= HandleAddBet);

        buttonReturnAllBets.onClick.RemoveListener(() => OnReturnAllBets?.Invoke());
        buttonReturnLastBet.onClick.RemoveListener(() => OnReturnLastBet?.Invoke());
    }

    #region Output

    public event Action OnReturnAllBets;
    public event Action OnReturnLastBet;

    public event Action<int, Chip, List<int>, TypeCell, System.Numerics.Vector3> OnAddBet;

    private void HandleAddBet(int index, Chip chip, List<int> betCellsIndexes, TypeCell typeCell, System.Numerics.Vector3 vector)
    {
        Debug.Log("Chip id: " + index);
        Debug.Log("bet cells indexes: " + string.Join(", ", betCellsIndexes));
        OnAddBet?.Invoke(index, chip, betCellsIndexes, typeCell, vector);
    }

    #endregion
}
