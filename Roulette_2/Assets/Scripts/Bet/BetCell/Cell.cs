using System;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour, ICell
{
    [SerializeField] private List<int> bettCells = new();

    public void AddChip(int id, Chip chip, Transform transformParent)
    {
        Debug.Log(id);
        OnAddBet?.Invoke(id, chip, transformParent, bettCells);
    }

    #region Output

    public event Action<int, Chip, Transform, List<int>> OnAddBet;

    #endregion
}

public interface ICell
{
    void AddChip(int id, Chip chip, Transform transformParent);
}
