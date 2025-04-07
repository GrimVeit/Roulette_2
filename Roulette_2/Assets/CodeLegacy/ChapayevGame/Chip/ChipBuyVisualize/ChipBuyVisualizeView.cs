using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChipBuyVisualizeView : View
{
    [SerializeField] private ChipBuyVisualize chipBuyVisualizePrefab;
    [SerializeField] private Transform transformContent;

    private List<ChipBuyVisualize> chipBuyVisualizes = new List<ChipBuyVisualize>();

    public void Initialize()
    {

    }

    public void Dispose()
    {
        chipBuyVisualizes.Clear();
    }

    public void SetOpenChipBuyVisualize(Chip chip)
    {
        var chipBuyVisualize = chipBuyVisualizes.FirstOrDefault(data => data.Id ==  chip.ID);

        if(chipBuyVisualize == null)
        {
            chipBuyVisualize = Instantiate(chipBuyVisualizePrefab, transformContent);
            chipBuyVisualize.SetData(chip);

            chipBuyVisualizes.Add(chipBuyVisualize);
        }

        chipBuyVisualize.Open();
    }

    public void SetNewOpenChipBuyVisualize(Chip chip)
    {
        var chipBuyVisualize = chipBuyVisualizes.FirstOrDefault(data => data.Id == chip.ID);

        if (chipBuyVisualize == null) return;

        chipBuyVisualize.OpenNew();
    }

    public void SetCloseChipBuyVisualize(Chip chip)
    {
        var chipBuyVisualize = chipBuyVisualizes.FirstOrDefault(data => data.Id == chip.ID);

        if (chipBuyVisualize == null)
        {
            chipBuyVisualize = Instantiate(chipBuyVisualizePrefab, transformContent);
            chipBuyVisualize.SetData(chip);

            chipBuyVisualizes.Add(chipBuyVisualize);
        }

        chipBuyVisualize.Close();
    }
}
