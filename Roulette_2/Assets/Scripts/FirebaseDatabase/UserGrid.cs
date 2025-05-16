using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserGrid : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textNumber;
    [SerializeField] private TextMeshProUGUI textNickname;
    [SerializeField] private Image imageAvatar;
    [SerializeField] private TextMeshProUGUI textRecord;
  
    [Header("Move")]
    [SerializeField] private Transform transformMove;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Vector3 vectorLeft;
    [SerializeField] private Vector3 vectorRight;


    private Tween tweenMove;
    private Tween tweenFade;

    public void SetData(int number, string nickname, int record, Sprite avatar)
    {
        textNumber.text = number.ToString();
        textNickname.text = nickname;
        imageAvatar.sprite = avatar;
        textRecord.text = record.ToString();
    }

    public void ActivateRight()
    {
        transformMove.localPosition = vectorLeft;

        tweenMove?.Kill();
        tweenFade?.Kill();

        tweenMove = transformMove.DOLocalMove(Vector3.zero, 0.2f);
        tweenFade = canvasGroup.DOFade(1, 0.2f);
    }

    public void ActivateLeft()
    {
        transformMove.localPosition = vectorRight;

        tweenMove?.Kill();
        tweenFade?.Kill();

        tweenMove = transformMove.DOLocalMove(Vector3.zero, 0.2f);
        tweenFade = canvasGroup.DOFade(1, 0.2f);
    }

    public void DeactivateRight()
    {
        tweenMove?.Kill();
        tweenFade?.Kill();

        tweenMove = transformMove.DOLocalMove(vectorRight, 0.2f);
        tweenFade = canvasGroup.DOFade(0, 0.2f).OnComplete(() => Destroy(gameObject));
    }

    public void DeactivateLeft()
    {
        tweenMove?.Kill();
        tweenFade?.Kill();

        tweenMove = transformMove.DOLocalMove(vectorLeft, 0.2f);
        tweenFade = canvasGroup.DOFade(0, 0.2f).OnComplete(() => Destroy(gameObject));
    }

    private void OnDestroy()
    {
        tweenMove?.Kill();
        tweenFade?.Kill();
    }
}
