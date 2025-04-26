using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

public class ChipGameVisualModel
{
    public event Action<int, Chip, int, TypeCell, Vector3> OnAddChip;
    public event Action<int, int> OnReturnChip;

    private readonly IBettCellProvider _cellProvider;

    public ChipGameVisualModel(IBettCellProvider cellProvider)
    {
        _cellProvider = cellProvider;

        _cellProvider.OnAddChip += AddChip;
        _cellProvider.OnReturnChip += ReturnChip;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {
        _cellProvider.OnAddChip -= AddChip;
        _cellProvider.OnReturnChip -= ReturnChip;
    }

    private void AddChip(int id, Chip chip, int positionIndex, TypeCell typeCell, Vector3 vectorPosition)
    {
        OnAddChip?.Invoke(id, chip, positionIndex, typeCell, vectorPosition);
    }


    private void ReturnChip(int idChipGroup, int indexPosition)
    {
        OnReturnChip?.Invoke(idChipGroup, indexPosition);
    }

}
