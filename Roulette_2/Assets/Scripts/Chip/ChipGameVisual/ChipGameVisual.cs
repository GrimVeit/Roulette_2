using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChipGameVisual : MonoBehaviour
{
    [SerializeField] private Image image;

    public void SetData(Chip chip)
    {
        image.sprite = chip.SpriteChip;
    }
}
