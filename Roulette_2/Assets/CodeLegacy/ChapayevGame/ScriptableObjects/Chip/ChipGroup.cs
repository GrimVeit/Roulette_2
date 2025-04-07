using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ChipGroup", menuName = "Game/Chip/New Chip Group")]
public class ChipGroup : ScriptableObject
{
    public List<Chip> Chips = new();

    public Chip GetChipById(int id)
    {
        return Chips.FirstOrDefault(data => data.ID == id);
    }

    public bool IsAvailableChip()
    {
        return Chips.FirstOrDefault(data => data.ChipData.IsOpen == false) != null;
    }

    public Chip GetRandomCloseChip()
    {
        var strategies = Chips.Where(data => !data.ChipData.IsOpen).ToList();
        return strategies[Random.Range(0, strategies.Count)];
    }
}
