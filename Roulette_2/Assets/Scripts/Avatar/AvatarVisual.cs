using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class AvatarVisual
{
    public int Id => id;

    [SerializeField] private int id;
    [SerializeField] private Button button;
    [SerializeField] private Image imageSelect;
    [SerializeField] private Sprite spriteSelect;
    [SerializeField] private Sprite spriteDeselect;

    public void Initialize()
    {
        button.onClick.AddListener(() => OnChooseAvatar?.Invoke(id));
    }

    public void Dispose()
    {
        button.onClick.RemoveListener(() => OnChooseAvatar?.Invoke(id));
    }

    public void Unselect()
    {
        imageSelect.sprite = spriteDeselect;
    }

    public void Select()
    {
        imageSelect.sprite = spriteSelect;
    }

    #region Output

    public event Action<int> OnChooseAvatar;

    #endregion
}
