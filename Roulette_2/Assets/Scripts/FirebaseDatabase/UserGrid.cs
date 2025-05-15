using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserGrid : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textNumber;
    [SerializeField] private TextMeshProUGUI textNickname;
    [SerializeField] private Image imageAvatar;
    [SerializeField] private TextMeshProUGUI textRecord;

    public void SetData(int number, string nickname, int record, Sprite avatar)
    {
        textNumber.text = number.ToString();
        textNickname.text = nickname;
        imageAvatar.sprite = avatar;
        textRecord.text = record.ToString();
    }
}
