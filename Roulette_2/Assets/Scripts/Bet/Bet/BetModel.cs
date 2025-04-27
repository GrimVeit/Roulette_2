using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

public class BetModel
{
    public event Action<int, Chip, int, TypeCell, Vector3> OnAddChip;
    public event Action<int, int> OnReturnChip;
    public event Action<int> OnFallenChips;
    public event Action<int> OnReturnChips;

    private readonly IChipGroupBet _chipGroupBet;
    private readonly IStoreChip _storeChip;

    private readonly Bets _bets;
    private readonly List<RouletteNumber> rouletteNumbers = new();

    private readonly List<BetInfo> betInfos = new();

    private List<IRouletteValueProvider> _rouletteValueProviders = new();

    public BetModel(IChipGroupBet chipGroupBet, IStoreChip storeChip, Bets bets, List<IRouletteValueProvider> rouletteValueProviders)
    {
        _chipGroupBet = chipGroupBet;
        _storeChip = storeChip;
        _bets = bets;
        _rouletteValueProviders = rouletteValueProviders;
    }

    public void Initialize()
    {
        _rouletteValueProviders.ForEach(rvp => rvp.OnGetRouletteSlotValue += SetRouletteNumber);
    }

    public void Dispose()
    {
        _rouletteValueProviders.ForEach(rvp => rvp.OnGetRouletteSlotValue -= SetRouletteNumber);
    }

    public void SetRouletteNumber(RouletteNumber rouletteNumber)
    {
        rouletteNumbers.Add(rouletteNumber);
    }

    public void AddChip(int id, Chip chip, List<int> positionIndexes, TypeCell typeCell, Vector3 vector)
    {
        if (_chipGroupBet.CanHaveCountChips(id, positionIndexes.Count))
        {
            SubmitBet(id, chip, positionIndexes, typeCell, vector);
        }
    }

    private void SubmitBet(int id, Chip chip, List<int> positionIndexes, TypeCell typeCell, Vector3 vector)
    {
        for (int i = 0; i < positionIndexes.Count; i++)
        {
            RemoveChipFromStore(id);
            OnAddChip?.Invoke(id, chip, positionIndexes[i], typeCell, vector);

            betInfos.Add(new BetInfo(id, chip, positionIndexes[i]));
        }
    }

    public void SearchWin()
    {
        var totalWin = 0;

        for (int i = 0; i < rouletteNumbers.Count; i++)
        {
            foreach (var info in betInfos)
            {

                var bet = _bets.bets[info.PosIndex];

                if (bet.Numbers.Contains(rouletteNumbers[i].Number))
                {
                    totalWin += info.Chip.Nominal * bet.MultiplyPayout;
                }
            }
        }

        Debug.Log(totalWin);
    }

    public void ClearTable()
    {
        for (int i = 0; i < rouletteNumbers.Count; i++)
        {
            foreach (var info in betInfos)
            {
                var bet = _bets.bets[info.PosIndex];

                if (bet.Numbers.Contains(rouletteNumbers[i].Number))
                {
                    OnReturnChips?.Invoke(info.PosIndex);
                }
                else
                {
                    OnFallenChips?.Invoke(info.PosIndex);
                }
            }
        }
    }
    
    public void ReturnLastChip()
    {
        if (betInfos.Count == 0) return;

        var lastBet = betInfos.Last();

        AddChipInStore(lastBet.IdChipGroup);
        OnReturnChip?.Invoke(lastBet.IdChipGroup, lastBet.PosIndex);

        betInfos.Remove(lastBet);
    }

    public void ReturnAllChips()
    {
        if (betInfos.Count == 0) return;

        for (int i = 0; i < betInfos.Count; i++)
        {
            AddChipInStore(betInfos[i].IdChipGroup);
            OnReturnChip?.Invoke(betInfos[i].IdChipGroup, betInfos[i].PosIndex);
        }

        betInfos.Clear();
    }

    public void ClearBets()
    {

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
    public int IdChipGroup;
    public Chip Chip;
    public int PosIndex;

    public BetInfo(int idChipGroup, Chip chip, int posIndex)
    {
        IdChipGroup = idChipGroup;
        PosIndex = posIndex;
        Chip = chip;
    }
}
