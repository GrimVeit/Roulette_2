using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetCellModel
{
    private readonly IChipGroupBet _chipGroupBet;
    private readonly IStoreChip _storeChip;

    public BetCellModel(IChipGroupBet chipGroupBet, IStoreChip storeChip)
    {
        _chipGroupBet = chipGroupBet;
        _storeChip = storeChip;
    }

    public void AddBet(int id, Chip chip, Transform transform, List<int> positionIndexes)
    {
        if (_chipGroupBet.CanHaveCountChips(id, positionIndexes.Count))
        {
            Debug.Log("SUCCESS");

            RemoveChipsFromStore(id, positionIndexes);
        }
        else
        {
            Debug.Log("ERROR");
        }
    }

    private void RemoveChipsFromStore(int id, List<int> positionIndexes)
    {
        for (int i = 0; i < positionIndexes.Count; i++)
        {
            RemoveChipFromStore(id);
        }
    }

    private void RemoveChipFromStore(int id)
    {
        _storeChip.RemoveChip(id);
    }
}
