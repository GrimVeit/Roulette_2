using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

public class BetModel
{
    public event Action OnAddBet;

    public event Action<int, Chip, int, TypeCell, Vector3> OnAddChip;
    public event Action<int, int> OnReturnChip;
    public event Action<int, int> OnFallenChip;

    public event Action<int> OnGetWin;

    private readonly IChipGroupBet _chipGroupBet;
    private readonly IStoreChip _storeChip;

    private readonly Bets _bets;
    private readonly List<RouletteNumber> rouletteNumbers = new();

    private readonly List<BetInfo> _currentBets = new();
    private readonly List<BetInfo> _savedBets = new();

    private readonly List<IRouletteValueProvider> _rouletteValueProviders = new();

    private readonly HashSet<int> winningPosIndexes = new();

    private readonly IMoneyProvider _moneyProvider;

    private readonly IMetric_BetNumber _metricBetNumber;
    private readonly IMetric_WinCount _metric_WinCount;
    private readonly INotificationProvider _notificationProvider;
    private readonly ISoundProvider _soundProvider;
    private readonly IScoreRecordProvider _scoreProvider;

    public BetModel(IChipGroupBet chipGroupBet, IStoreChip storeChip, Bets bets, List<IRouletteValueProvider> rouletteValueProviders, IMoneyProvider moneyProvider, IMetric_BetNumber metricBetNumber, IMetric_WinCount metric_WinCount, INotificationProvider notificationProvider, ISoundProvider soundProvider, IScoreRecordProvider scoreProvider)
    {
        _chipGroupBet = chipGroupBet;
        _storeChip = storeChip;
        _bets = bets;
        _rouletteValueProviders = rouletteValueProviders;
        _moneyProvider = moneyProvider;
        _metricBetNumber = metricBetNumber;
        _metric_WinCount = metric_WinCount;
        _notificationProvider = notificationProvider;
        _soundProvider = soundProvider;
        _scoreProvider = scoreProvider;
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

    public void AddChip(int id, Chip chip, List<int> positionIndexes, TypeCell typeCell, bool isNumber, Vector3 vector)
    {
        if (_chipGroupBet.CanHaveCountChipsByOneId(id, positionIndexes.Count))
        {
            if (isNumber)
            {
                _metricBetNumber.BetNumber();
            }

            for (int i = 0; i < positionIndexes.Count; i++)
            {
                RemoveChipFromStore(id);
                OnAddChip?.Invoke(id, chip, positionIndexes[i], typeCell, vector);

                _currentBets.Add(new BetInfo(id, chip, positionIndexes[i]));
            }

            _soundProvider.PlayOneShot("ChipDrop");
            OnAddBet?.Invoke();
        }
        else
        {
            int need = _chipGroupBet.HowNeedChipsById(id, positionIndexes.Count);
            _notificationProvider.SendMessage($"<color=#ffccd4>{need} chips</color> are missing with a face value of <color=#ffccd4>{chip.Nominal}</color> for a bet", "<color=#ffccd4>Not Enough Chips!</color>", 1);
            _soundProvider.PlayOneShot("Error");
        }
    }
    
    public void ReturnLastChip()
    {
        if (_currentBets.Count == 0)
        {
            _notificationProvider.SendMessage("All chips have already been removed", "<color=#ffccd4>Action Not Needed!</color>", 1);
            _soundProvider.PlayOneShot("Error");
            return;
        }

        var lastBet = _currentBets.Last();

        AddChipInStore(lastBet.IdChipGroup);
        OnReturnChip?.Invoke(lastBet.IdChipGroup, lastBet.PosIndex);

        _currentBets.Remove(lastBet);
        _soundProvider.PlayOneShot("Whoosh");
    }

    public void ReturnAllChips()
    {
        if (_currentBets.Count == 0)
        {
            _notificationProvider.SendMessage("All chips have already been removed", "<color=#ffccd4>Action Not Needed!</color>", 1);
            _soundProvider.PlayOneShot("Error");
            return;
        }

        for (int i = 0; i < _currentBets.Count; i++)
        {
            AddChipInStore(_currentBets[i].IdChipGroup);
            OnReturnChip?.Invoke(_currentBets[i].IdChipGroup, _currentBets[i].PosIndex);
        }

        _soundProvider.PlayOneShot("Whoosh");

        _currentBets.Clear();
    }

    public void ReturnAllBets()
    {
        if(_savedBets.Count == 0)
        {
            _notificationProvider.SendMessage("No previous bets to repeat", "<color=#ffccd4>Action Not Needed!</color>", 1);
            _soundProvider.PlayOneShot("Error");
            return;
        }

        var requiredChips = new Dictionary<int, int>();

        foreach (var chip in _savedBets)
        {
            if (requiredChips.ContainsKey(chip.IdChipGroup))
            {
                requiredChips[chip.IdChipGroup] += 1;
            }
            else
            {
                requiredChips[chip.IdChipGroup] = 1;
            }
        }

        bool potentialReturnBet = _chipGroupBet.CanHaveCountChipsByManyId(requiredChips);

        if (potentialReturnBet)
        {
            Rebet();
        }
        else
        {
            _notificationProvider.SendMessage("Not enough chips to repeat previous bet", "<color=#ffccd4>Not Enough Chips!</color>", 1);
            _soundProvider.PlayOneShot("Error");
        }
    }

    private void Rebet()
    {
        Dictionary<(int, int), int> currentBetCount = new();

        foreach(var bet in _currentBets)
        {
            var key = (bet.IdChipGroup, bet.PosIndex);

            if (currentBetCount.ContainsKey(key))
            {
                currentBetCount[key]++;
            }
            else
            {
                currentBetCount[key] = 1;
            }
        }

        List<BetInfo> betsToRemove = new List<BetInfo>();

        foreach (var savedBet in _savedBets)
        {
            var key = (savedBet.IdChipGroup, savedBet.PosIndex);

            int savedCount = _savedBets.Count(b => b.IdChipGroup == savedBet.IdChipGroup && b.PosIndex == savedBet.PosIndex);

            if (currentBetCount.ContainsKey(key))
            {
                int currentCount = currentBetCount[key];

                if(currentCount > savedCount)
                {
                    int toRemove = currentCount - savedCount;

                    for (int i = 0; i < toRemove; i++)
                    {
                        var betToRemove = _currentBets.FirstOrDefault(b => b.IdChipGroup == savedBet.IdChipGroup && b.PosIndex == savedBet.PosIndex);
                        betsToRemove.Add(betToRemove);
                    }
                }
            }
        }

        foreach (var betToRemove in betsToRemove)
        {
            _currentBets.Remove(betToRemove);
            OnReturnChip?.Invoke(betToRemove.IdChipGroup, betToRemove.PosIndex);
            AddChipInStore(betToRemove.IdChipGroup);
        }


        Dictionary<(int, int), int> savedBetCount = new();

        foreach (var savedBet in _savedBets)
        {
            var key = (savedBet.IdChipGroup, savedBet.PosIndex);

            if (savedBetCount.ContainsKey(key))
            {
                savedBetCount[key]++;
            }
            else
            {
                savedBetCount[key] = 1;
            }
        }

        foreach (var savedBet in savedBetCount)
        {
            int savedCount = savedBet.Value;
            int currentCount = _currentBets.Count(b => b.IdChipGroup == savedBet.Key.Item1 && b.PosIndex == savedBet.Key.Item2);

            if(currentCount < savedCount)
            {
                int toAdd = savedCount - currentCount;

                for (int i = 0; i < toAdd; i++)
                {
                    var betInfo = new BetInfo(savedBet.Key.Item1, _savedBets.FirstOrDefault(b => b.IdChipGroup == savedBet.Key.Item1 && b.PosIndex == savedBet.Key.Item2).Chip, savedBet.Key.Item2);
                    _currentBets.Add(betInfo);
                    OnAddChip?.Invoke(betInfo.IdChipGroup, betInfo.Chip, betInfo.PosIndex, TypeCell.Tracker, new Vector3());
                    RemoveChipFromStore(betInfo.IdChipGroup);
                }
            }
        }
    }

    private void AddChipInStore(int id)
    {
        _storeChip.AddChip(id);
    }

    private void RemoveChipFromStore(int id)
    {
        _storeChip.RemoveChip(id);
    }

    public void SearchWin()
    {
        float totalWin = 0;
        winningPosIndexes.Clear();

        Debug.Log(rouletteNumbers.Count);

        foreach (var number in rouletteNumbers)
        {
            for (int i = 0; i < _currentBets.Count; i++)
            {
                var betInfo = _currentBets[i];

                Bet bet = _bets.GetBetById(betInfo.PosIndex);

                if (bet.Numbers.Contains(number.Number))
                {
                    float win = betInfo.Chip.Nominal * bet.MultiplyPayout;
                    totalWin += win;
                }

                if (winningPosIndexes.Contains(betInfo.PosIndex))
                    continue;

                Debug.Log("Number:" + number.NumberVisual + "//" + bet + "//" + betInfo.PosIndex);

                if (bet.Numbers.Contains(number.Number))
                {
                    winningPosIndexes.Add(betInfo.PosIndex);
                }
            }
        }

        if(totalWin == 0)
        {
            _metric_WinCount.Reset();
        }
        else
        {
            _metric_WinCount.Win();
        }

        float winFloat = Mathf.Round(totalWin * 10f) / 10f;
        int winInt = (int)Math.Round(winFloat, 1);
        _moneyProvider.SendMoney(winInt);
        _scoreProvider.SetScore(winInt);
        OnGetWin?.Invoke(winInt);

        Debug.Log("Winnings:" + string.Join(", ", winningPosIndexes));
    }

    public void ClearTable()
    {
        //for (int i = 0; i < rouletteNumbers.Count; i++)
        //{
        //    foreach (var info in _currentBets)
        //    {
        //        var bet = _bets.bets[info.PosIndex];

        //        var list = _currentBets.Where(data => data.PosIndex == info.PosIndex).ToList();

        //        if (bet.Numbers.Contains(rouletteNumbers[i].Number))
        //        {
        //            for (int j = 0; j < list.Count; j++)
        //            {
        //                AddChipInStore(list[j].IdChipGroup);
        //                OnReturnChip?.Invoke(list[j].IdChipGroup, list[j].PosIndex);
        //            }
        //        }
        //        else
        //        {
        //            for (int j = 0; j < list.Count; j++)
        //            {
        //                OnFallenChip?.Invoke(list[j].IdChipGroup, list[j].PosIndex);
        //            }
        //        }
        //    }
        //}

        foreach (var currentBet in _currentBets)
        {
            var listChips = _currentBets.Where(data => data.PosIndex == currentBet.PosIndex).ToList();

            if (!winningPosIndexes.Contains(currentBet.PosIndex))
            {
                for (int i = 0; i < listChips.Count; i++)
                {
                    OnFallenChip?.Invoke(listChips[i].IdChipGroup, listChips[i].PosIndex);
                }

                Debug.Log("Failure:" + string.Join(", ", listChips));
            }

            //if (winningPosIndexes.Contains(currentBet.PosIndex))
            //{
            //    for (int i = 0; i < listChips.Count; i++)
            //    {
            //        AddChipInStore(listChips[i].IdChipGroup);
            //        OnReturnChip?.Invoke(listChips[i].IdChipGroup, listChips[i].PosIndex);
            //    }

            //    Debug.Log("Winnings:" + string.Join(", ", listChips));
            //}
            //else
            //{
            //    for (int i = 0; i < listChips.Count; i++)
            //    {
            //        OnFallenChip?.Invoke(listChips[i].IdChipGroup, listChips[i].PosIndex);
            //    }

            //    Debug.Log("Failure:" + string.Join(", ", listChips));
            //}
        }

        foreach (var currentBetIndex in winningPosIndexes)
        {
            var listChips = _currentBets.Where(data => data.PosIndex == currentBetIndex).ToList();

            for (int i = 0; i < listChips.Count; i++)
            {
                AddChipInStore(listChips[i].IdChipGroup);
                OnReturnChip?.Invoke(listChips[i].IdChipGroup, listChips[i].PosIndex);
            }

            Debug.Log("Winnings:" + string.Join(", ", listChips));
        }

        _savedBets.Clear();
        _savedBets.AddRange(_currentBets);
        _currentBets.Clear();
        winningPosIndexes.Clear();
        rouletteNumbers.Clear();
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
