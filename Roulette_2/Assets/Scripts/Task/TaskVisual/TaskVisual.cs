using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskVisual : MonoBehaviour
{
    public int TaskNumber => taskNumber;

    [SerializeField] private int taskNumber;
    [SerializeField] private Transform transformTask;
    [SerializeField] private Button buttonTask;

    [Header("Window")]
    [SerializeField] private GameObject objectMark;
    [SerializeField] private GameObject objectWindow;

    [Header("TextDescription")]
    [SerializeField] private TextMeshProUGUI textDescription;
    [SerializeField] private Color colorActivateDeactivateDescription;
    [SerializeField] private Color colorCompletedDescription;

    [Header("TextBonus")]
    [SerializeField] private TextMeshProUGUI textBonus;
    [SerializeField] private Color colorActivateDeactivateBonus;
    [SerializeField] private Color colorCompletedBonus;

    [Header("Line")]
    [SerializeField] private Image imageLine;
    [SerializeField] private Sprite spriteActivateDeactivateLine;
    [SerializeField] private Sprite spriteCompletedLine;

    [Header("Coins")]
    [SerializeField] private Image imageCoins;
    [SerializeField] private Sprite spriteActivateDeactivateCoins;
    [SerializeField] private Sprite spriteCompletedCoins;

    private Tween tweenScale;

    public void Initialize()
    {
        buttonTask.onClick.AddListener(() => OnChooseTask?.Invoke(taskNumber));
    }

    public void Dispose()
    {
        buttonTask.onClick.RemoveListener(() => OnChooseTask?.Invoke(taskNumber));
    }

    public void Activate()
    {
        tweenScale?.Kill();

        objectMark.SetActive(true);
        objectWindow.SetActive(true);

        textDescription.color = colorActivateDeactivateDescription;

        textBonus.color = colorActivateDeactivateBonus;

        imageLine.sprite = spriteActivateDeactivateLine;

        imageCoins.sprite = spriteActivateDeactivateCoins;

        tweenScale = transformTask.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.6f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear);
    }

    public void Deactivate()
    {
        tweenScale?.Kill();

        objectMark.SetActive(false);
        objectWindow.SetActive(true);

        textDescription.color = colorActivateDeactivateDescription;

        textBonus.color = colorActivateDeactivateBonus;

        imageLine.sprite = spriteActivateDeactivateLine;

        imageCoins.sprite = spriteActivateDeactivateCoins;

        transformTask.localScale = Vector3.one; 
    }

    public void Complete()
    {
        tweenScale?.Kill();

        objectMark.SetActive(false);
        objectWindow.SetActive(false);

        textDescription.color = colorCompletedDescription;

        textBonus.color = colorCompletedBonus;

        imageLine.sprite = spriteCompletedLine;

        imageCoins.sprite = spriteCompletedCoins;

        tweenScale = transformTask.DOScale(Vector3.one, 0.6f)
            .SetEase(Ease.Linear);
    }

    #region Output

    public event Action<int> OnChooseTask;

    #endregion
}
