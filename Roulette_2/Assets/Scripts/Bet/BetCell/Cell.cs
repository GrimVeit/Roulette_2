using System;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour, ICell
{
    [SerializeField] private TypeCell typeCell;
    [SerializeField] private bool isNumber;
    [SerializeField] private List<int> bettCells = new();

    public void AddChip(int id, Chip chip, Vector3 vector)
    {
        Debug.Log(id);
        OnAddBet?.Invoke(id, chip, bettCells, typeCell, isNumber, new System.Numerics.Vector3(vector.x, vector.y, vector.z));
    }

    #region Output

    public event Action<int, Chip, List<int>, TypeCell, bool, System.Numerics.Vector3> OnAddBet;

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
