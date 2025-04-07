using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StrategySelectView : View
{
    [SerializeField] private StrategySelect strategySelectPrefab;
    [SerializeField] private Transform transformContent;
    [SerializeField] private Button buttonSelect;

    private readonly List<StrategySelect> strategySelects = new List<StrategySelect>();

    public void Initialize()
    {

    }

    public void Dispose()
    {
        strategySelects.ForEach(s =>
        {
            s.OnChooseStrategy -= HandleClickToChooseStrategy;
            s.Dispose();
        });

        strategySelects.Clear();
    }

    public void SetOpenStrategy(Strategy strategy)
    {
        var strategySelect = strategySelects.FirstOrDefault(data => data.Id == strategy.ID);

        if (strategySelect == null)
        {
            strategySelect = Instantiate(strategySelectPrefab, transformContent);
            strategySelect.SetData(strategy);
            strategySelect.OnChooseStrategy += HandleClickToChooseStrategy;
            strategySelect.Initialize();

            strategySelects.Add(strategySelect);
        }
    }

    public void SetOpenNewStrategy(Strategy strategy)
    {
        var strategySelect = strategySelects.FirstOrDefault(data => data.Id == strategy.ID);

        if (strategySelect == null) return;

        strategySelect.OpenNew();
    }

    public void SelectStrategy(int id)
    {
        var strategy = strategySelects.FirstOrDefault(s => s.Id == id);

        if (strategy != null)
        {
            strategy.Select();
        }

        buttonSelect.gameObject.SetActive(true);
    }

    public void DeselectStrategy(int id)
    {

        var strategy = strategySelects.FirstOrDefault(s => s.Id == id);

        if (strategy != null)
        {
            strategy.Deselect();
        }

        buttonSelect.gameObject.SetActive(false);
    }

    #region Input

    public event Action<int> OnChooseStrategy;

    private void HandleClickToChooseStrategy(int strategyId)
    {
        OnChooseStrategy?.Invoke(strategyId);
    }

    #endregion
}
