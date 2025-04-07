using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChipMove : MonoBehaviour
{
    [SerializeField] private protected Rigidbody2D rb;

    public int ID => currentChipData.ID;
    public RectTransform RectTransform;
    public Chip Chip => currentChipData;

    [SerializeField] private protected Transform transformAim;
    [SerializeField] private protected Image imageChip;
    [SerializeField] private protected Image imageChip_2;

    private protected Chip currentChipData;

    private protected IEnumerator coroutineMove;
    private protected bool isMoving;

    public void SetData(Chip chip)
    {
        currentChipData = chip;
        imageChip.sprite = currentChipData.Sprite;
        imageChip_2.sprite = currentChipData.Sprite;
    }

    public void ActivateAim()
    {
        transformAim.gameObject.SetActive(true);
    }

    public void DeactivateAim()
    {
        transformAim.gameObject.SetActive(false);
    }

    public void RotateAim(float angle)
    {
        transformAim.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void ScaleAim(float scale)
    {
        transformAim.localScale = new Vector3(scale, scale, scale);
    }

    public void ZeroForce()
    {
        rb.velocity = Vector3.zero;
    }

    public void AddForce(Vector2 vector)
    {
        rb.AddForce(vector, ForceMode2D.Impulse);

        if(coroutineMove != null)
            Coroutines.Stop(coroutineMove);

        coroutineMove = CoroutineCheckStopped();
        Coroutines.Start(coroutineMove);
    }

    public void Destroy()
    {
        if (coroutineMove != null)
            Coroutines.Stop(coroutineMove);

        Destroy(gameObject);
    }

    private IEnumerator CoroutineCheckStopped()
    {
        isMoving = true;

        yield return new WaitForSeconds(0.1f);

        while(rb.velocity.magnitude > 0.1)
        {
            yield return null;
        }

        isMoving = false;

        OnStoppedCurrent?.Invoke(this);
    }

    private protected void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision != null)
        {
            if (collision.gameObject.TryGetComponent(out ChipMove chip))
            {
                if (transform.GetSiblingIndex() > chip.transform.GetSiblingIndex())
                {
                    //Debug.Log(collision.relativeVelocity.magnitude + "//" + collision.transform.gameObject.name);
                    //Debug.Log(rb.velocity.magnitude + "//" + transform.gameObject.name);

                    var force = Mathf.Max(collision.relativeVelocity.magnitude, rb.velocity.magnitude);

                    OnPunch?.Invoke(transform, chip.transform, collision.GetContact(0).point, force);
                }

            }
        }
    }

    #region Input

    public event Action<ChipMove> OnDead;
    public event Action<ChipMove> OnStoppedCurrent;
    public event Action<ChipMove> OnDeadCurrent;
    public event Action<Transform, Transform, Vector2, float> OnPunch;

    public void DeadTrigger()
    {
        if (isMoving)
        {
            OnDeadCurrent?.Invoke(this);
        }

        OnDead?.Invoke(this);
    }

    #endregion
}
