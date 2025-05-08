using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

public class BetCellModel
{
    private readonly IBetProvider _betProvider;

    private bool isActive;

    public BetCellModel(IBetProvider betProvider)
    {
        _betProvider = betProvider;
    }

    public void AddChip(int id, Chip chip, List<int> positionIndexes, TypeCell typeCell, bool isNumber, Vector3 vector)
    {
        _betProvider.AddChip(id, chip, positionIndexes, typeCell, isNumber, vector);
    }

    public void ReturnLastChip()
    {
        if(!isActive) return;

        _betProvider.ReturnLastChip();
    }

    public void ReturnAllChips()
    {
        if (!isActive) return;

        _betProvider.ReturnAllChips();
    }

    public void ReturnAllBets()
    {
        if (!isActive) return;

        _betProvider.ReturnAllBets();
    }



    public void Activate()
    {
        isActive = true;
    }

    public void Deactivate()
    {
        isActive = false;
    }
}
