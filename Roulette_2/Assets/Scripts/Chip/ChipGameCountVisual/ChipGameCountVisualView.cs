using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ChipGameCountVisualView : View
{
    [SerializeField] private List<ChipGameCountVisual> chipCountVisuals = new List<ChipGameCountVisual>();

    public void SetData(int id, int count)
    {
        var visual = GetChipCountVisualById(id);

        if (visual == null)
        {
            Debug.LogError("Not found chip count visual by id - " + id);
            return;
        }

        visual.SetData(count);
    }

    public void Show(int id)
    {
        var visual = GetChipCountVisualById(id);

        if (visual == null)
        {
            Debug.LogError("Not found chip count visual by id - " + id);
            return;
        }

        visual.Show();
    }

    public void Hide(int id)
    {
        var visual = GetChipCountVisualById(id);

        if (visual == null)
        {
            Debug.LogError("Not found chip count visual by id - " + id);
            return;
        }

        visual.Hide();
    }

    private ChipGameCountVisual GetChipCountVisualById(int id)
    {
        return chipCountVisuals.FirstOrDefault(ccv => ccv.ID == id);
    }
}

[System.Serializable]
public class ChipGameCountVisual
{
    public int ID => id;

    [SerializeField] private int id;
    [SerializeField] private TextMeshProUGUI textCount;
    [SerializeField] private GameObject objectChips;

    public void SetData(int count)
    {
        textCount.text = $"x{count}";
    }

    public void Show()
    {
        objectChips.SetActive(true);
    }

    public void Hide()
    {
        objectChips.SetActive(false);
    }
}
