using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialDescriptionVisual : MovePanel
{
    public string ID => currentTutorialDescription.GetID();

    [SerializeField] private TextMeshProUGUI textDescription;
    //[SerializeField] private Button buttonClose;

    private IEnumerator coroutineClose;

    private TutorialDescription currentTutorialDescription;

    public override void Initialize()
    {
        base.Initialize();

        //buttonClose.onClick.AddListener(HandleDestroy);
    }

    public override void Dispose()
    {
        base.Dispose();

        //buttonClose.onClick.RemoveListener(HandleDestroy);
    }

    public void SetData(TutorialDescription description)
    {
        currentTutorialDescription = description;
        from = description.GetVectorFrom();
        to = description.GetVectorTo();
        textDescription.text = currentTutorialDescription.Description;
    }

    public void ActivateTimerClose(float seconds)
    {
        DeactivateTimerClose();

        coroutineClose = TimerClose(seconds);
        Coroutines.Start(coroutineClose);
    }

    public void DeactivateTimerClose()
    {
        if (coroutineClose != null)
            Coroutines.Stop(coroutineClose);
    }

    public void OnDestroy()
    {
        DeactivateTimerClose();
    }

    private IEnumerator TimerClose(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        OnTimerCloseEnd?.Invoke(currentTutorialDescription);
    }

    #region Input

    public event Action<TutorialDescription> OnClickToDestroy;
    public event Action<TutorialDescription> OnTimerCloseEnd;

    private void HandleClickToDestroy()
    {
        //OnClickToDestroy?.Invoke();
    }

    #endregion

}
