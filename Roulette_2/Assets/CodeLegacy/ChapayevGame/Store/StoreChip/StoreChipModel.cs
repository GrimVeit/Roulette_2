using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class StoreChipModel
{
    public event Action<Chip> OnOpenChip;
    public event Action<Chip> OnOpenNewChip;
    public event Action<Chip> OnCloseChip;

    public event Action<Chip> OnDeselectChip;
    public event Action<Chip> OnSelectChip;


    private readonly ChipGroup chipGroup;

    private List<ChipData> chipDatas = new List<ChipData>();

    public readonly string FilePath = Path.Combine(Application.persistentDataPath, "Chip.json");

    public StoreChipModel(ChipGroup chipGroup)
    {
        this.chipGroup = chipGroup;

        if (File.Exists(FilePath))
        {
            string loadedJson = File.ReadAllText(FilePath);
            ChipDatas gameTypeDatas = JsonUtility.FromJson<ChipDatas>(loadedJson);

            Debug.Log("Load data");

            this.chipDatas = gameTypeDatas.Datas.ToList();
        }
        else
        {
            Debug.Log("New Data");

            chipDatas = new List<ChipData>();

            for (int i = 0; i < chipGroup.Chips.Count; i++)
            {
                if (i == 0)
                {
                    chipDatas.Add(new ChipData(false, true));
                }
                else
                {
                    chipDatas.Add(new ChipData(false, false));
                }
            }
        }

        for (int i = 0; i < chipGroup.Chips.Count; i++)
        {
            chipGroup.Chips[i].SetData(chipDatas[i]);
        }
    }

    public void Initialize()
    {
        for (int i = 0; i < chipGroup.Chips.Count; i++)
        {
            if (chipGroup.Chips[i].ChipData.IsOpen)
                OnOpenChip?.Invoke(chipGroup.Chips[i]);
            else
                OnCloseChip?.Invoke(chipGroup.Chips[i]);

            if (chipGroup.Chips[i].ChipData.IsSelect)
                OnSelectChip?.Invoke(chipGroup.Chips[i]);
            else
                OnDeselectChip?.Invoke(chipGroup.Chips[i]);
        }
    }

    public void Dispose()
    {
        string json = JsonUtility.ToJson(new ChipDatas(chipDatas.ToArray()));
        File.WriteAllText(FilePath, json);
    }

    public void SelectChip(int number)
    {
        var chip = chipGroup.GetChipById(number);

        if(chip == null)
        {
            Debug.LogError($"Not found chip by id - {number}");
            return;
        }

        if (chip.ChipData.IsSelect)
        {
            chip.ChipData.IsSelect = false;
            OnDeselectChip?.Invoke(chip);
        }
        else
        {
            chip.ChipData.IsSelect = true;
            OnSelectChip?.Invoke(chip);
        }
    }

    public void UnselectAllChips()
    {
        chipGroup.Chips.ForEach(data =>
        {
            if (data.ChipData.IsSelect)
            {
                data.ChipData.IsSelect = false;
                OnDeselectChip?.Invoke(data);
            }
        });
    }

    public void OpenChip(int number)
    {
        var chip = chipGroup.GetChipById(number);

        if (chip == null)
        {
            Debug.LogError($"Not found chip by id - {number}");
            return;
        }

        if (chip.ChipData.IsOpen)
        {
            Debug.LogWarning($"Chip by id - {number} is already open");
        }
        else
        {
            chip.ChipData.IsOpen = true;
            OnOpenChip?.Invoke(chip);
            OnOpenNewChip?.Invoke(chip);
        }
    }

    public bool IsAvailableChip()
    {
        return chipGroup.IsAvailableChip();
    }

    public Chip GetRandomCloseChip()
    {
        return chipGroup.GetRandomCloseChip();
    }
}

[Serializable]
public class ChipDatas
{
    public ChipData[] Datas;

    public ChipDatas(ChipData[] datas)
    {
        Datas = datas;
    }
}

[Serializable]
public class ChipData
{
    public bool IsOpen;
    public bool IsSelect;

    public ChipData(bool isSelect, bool isOpen)
    {
        this.IsSelect = isSelect;
        this.IsOpen = isOpen;
    }
}
