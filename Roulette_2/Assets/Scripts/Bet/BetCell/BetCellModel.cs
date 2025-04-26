using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BetCellModel
{
    private readonly IChipGroupBet _chipGroupBet;
    private readonly IStoreChip _storeChip;

    private List<BetInfo> betInfos = new();

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

            betInfos.Add(new BetInfo(id, positionIndexes[i]));
        }
    }

    public void ReturnLastBet()
    {
        if(betInfos.Count == 0) return;

        var lastBet = betInfos.Last();

        AddChipInStore(lastBet.idChipGroup);

        betInfos.Remove(lastBet);
    }

    public void ReturnAllBets()
    {
        if(betInfos.Count == 0) return;

        for (int i = 0; i < betInfos.Count; i++)
        {
            AddChipInStore(betInfos[i].idChipGroup);
        }
    }

    public void ClearBets()
    {
        betInfos.Clear();
    }

    private void AddChipInStore(int id)
    {
        _storeChip.AddChip(id);
    }

    private void RemoveChipFromStore(int id)
    {
        _storeChip.RemoveChip(id);
    }
}

public record BetInfo
{
    public int idChipGroup;
    public int posIndex;

    public BetInfo(int idChipGroup, int posIndex)
    {
        this.idChipGroup = idChipGroup;
        this.posIndex = posIndex;
    }
}
