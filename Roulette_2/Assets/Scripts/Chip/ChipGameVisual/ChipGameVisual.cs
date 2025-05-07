using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ChipGameVisual : MonoBehaviour
{
    [SerializeField] private Image image;

    private Transform _transformParent;

    public void SetData(Chip chip, Transform transformParent)
    {
        image.sprite = chip.SpriteChip;
        _transformParent = transformParent;
    }

    public void MoveTo(Vector3 pos)
    {
        transform.position = _transformParent.position;
        transform.DOMove(pos, 0.1f);
    }

    public void TeleportTo(Vector3 pos)
    {
        transform.position = pos;
    }

    public void Return()
    {
        transform.SetParent(_transformParent);
        transform.DOLocalMove(Vector3.zero, 0.1f).OnComplete(() => Destroy(gameObject));
    }

    public void Fallen(Vector3 vector)
    {
        transform.DOMove(vector, 0.3f).OnComplete(() => Destroy(gameObject));
    }
}
