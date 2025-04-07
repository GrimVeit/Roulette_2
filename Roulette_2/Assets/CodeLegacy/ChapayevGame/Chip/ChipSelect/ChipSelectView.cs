using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChipSelectView : View
{
    [SerializeField] private ChipSelect chipSelectPrefab;
    [SerializeField] private Transform transformContent;
    [SerializeField] private Button buttonSelect;
    [SerializeField] private TextMeshProUGUI textChipCount;

    private readonly List<ChipSelect> chipSelects = new List<ChipSelect>();

    public void Initialize()
    {

    }

    public void Dispose()
    {
        chipSelects.ForEach(s =>
        {
            s.OnChooseChip -= HandleClickToChooseChip;
            s.Dispose();
        });

        chipSelects.Clear();
    }

    public void Activate()
    {
        buttonSelect.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        buttonSelect.gameObject.SetActive(false);
    }

    public void SetChipCount(int count)
    {
        switch (count)
        {
            case 1:
                textChipCount.text = "1 chip";
                break;
            case 2:
                textChipCount.text = "2 chips";
                break;
            case 3:
                textChipCount.text = "3 chips";
                break;
        }
    }

    public void SetOpenChip(Chip chip)
    {
        var chipSelect = chipSelects.FirstOrDefault(data => data.Id == chip.ID);

        if (chipSelect == null)
        {
            chipSelect = Instantiate(chipSelectPrefab, transformContent);
            chipSelect.SetData(chip);
            chipSelect.OnChooseChip += HandleClickToChooseChip;
            chipSelect.Initialize();

            chipSelects.Add(chipSelect);
        }
    }

    public void SetOpenNewChip(Chip chip)
    {
        var chipSelect = chipSelects.FirstOrDefault(data => data.Id == chip.ID);

        if (chipSelect == null) return;

        chipSelect.OpenNew();
    }

    public void SelectChip(int id)
    {
        var chip = chipSelects.FirstOrDefault(s => s.Id == id);

        if (chip != null)
        {
            chip.Select();
        }
    }

    public void DeselectChip(int id)
    {
        var chip = chipSelects.FirstOrDefault(s => s.Id == id);

        if(chip != null)
        {
            chip.Deselect();
        }
    }

    #region Input

    public event Action<int> OnChooseChip;

    private void HandleClickToChooseChip(int strategyId)
    {
        OnChooseChip?.Invoke(strategyId);
    }

    #endregion
}
