using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ChipMenuCountVisualView : View
{
    [SerializeField] private List<ChipMenuCountVisual> chipCountVisuals = new List<ChipMenuCountVisual>();

    public void SetData(int id, int count)
    {
        var visual = GetChipCountVisualById(id);

        if(visual == null)
        {
            Debug.LogError("Not found chip count visual by id - " + id);
            return;
        }

        visual.SetData(count);
    }

    private ChipMenuCountVisual GetChipCountVisualById(int id)
    {
        return chipCountVisuals.FirstOrDefault(ccv => ccv.ID == id);
    }
}

[System.Serializable]
public class ChipMenuCountVisual
{
    public int ID => id;

    [SerializeField] private int id;
    [SerializeField] private TextMeshProUGUI textCount;

    public void SetData(int count)
    {
        textCount.text = count.ToString();
    }
}