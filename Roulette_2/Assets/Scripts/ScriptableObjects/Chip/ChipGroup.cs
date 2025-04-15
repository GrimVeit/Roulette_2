using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ChipGroup", menuName = "Game/Chip/New Group")]
public class ChipGroup : ScriptableObject, IChipGroupStore
{
    public List<Chips> Chips = new List<Chips>();

    public Chips GetChipsById(int id)
    {
        return Chips.FirstOrDefault(cc => cc.ID == id);
    }

    public Chip GetChipBiId(int id)
    {
        return GetChipsById(id).Chip;
    }

    public int GetNominalChipByID(int id)
    {
        return GetChipsById(id).Chip.Nominal;
    }

    public int GetCountChipsByID(int id)
    {
        return GetChipsById(id).ChipData.ChipsCount;
    }

    public bool HasElementByID(int id)
    {
        return Chips.Any(cc => cc.ID == id);
    }
}

public interface IChipGroupStore
{
    public int GetNominalChipByID(int id);

    public int GetCountChipsByID(int id);
    public bool HasElementByID(int id);
}
