using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class ChipGameVisualView : View
{
    [SerializeField] private ChipGameVisual chipGameVisualPrefab;
    [SerializeField] private List<ChipGamePosition> chipGamePositions = new List<ChipGamePosition>();
    [SerializeField] private List<TransformById> transformByIds = new List<TransformById>();
    [SerializeField] private Transform transformSpawnChip;
    [SerializeField] private Transform transformFallen;

    public void AddChip(int id, Chip chip, int posId, TypeCell typeCell, System.Numerics.Vector3 vector)
    {
        if(typeCell == TypeCell.Main)
        {
            AddChipInMainType(id, chip, posId, new Vector3(vector.X, vector.Y, vector.Z));
        }
        else
        {
            AddChipInTrackerType(id, chip, posId);
        }
    }

    public void ReturnChip(int id, int posIndex)
    {
        var chipGamePosition = GetGamePositionById(posIndex);

        chipGamePosition.ReturnChip(id);
    }

    public void FallenChip(int id, int posIndex)
    {
        var chipGamePosition = GetGamePositionById(posIndex);

        chipGamePosition.FallenChip(id, transformFallen.position);
    }

    private void AddChipInMainType(int id, Chip chip, int posId, Vector3 vector)
    {
        var chipGamePosition = GetGamePositionById(posId);
        var transformSpawn = GetTransformById(id);

        var chipVisual = Instantiate(chipGameVisualPrefab, transformSpawnChip);
        chipVisual.SetData(chip, transformSpawn.TransformSpawn);
        chipVisual.TeleportTo(vector);

        chipGamePosition.AddChip(chipVisual, id);
    }

    private void AddChipInTrackerType(int id, Chip chip, int posId)
    {
        var chipGamePosition = GetGamePositionById(posId);
        var transformSpawn = GetTransformById(id);

        var chipVisual = Instantiate(chipGameVisualPrefab, transformSpawnChip);
        chipVisual.SetData(chip, transformSpawn.TransformSpawn);
        chipVisual.MoveTo(chipGamePosition.GetPosition());

        chipGamePosition.AddChip(chipVisual, id);
    }

    private ChipGamePosition GetGamePositionById(int id)
    {
        return chipGamePositions.FirstOrDefault(data => data.ID == id);
    }

    private TransformById GetTransformById(int id)
    {
        return transformByIds.FirstOrDefault(data => data.ID == id);
    }
}

[System.Serializable]
public class TransformById
{
    public int ID => id;
    public Transform TransformSpawn => transformSpawn;

    [SerializeField] private int id;
    [SerializeField] private Transform transformSpawn;
}

[System.Serializable]
public class ChipGamePosition
{
    public int ID => id;

    [SerializeField] private int id;
    [SerializeField] private Transform transform;
    [SerializeField] private float displacement_x;
    [SerializeField] private float displacement_y;

    [SerializeField] private Dictionary<ChipGameVisual, int> chipGameVisuals = new Dictionary<ChipGameVisual, int>();

    public Vector3 GetPosition()
    {
        return transform.position + new Vector3(Random.Range(-displacement_x, displacement_x), Random.Range(-displacement_y, displacement_y), 0);
    }

    public void AddChip(ChipGameVisual chip, int idGroup)
    {
        chipGameVisuals.Add(chip, idGroup);
    }

    public void ReturnChip(int id)
    {
        if (chipGameVisuals.Count == 0) return;

        var chip = GetChipById(id);

        if(chip == null)
        {
            Debug.LogError("Not found chip by id group - " + id);
            return;
        }

        chipGameVisuals.Remove(chip);
        chip.Return();
    }

    public void FallenChip(int id, Vector3 vector)
    {
        if (chipGameVisuals.Count == 0) return;

        var chip = GetChipById(id);

        if (chip == null)
        {
            Debug.LogError("Not found chip by id group - " + id);
            return;
        }

        chipGameVisuals.Remove(chip);
        chip.Fallen(vector);
    }

    private ChipGameVisual GetChipById(int id)
    {
        return chipGameVisuals.FirstOrDefault(data => data.Value == id).Key;
    }
}
