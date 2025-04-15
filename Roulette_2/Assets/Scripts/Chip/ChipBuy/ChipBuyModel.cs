using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipBuyModel
{
    private readonly IChipGroupStore _chipGroupStore;
    private readonly IStoreChip _storeChip;
    private readonly IMoneyProvider _moneyProvider;

    public ChipBuyModel(IChipGroupStore chipGroupStore, IStoreChip storeChip, IMoneyProvider moneyProvider)
    {
        _chipGroupStore = chipGroupStore;
        _storeChip = storeChip;
        _moneyProvider = moneyProvider;
    }

    public void AddChip(int id)
    {
        if (!_chipGroupStore.HasElementByID(id))
        {
            Debug.LogWarning("Not found chip group by id - " + id);
            return;
        }

        int nominal = _chipGroupStore.GetNominalChipByID(id);

        if (_moneyProvider.CanAfford(nominal))
        {
            Debug.LogWarning(nominal);
            _storeChip.AddChip(id);
            _moneyProvider.SendMoney(-nominal);
        }
    }

    public void RemoveChip(int id)
    {
        if (!_chipGroupStore.HasElementByID(id))
        {
            Debug.LogWarning("Not found chip group by id - " + id);
            return;
        }

        if(_chipGroupStore.GetCountChipsByID(id) == 0) return;

        int nominal = _chipGroupStore.GetNominalChipByID(id);

        _storeChip.RemoveChip(id);
        _moneyProvider.SendMoney(nominal);
    }
}
