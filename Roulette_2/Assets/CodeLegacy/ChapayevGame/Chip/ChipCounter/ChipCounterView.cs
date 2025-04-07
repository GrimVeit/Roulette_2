using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ChipCounterView : View, IIdentify
{
    public string GetID() => ID;

    [SerializeField] private string ID;
    [SerializeField] private ChipCounterVisual chipCounterVisualPrefab;
    [SerializeField] private Transform transformParent;
    [SerializeField] private Sprite spriteBorder;

    [SerializeField] private TextMeshProUGUI textCount;

    private List<ChipCounterVisual> chipCounterVisuals = new List<ChipCounterVisual>();

    public void AddChip(Chip chip)
    {
        var visual = chipCounterVisuals.FirstOrDefault(v => v.ID == chip.ID);

        if(visual != null) return;

        visual = Instantiate(chipCounterVisualPrefab, transformParent);
        visual.SetData(chip, spriteBorder);

        chipCounterVisuals.Add(visual);
    }

    public void RemoveChip(Chip chip)
    {
        var visual = chipCounterVisuals.FirstOrDefault(v => v.ID == chip.ID);

        if (visual == null) return;

        chipCounterVisuals.Remove(visual);

        Destroy(visual.gameObject);
    }

    public void SetCount(int count)
    {
        textCount.text = $"×{count}";
    }
}
