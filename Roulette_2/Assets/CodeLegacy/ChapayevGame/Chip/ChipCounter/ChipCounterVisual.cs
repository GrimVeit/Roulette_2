using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChipCounterVisual : MonoBehaviour
{
    [SerializeField] private Image imageBorder;
    [SerializeField] private Image imageChip;
    public int ID => currentChip.ID;

    private Chip currentChip;

    public void SetData(Chip chip, Sprite border)
    {
        currentChip = chip;

        imageBorder.sprite = border;
        imageChip.sprite = currentChip.Sprite;
    }
}
