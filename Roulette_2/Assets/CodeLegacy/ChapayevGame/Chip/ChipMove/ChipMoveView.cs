using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChipMoveView : View
{
    [SerializeField] private List<ChipMove_Player> chipMoves = new List<ChipMove_Player>();
    [SerializeField] private float force = 1000;

    [SerializeField] private float minimalForce;

    private ChipMove currentChipMove;

    private IEnumerator enumeratorMove;

    private bool isDragging;
    private bool isActive = false;

    private Vector2 startDragPosition;

    public void Initialize()
    {

    }

    public void Dispose()
    {
        chipMoves.ForEach(chip =>
        {
            chip.OnDown -= HandleDownChip;
            chip.OnUp -= HandleUpChip;
            chip.OnDeadCurrent -= HandleDestroyedCurrent;
            chip.OnStoppedCurrent -= HandleStoppedCurrent;
        });

        chipMoves.Clear();
    }

    public void AddChip(ChipMove_Player chipMove)
    {
        chipMove.OnDown += HandleDownChip;
        chipMove.OnUp += HandleUpChip;
        chipMove.OnDeadCurrent += HandleDestroyedCurrent;
        chipMove.OnStoppedCurrent += HandleStoppedCurrent;

        chipMoves.Add(chipMove);
    }

    public void RemoveChip(ChipMove_Player chipMove)
    {
        var chip = chipMoves.FirstOrDefault(data => data.ID == chipMove.ID);

        if(chip != null)
        {
            chip.OnDown -= HandleDownChip;
            chip.OnUp -= HandleUpChip;
            chipMove.OnDeadCurrent -= HandleDestroyedCurrent;
            chipMove.OnStoppedCurrent -= HandleStoppedCurrent;

            chipMoves.Remove(chipMove);

            Debug.Log("DESTROY");
        }
    }

    public void ActivateChips()
    {
        chipMoves.ForEach(data => 
        {
            data.ActivateChip();
        });

        isActive = true;
    }

    public void DeactivateChips()
    {
        chipMoves.ForEach(data =>
        {
            data.DeactivateChip();
        });

        isActive = false;
    }

    private void ActivateMove()
    {
        if (enumeratorMove != null)
            Coroutines.Stop(enumeratorMove);

        isDragging = true;
        currentChipMove.ActivateAim();

        enumeratorMove = Move_Coro();
        Coroutines.Start(enumeratorMove);
    }

    private void DeactivateMove()
    {
        if (enumeratorMove != null)
            Coroutines.Stop(enumeratorMove);

        currentChipMove?.DeactivateAim();

        isDragging = false;
    }

    private IEnumerator Move_Coro()
    {
        while (isDragging)
        {
            Vector2 screenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            float distance = Vector2.Distance(currentChipMove.RectTransform.position, screenPosition);

            Vector3 direction = screenPosition - (Vector2)currentChipMove.RectTransform.position;
            direction.Normalize();

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 270;
            currentChipMove.RotateAim(angle);

            AdjustCrocchairScale(distance);

            yield return null;
        }
    }

    private void AdjustCrocchairScale(float distance)
    {
        float minDistance = 0.25f;

        float maxDistance = 2;

        float t = Mathf.InverseLerp(minDistance, maxDistance, distance);

        float newScale = Mathf.Lerp(0.4f, 1, t);

        currentChipMove.ScaleAim(newScale);
    }

    private void HandleDownChip(ChipMove chipMove, PointerEventData pointerEventData)
    {
        if(!isActive) return;

        if(isDragging) return;

        OnStartDrag?.Invoke();

        startDragPosition = Camera.main.ScreenToWorldPoint(pointerEventData.position);

        currentChipMove = chipMove;
        currentChipMove.ZeroForce();

        ActivateMove();
    }

    private void HandleUpChip(ChipMove chipMove, PointerEventData pointerEventData)
    {
        if (!isActive) return;

        DeactivateMove();

        if(currentChipMove != null)
        {
            Vector2 releasePosition = Camera.main.ScreenToWorldPoint(pointerEventData.position);

            Vector2 direction = (startDragPosition - releasePosition).normalized;

            float forceMagnitude = (startDragPosition - releasePosition).magnitude * force;

            //Debug.Log(forceMagnitude);

            OnEndDrag?.Invoke();

            if (forceMagnitude < minimalForce) return;

            currentChipMove.AddForce(direction * forceMagnitude);

            OnDoMotion?.Invoke();

            currentChipMove = null;
        }
    }

    #region Input

    public event Action OnStartDrag;
    public event Action OnEndDrag;

    public event Action OnStoppedCurrentChip;
    public event Action OnDestroyedCurrentChip;

    public event Action OnDoMotion;

    private void HandleStoppedCurrent(ChipMove chipMove)
    {
        OnStoppedCurrentChip?.Invoke();

        Debug.Log("STOPPED CHIP!!!");
    }

    private void HandleDestroyedCurrent(ChipMove chipMove)
    {
        OnDestroyedCurrentChip?.Invoke();

        Debug.Log("STOPPED CHIP!!!");
    }


    #endregion
}
