using System;
using UnityEngine;
using UnityEngine.UI;

public class FooterPanel_Game : MovePanel
{
    [SerializeField] private Button buttonSpin;

    public override void Initialize()
    {
        base.Initialize();

        buttonSpin.onClick.AddListener(() => OnClickToSpin?.Invoke());
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonSpin.onClick.RemoveListener(() => OnClickToSpin?.Invoke());
    }

    #region Output

    public event Action OnClickToSpin;

    #endregion
}
