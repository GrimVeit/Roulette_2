using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ChipGameVisual : MonoBehaviour
{
    [SerializeField] private Image image;

    public void SetData(Chip chip)
    {
        image.sprite = chip.SpriteChip;
    }

    public void MoveTo(Vector3 pos)
    {
        transform.DOMove(pos, 0.1f);
    }

    public void TeleportTo(Vector3 pos)
    {
        transform.position = pos;
    }

    public void Return()
    {
        transform.DOLocalMove(Vector3.zero, 0.1f).OnComplete(() => Destroy(gameObject));
    }
}
