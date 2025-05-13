using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Notification : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textTitle;
    [SerializeField] private TextMeshProUGUI textDescription;
    [SerializeField] private MovePanel movePanel;

    public void Initialize()
    {
        movePanel.OnDeactivatePanel_Data += HandleDestroy;
    }

    public void Dispose()
    {
        movePanel.OnDeactivatePanel_Data -= HandleDestroy;
    }

    public void Activate()
    {
        movePanel.ActivatePanel();
    }

    public void Deactivate()
    {
        movePanel.DeactivatePanel();
    }

    #region Input

    public void SetData(string description, string title)
    {
        textDescription.text = description;
        textTitle.text = title;
    }

    #endregion

    #region Output

    public event Action<Notification> OnDeactivate;

    private void HandleDestroy(MovePanel movePanel)
    {
        Destroy(gameObject);
    }


    #endregion
}
