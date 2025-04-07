using System;
using UnityEngine;
using UnityEngine.UI;

public class BetPrepareView : View
{
    [SerializeField] private Button buttonPlay;
    [SerializeField] private Image imagePlay;
    [SerializeField] private Sprite spriteActive;
    [SerializeField] private Sprite spriteDeactive;

    public void Initialize()
    {
        buttonPlay.onClick.AddListener(() => OnPlay?.Invoke());
    }

    public void Dispose()
    {
        buttonPlay.onClick.RemoveListener(() => OnPlay?.Invoke());
    }

    #region Input

    public void Activate()
    {
        buttonPlay.enabled = true;
        imagePlay.sprite = spriteActive;
    }

    public void Deactivate()
    {
        buttonPlay.enabled = false;
        imagePlay.sprite = spriteDeactive;
    }

    #endregion

    #region Output

    public event Action OnPlay;

    #endregion
}
