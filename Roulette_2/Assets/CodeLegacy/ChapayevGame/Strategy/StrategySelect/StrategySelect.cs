using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrategySelect : MonoBehaviour
{
    public int Id => currentStrategy.ID;

    [SerializeField] private Button buttonSelect;
    [SerializeField] private GameObject newObject;
    [SerializeField] private Image imageStrategyBuyVisualize;
    [SerializeField] private GameObject objectSelect;

    private Strategy currentStrategy;

    public void Initialize()
    {
        buttonSelect.onClick.AddListener(() => OnChooseStrategy?.Invoke(Id));
    }

    public void Dispose()
    {
        buttonSelect.onClick.RemoveListener(() => OnChooseStrategy?.Invoke(Id));
    }

    public void SetData(Strategy strategy)
    {
        currentStrategy = strategy;
        imageStrategyBuyVisualize.sprite = currentStrategy.Sprite;
    }

    public void OpenNew()
    {
        newObject.SetActive(true);
    }

    public void CloseNew()
    {
        newObject?.SetActive(false);
    }

    public void Select()
    {
        objectSelect.SetActive(true);
    }

    public void Deselect()
    {
        objectSelect.SetActive(false);
    }

    #region Input

    public event Action<int> OnChooseStrategy;

    #endregion
}
