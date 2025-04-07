using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChipMove_Player : ChipMove, IPointerDownHandler, IPointerUpHandler
{
    public event Action<ChipMove, PointerEventData> OnDown;
    public event Action<ChipMove, PointerEventData> OnUp;

    private bool isActive;

    public void ActivateChip()
    {
        isActive = true;
    }

    public void DeactivateChip()
    {
        isActive = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(!isActive) return;

        OnDown?.Invoke(this, eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isActive) return;

        OnUp?.Invoke(this, eventData);
    }
}
