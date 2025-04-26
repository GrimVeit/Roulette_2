using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

public class ChipGameVisualModel
{
    public event Action<int, Chip, int, TypeCell, Vector3> OnAddChip;
    public event Action<int, int> OnReturnChip;
    public event Action<int> OnFallenChips;
    public event Action<int> OnReturnChips;

    private readonly IBetChipEventsProvider _betChipEventsProvider;

    public ChipGameVisualModel(IBetChipEventsProvider betChipEventsProvider)
    {
        _betChipEventsProvider = betChipEventsProvider;
    }

    public void Initialize()
    {
        _betChipEventsProvider.OnAddChip += AddChip;
        _betChipEventsProvider.OnReturnChip += ReturnChip;
        _betChipEventsProvider.OnReturnChips += ReturnChips;
        _betChipEventsProvider.OnFallenChips += FallenChips;
    }

    public void Dispose()
    {
        _betChipEventsProvider.OnAddChip -= AddChip;
        _betChipEventsProvider.OnReturnChip -= ReturnChip;
        _betChipEventsProvider.OnReturnChips -= ReturnChips;
        _betChipEventsProvider.OnFallenChips -= FallenChips;
    }

    private void AddChip(int id, Chip chip, int positionIndex, TypeCell typeCell, Vector3 vectorPosition)
    {
        OnAddChip?.Invoke(id, chip, positionIndex, typeCell, vectorPosition);
    }


    private void ReturnChip(int idChipGroup, int indexPosition)
    {
        OnReturnChip?.Invoke(idChipGroup, indexPosition);
    }

    private void ReturnChips(int indexPositions)
    {
        OnReturnChips?.Invoke(indexPositions);
    }

    private void FallenChips(int indexPositions)
    {
        OnFallenChips?.Invoke(indexPositions);
    }

}
