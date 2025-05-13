using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NicknameView : View
{
    public event Action OnSubmitNickname;
    public event Action<string> OnChangeNickname;

    [SerializeField] private List<TextMeshProUGUI> textNicknames = new List<TextMeshProUGUI>();

    [SerializeField] private TMP_InputField inputFieldNickname;
    [SerializeField] private TextMeshProUGUI textDescription;
    [SerializeField] private Button button;

    public void Initialize()
    {
        inputFieldNickname.onValueChanged.AddListener(HandlerOnNicknameTextValueChanged);
    }

    public void Dispose()
    {
        inputFieldNickname.onValueChanged.RemoveListener(HandlerOnNicknameTextValueChanged);
    }

    public void ActivateButton()
    {
        button.gameObject.SetActive(true);
    }

    public void DeactivateButton()
    {
        button.gameObject.SetActive(false);
    }

    public void ChangeNickname(string nickname)
    {
        for (int i = 0; i < textNicknames.Count; i++)
        {
            textNicknames[i].text = nickname;
        }
    }

    public void DisplayDescription(string text)
    {
        if(textDescription != null)
           textDescription.text = text;
    }

    #region Input

    private void HandlerOnNicknameTextValueChanged(string value)
    {
        OnChangeNickname?.Invoke(value);
    }

    #endregion
}
