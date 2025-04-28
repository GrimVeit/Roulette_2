using TMPro;
using UnityEngine;

public class CooldownView : View, IIdentify
{
    [SerializeField] private string viewID;

    [SerializeField] private GameObject cooldownObject;
    [SerializeField] private TextMeshProUGUI textCountdown;

    public string GetID() => viewID;

    public void ChangeTimer(string time)
    {
        textCountdown.text = time;
    }

    public void ActivateButton()
    {
        cooldownObject.SetActive(false);
    }

    public void DeactivateButton()
    {
        cooldownObject.SetActive(true);
    }
}
