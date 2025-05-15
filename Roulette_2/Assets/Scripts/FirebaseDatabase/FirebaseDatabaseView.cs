using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FirebaseDatabaseView : View
{
    [SerializeField] private List<TopRecord> topRecords = new List<TopRecord>();
    [SerializeField] private SpriteAvatars spriteAvatars;

    public void Initialize()
    {

    }

    public void Dispose()
    {

    }

    public void TestDebugNickname(string nickname)
    {

    }

    public void DisplayUsersRecords(List<UserData> users)
    {
        var top = users.Take(3).ToList();

        for (int i = 0; i < top.Count; i++)
        {
            topRecords[i].SetData(top[i].Nickname, top[i].Record, spriteAvatars.GetSpriteById(top[i].Avatar));
        }
    }
}

[Serializable]
public class TopRecord
{
    public int Id => id;

    [SerializeField] private int id;
    [SerializeField] private TextMeshProUGUI textNickname;
    [SerializeField] private Image imageAvatar;
    [SerializeField] private TextMeshProUGUI textRecord;

    public void SetData(string nickname, int record, Sprite avatar)
    {
        textNickname.text = nickname;
        imageAvatar.sprite = avatar;
        textRecord.text = record.ToString();
    }
}

[Serializable]
public class SpriteAvatars
{
    [SerializeField] private List<SpriteAvatar> avatars = new List<SpriteAvatar>();

    public Sprite GetSpriteById(int id)
    {
        return avatars.FirstOrDefault(x => x.Id == id).GetSprite();
    }
}

[Serializable]
public class SpriteAvatar
{
    public int Id => id;

    [SerializeField] private int id;
    [SerializeField] private Sprite sprite;

    public Sprite GetSprite()
    {
        return sprite;
    }
}
