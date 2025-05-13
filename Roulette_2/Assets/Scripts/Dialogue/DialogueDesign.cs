using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueDesign : MonoBehaviour
{
    public int Id => id;

    [SerializeField] private int id;
    [SerializeField] private MovePanel movePanel;
    [SerializeField] private DialogueAvatars dialogueAvatars;

    [SerializeField] private Image imageAvatar;
    [SerializeField] private TextMeshProUGUI textDescription;

    public void Initialize()
    {
        movePanel.OnDeactivatePanel_Data += DestroyObj;
    }

    public void Dispose()
    {
        movePanel.OnDeactivatePanel_Data -= DestroyObj;
    }

    public void Activate()
    {
        movePanel.ActivatePanel();
    }

    public void Deactivate()
    {
        movePanel.DeactivatePanel();
    }

    public void SetData(int idPerson, string description)
    {
        var avatar = dialogueAvatars.GetSpriteAvatarById(idPerson);

        imageAvatar.sprite = avatar;
        textDescription.text = description;
    }

    private void DestroyObj(MovePanel movePanel)
    {
        Destroy(gameObject);
    }
}

[System.Serializable]
public class DialogueAvatars
{
    [SerializeField] private List<DialogueAvatar> dialogueAvatars = new List<DialogueAvatar>();

    public Sprite GetSpriteAvatarById(int id)
    {
        return dialogueAvatars.FirstOrDefault(data => data.Id == id).Avatar;
    }
}

[System.Serializable]
public class DialogueAvatar
{
    public int Id => id;
    public Sprite Avatar => apriteAvatar;

    [SerializeField] private int id;
    [SerializeField] private Sprite apriteAvatar;
}
