using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StrategyBuyVisualizeView : View
{
    [SerializeField] private StrategyBuyVisualize strategyBuyVisualizePrefab;
    [SerializeField] private Transform transformContent;

    private readonly List<StrategyBuyVisualize> strategyBuyVisualizes = new List<StrategyBuyVisualize>();

    public void Initialize()
    {

    }

    public void Dispose()
    {
        strategyBuyVisualizes.Clear();
    }

    public void SetOpenStrategyBuyVisualize(Strategy strategy)
    {
        var strategyBuyVisualize = strategyBuyVisualizes.FirstOrDefault(data => data.Id == strategy.ID);

        if (strategyBuyVisualize == null)
        {
            strategyBuyVisualize = Instantiate(strategyBuyVisualizePrefab, transformContent);
            strategyBuyVisualize.SetData(strategy);

            strategyBuyVisualizes.Add(strategyBuyVisualize);
        }

        strategyBuyVisualize.Open();
    }

    public void SetOpenNewStrategyBuyVisualize(Strategy strategy)
    {
        var strategyBuyVisualize = strategyBuyVisualizes.FirstOrDefault(data => data.Id == strategy.ID);

        if (strategyBuyVisualize == null) return;

        strategyBuyVisualize.OpenNew();
    }

    public void SetCloseStrategyBuyVisualize(Strategy strategy)
    {
        var strategyBuyVisualize = strategyBuyVisualizes.FirstOrDefault(data => data.Id == strategy.ID);

        if (strategyBuyVisualize == null)
        {
            strategyBuyVisualize = Instantiate(strategyBuyVisualizePrefab, transformContent);
            strategyBuyVisualize.SetData(strategy);

            strategyBuyVisualizes.Add(strategyBuyVisualize);
        }

        strategyBuyVisualize.Close();
    }
}
