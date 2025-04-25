using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChipGameVisualView : View
{
    [SerializeField] private ChipGameVisual chipGameVisualPrefab;
    [SerializeField] private List<ChipGamePosition> chipGamePositions = new List<ChipGamePosition>();
    [SerializeField] private Transform transformSpawn;

    private List<ChipGameVisual> chips = new List<ChipGameVisual>();

    public void SpawnChip(int id, Chip chip, int posId)
    {
        var position = GetGamePositionById(posId);

        var chipVisual = Instantiate(chipGameVisualPrefab, transformSpawn);
        chipVisual.SetData(chip);
        chipVisual.transform.SetPositionAndRotation(position.GetPosition(), chipGameVisualPrefab.transform.rotation);

        chips.Add(chipVisual);
    }

    private ChipGamePosition GetGamePositionById(int id)
    {
        return chipGamePositions.FirstOrDefault(data => data.ID == id);
    }
}

public class ChipGamePosition
{
    public int ID;

    [SerializeField] private int id;
    [SerializeField] private Transform transform;
    [SerializeField] private int displacement_x;
    [SerializeField] private int displacement_y;

    public Vector3 GetPosition()
    {
        return transform.position + new Vector3(Random.Range(-displacement_x, displacement_x), Random.Range(-displacement_y, displacement_y), 0);
    }
}
