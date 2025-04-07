using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrategyBuyVisualize : MonoBehaviour
{
    public int Id => currentStrategy.ID;

    [SerializeField] private Image imageStrategyBuyVisualize;
    [SerializeField] private GameObject newObject;
    [SerializeField] private Color colorClose;

    private Strategy currentStrategy;

    public void SetData(Strategy strategy)
    {
        currentStrategy = strategy;
    }

    public void OpenNew()
    {
        newObject.SetActive(true);
    }

    public void CloseNew()
    {
        newObject.SetActive(false);
    }

    public void Open()
    {
        imageStrategyBuyVisualize.sprite = currentStrategy.Sprite;
        imageStrategyBuyVisualize.color = Color.white;
    }

    public void Close()
    {
        imageStrategyBuyVisualize.sprite = null;
        imageStrategyBuyVisualize.color = colorClose;
    }
}
