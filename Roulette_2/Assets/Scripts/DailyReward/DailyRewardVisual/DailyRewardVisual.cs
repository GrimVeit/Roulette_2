using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewardVisual : MonoBehaviour
{
    public int Day => day;

    [SerializeField] private int day;
    [SerializeField] private Image imageVisual;
    [SerializeField] private Sprite spriteActivate;
    [SerializeField] private Sprite spriteDeactivate;

    public void Activate()
    {
        imageVisual.sprite = spriteActivate;
    }

    public void Deactivate()
    {
        imageVisual.sprite = spriteDeactivate;
    }
}
