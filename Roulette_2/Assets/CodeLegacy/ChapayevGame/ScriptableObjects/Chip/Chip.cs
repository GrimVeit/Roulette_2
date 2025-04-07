using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chip", menuName = "Game/Chip/New Chip")]
public class Chip : ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField] private Sprite sprite;

    private ChipData gameTypeData;

    public int ID => id;
    public Sprite Sprite => sprite;

    public ChipData ChipData => gameTypeData;

    internal void SetData(ChipData gameTypeData)
    {
        this.gameTypeData = gameTypeData;
    }
}
