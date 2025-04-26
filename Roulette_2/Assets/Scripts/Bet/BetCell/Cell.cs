using System;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour, ICell
{
    [SerializeField] private TypeCell typeCell;
    [SerializeField] private List<int> bettCells = new();

    public void AddChip(int id, Chip chip, Vector3 vector)
    {
        Debug.Log(id);
        OnAddBet?.Invoke(id, chip, bettCells, typeCell, new System.Numerics.Vector3(vector.x, vector.y, vector.z));
    }

    #region Output

    public event Action<int, Chip, List<int>, TypeCell, System.Numerics.Vector3> OnAddBet;

    #endregion
}

public enum TypeCell
{
    Main, Tracker
}

public interface ICell
{
    void AddChip(int id, Chip chip, Vector3 vector);
}
