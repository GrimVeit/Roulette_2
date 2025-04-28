using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewardView : View
{
    public event Action OnClickDailyReward;

    [SerializeField] private Button dailyRewardButton;

    private Tween scaleTween;

    public void Initialize()
    {
        dailyRewardButton.onClick.AddListener(() => OnClickDailyReward?.Invoke());
    }

    public void Dispose()
    {
        if (scaleTween != null)
            scaleTween.Kill();

        dailyRewardButton.onClick.RemoveListener(() => OnClickDailyReward?.Invoke());
    }

    public void ActivateDailyRewardButton()
    {
        dailyRewardButton.gameObject.SetActive(true);
        dailyRewardButton.enabled = true;

        scaleTween = dailyRewardButton.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.6f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear);
    }

    public void DeactivateDailyRewardButton()
    {
        scaleTween?.Kill();

        dailyRewardButton.transform.localScale = Vector3.one;
        dailyRewardButton.gameObject.SetActive(false);
        dailyRewardButton.enabled = false;
    }
}
