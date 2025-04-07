using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChipBuyVisualize : MonoBehaviour
{
    public int Id => currentChip.ID;

    [SerializeField] private Image imageChipBuyVisualize;
    [SerializeField] private Sprite spriteChipBuyClose;
    [SerializeField] private Color colorClose;
    [SerializeField] private GameObject newObject;

    private Chip currentChip;

    public void SetData(Chip chip)
    {
        currentChip = chip;
    }

    public void OpenNew()
    {
        newObject.gameObject.SetActive(true);
    }

    public void CloseNew()
    {
        newObject.gameObject.SetActive(true);
    }

    public void Open()
    {
        imageChipBuyVisualize.sprite = currentChip.Sprite;
        imageChipBuyVisualize.color = Color.white;
    }

    public void Close()
    {
        imageChipBuyVisualize.sprite = spriteChipBuyClose;
        imageChipBuyVisualize.color = colorClose;
    }
}
