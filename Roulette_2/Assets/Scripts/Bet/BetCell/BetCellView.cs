using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetCellView : View
{
    [SerializeField] private List<Cell> cells = new();

    [SerializeField] private Button buttonReturnAllChips;
    [SerializeField] private Button buttonReturnLastChip;
    [SerializeField] private Button buttonReturnAllBets;

    public void Initialize()
    {
        cells.ForEach(cell => cell.OnAddBet += HandleAddBet);

        buttonReturnAllChips.onClick.AddListener(() => OnReturnAllChips?.Invoke());
        buttonReturnLastChip.onClick.AddListener(() => OnReturnLastChip?.Invoke());
        buttonReturnAllBets.onClick.AddListener(() => OnReturnAllBets?.Invoke());
    }

    public void Dispose()
    {
        cells.ForEach(cell => cell.OnAddBet -= HandleAddBet);

        buttonReturnAllChips.onClick.RemoveListener(() => OnReturnAllChips?.Invoke());
        buttonReturnLastChip.onClick.RemoveListener(() => OnReturnLastChip?.Invoke());
        buttonReturnAllBets.onClick.RemoveListener(() => OnReturnAllBets?.Invoke());
    }

    #region Output

    public event Action OnReturnAllChips;
    public event Action OnReturnLastChip;
    public event Action OnReturnAllBets;

    public event Action<int, Chip, List<int>, TypeCell, bool, System.Numerics.Vector3> OnAddBet;

    private void HandleAddBet(int index, Chip chip, List<int> betCellsIndexes, TypeCell typeCell, bool isNumber, System.Numerics.Vector3 vector)
    {
        Debug.Log("Chip id: " + index);
        Debug.Log("bet cells indexes: " + string.Join(", ", betCellsIndexes));
        OnAddBet?.Invoke(index, chip, betCellsIndexes, typeCell, isNumber, vector);
    }

    #endregion
}
