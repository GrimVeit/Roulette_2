using System;
using System.Collections.Generic;
using UnityEngine;

public class RouletteValueHistoryModel
{
    private readonly List<IRouletteValueProvider> _rouletteValues;
    private readonly List<Action<RouletteNumber>> _handlers = new();

    public RouletteValueHistoryModel(List<IRouletteValueProvider> rouletteValues)
    {
        _rouletteValues = rouletteValues;
    }

    public void Initialize()
    {
        for (int i = 0; i < _rouletteValues.Count; i++)
        {
            var index = i;
            void Handler(RouletteNumber value) => HandleValue(index, value);
            _rouletteValues[i].OnGetRouletteSlotValue += Handler;
            _handlers.Add(Handler);
        }
    }

    public void Dispose()
    {
        for (int i = 0; i < _rouletteValues.Count; i++)
        {
            _rouletteValues[i].OnGetRouletteSlotValue -= _handlers[i];
        }

        _handlers.Clear();
        _rouletteValues.Clear();
    }

    private void HandleValue(int index, RouletteNumber value)
    {
        Debug.Log(value.Number);

        OnSlotValueChanged?.Invoke(index, value);
    }

    #region Output

    public event Action<int, RouletteNumber> OnSlotValueChanged;

    #endregion
}
