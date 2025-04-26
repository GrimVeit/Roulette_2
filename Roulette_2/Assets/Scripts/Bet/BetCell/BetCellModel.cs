using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

public class BetCellModel
{
    private readonly IBetProvider _betProvider;

    public BetCellModel(IBetProvider betProvider)
    {
        _betProvider = betProvider;
    }

    public void AddChip(int id, Chip chip, List<int> positionIndexes, TypeCell typeCell, Vector3 vector)
    {
        _betProvider.AddChip(id, chip, positionIndexes, typeCell, vector);
    }

    public void ReturnLastChip()
    {
        _betProvider.ReturnLastChip();
    }

    public void ReturnAllChips()
    {
        _betProvider.ReturnAllChips();
    }
}
