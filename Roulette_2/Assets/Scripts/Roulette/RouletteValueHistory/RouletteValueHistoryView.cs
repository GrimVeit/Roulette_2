using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RouletteValueHistoryView : View
{
    [Header("Current values")]
    [SerializeField] private List<RouletteValue> rouletteValuesHistory = new List<RouletteValue>();
    [SerializeField] private List<RouletteValueHistorySpriteColor> rouletteValueHistorySpriteColors = new List<RouletteValueHistorySpriteColor>();

    [Header("Statictics")]
    [SerializeField] private RouletteStatistics rouletteStatistics;
    public void SetRouletteNumber(int index, RouletteNumber rouletteNumber)
    {
        rouletteStatistics.SetData(rouletteNumber);

        var spriteColor = GetSpriteColorByColorNumber(rouletteNumber.Color);

        if (spriteColor == null)
        {
            Debug.Log("Not found sprite color with color - " + rouletteNumber.Color);
            return;
        }

        var rouletteValue = GetRouletteValueByIndex(index);

        if(rouletteValue == null)
        {
            Debug.Log("Not found roulette value with id - " + index);
            return;
        }

        rouletteValue.SetData(spriteColor.Sprite, spriteColor.ColorText, rouletteNumber.NumberVisual);
    }

    public void ClearValues()
    {
        rouletteValuesHistory.ForEach(r => r.Clear());
    }

    #region Tools

    private RouletteValueHistorySpriteColor GetSpriteColorByColorNumber(ColorNumber colorNumber)
    {
        return rouletteValueHistorySpriteColors.FirstOrDefault(rvhs => rvhs.ColorNumber == colorNumber);
    }

    private RouletteValue GetRouletteValueByIndex(int id)
    {
        return rouletteValuesHistory.FirstOrDefault(rvh => rvh.ID == id);
    }

    #endregion
}

[System.Serializable]
public class RouletteValueHistorySpriteColor
{
    [SerializeField] private ColorNumber colorNumber;
    [SerializeField] private Sprite sprite;
    [SerializeField] private Color colorText;

    public ColorNumber ColorNumber => colorNumber;
    public Sprite Sprite => sprite;
    public Color ColorText => colorText;
}

[System.Serializable]
public class RouletteValue
{
    public int ID => id;

    [SerializeField] private int id;
    [SerializeField] private GameObject objectValue;
    [SerializeField] private TextMeshProUGUI textNumber;
    [SerializeField] private Image imageNumber;

    public void SetData(Sprite sprite, Color colorText, string number)
    {
        objectValue.SetActive(true);
        textNumber.text = number;
        textNumber.color = colorText;
        imageNumber.sprite = sprite;
    }

    public void Clear()
    {
        objectValue.SetActive(false);
    }
}

[System.Serializable]
public class RouletteStatistics
{
    [SerializeField] private List<RouletteStatisticColumn> columns = new();
    [SerializeField] private RouletteStatistic rouletteStatisticPrefab;

    public void SetData(RouletteNumber rouletteNumber)
    {
        var column = GetColumnByColor(rouletteNumber.Color);

        if(column == null)
        {
            Debug.Log("Not found column with color - " + rouletteNumber.Color);
            return;
        }

        var number = Object.Instantiate(rouletteStatisticPrefab);
        column.AddNumber(number, rouletteNumber.NumberVisual);
    }

    private RouletteStatisticColumn GetColumnByColor(ColorNumber colorNumber)
    {
        return columns.FirstOrDefault(c => c.ColorNumber == colorNumber);
    }
}

[System.Serializable]
public class RouletteStatisticColumn
{
    public ColorNumber ColorNumber => colorNumber;

    [SerializeField] private ColorNumber colorNumber;
    [SerializeField] private Transform content;
    [SerializeField] private Color color;
    [SerializeField] private int maxCountItems;

    public void AddNumber(RouletteStatistic rouletteStatistic, string number)
    {
        rouletteStatistic.transform.SetParent(content);
        rouletteStatistic.transform.localScale = Vector3.one;
        rouletteStatistic.transform.SetSiblingIndex(0);
        rouletteStatistic.SetData(color, number);

        if(content.childCount > maxCountItems)
        {
            Object.Destroy(content.GetChild(content.childCount - 1).gameObject);
        }
    }
}
