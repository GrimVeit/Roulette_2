using System;
using System.Collections.Generic;
using UnityEngine;

public class ChipSpawnerView : View, IIdentify
{
    public string GetID() => Id;

    [SerializeField] private string Id; 
    [SerializeField] private ChipMove chipMovePrefab;
    [SerializeField] private Transform transformParent;
    [SerializeField] private List<Transform> transformsSpawns = new List<Transform>();

    public List<ChipMove> chipMoves = new List<ChipMove>();

    public void SetChip(int indexPosition, Chip chip)
    {
        var chipMove = Instantiate(chipMovePrefab, transformParent);
        chipMove.SetData(chip);
        chipMove.OnPunch += HandlePunch;
        chipMove.OnDead += HandleDestroyChip;
        chipMove.transform.SetPositionAndRotation(transformsSpawns[indexPosition].position, chipMovePrefab.transform.rotation);
        chipMoves.Add(chipMove);

        OnSpawnChip?.Invoke(chipMove);
    }

    #region Input

    public event Action<ChipMove> OnSpawnChip;
    public event Action<ChipMove> OnDestroyChip;
    public event Action<Transform, Transform, Vector2, float> OnPunch;

    private void HandleDestroyChip(ChipMove chipMove)
    {
        chipMove.OnPunch -= HandlePunch;
        chipMove.OnDead -= HandleDestroyChip;

        chipMoves.Remove(chipMove);
        OnDestroyChip?.Invoke(chipMove);

        Debug.Log("DESTROY CHIPPPP!");

        chipMove.Destroy();
    }

    private void HandlePunch(Transform first, Transform second, Vector2 point, float force)
    {
        OnPunch?.Invoke(first, second, point, force);
    }

    #endregion
}
