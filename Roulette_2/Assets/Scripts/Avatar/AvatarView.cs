using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AvatarView : View
{
    [SerializeField] private List<AvatarVisual> avatarVisuals = new List<AvatarVisual>();
    [SerializeField] private List<Image> imageAvatars = new List<Image>();
    [SerializeField] private SpriteAvatars spriteAvatars;

    public void Initialize()
    {
        avatarVisuals.ForEach(x =>
        {
            x.OnChooseAvatar += HandleChooseAvatar;
            x.Initialize();
        });
    }

    public void Dispose()
    {
        avatarVisuals.ForEach(x =>
        {
            x.OnChooseAvatar -= HandleChooseAvatar;
            x.Dispose();
        });
    }

    #region Input

    public void Select(int id)
    {
        var visual = GetAvatarVisualByid(id);

        if(visual == null)
        {
            Debug.LogWarning("Not found avatar visual by id - " + id);
            return;
        }

        visual.Select();

        var avatar = spriteAvatars.GetSpriteById(id);

        for (int i = 0; i < imageAvatars.Count; i++)
        {
            imageAvatars[i].sprite = avatar;
        }
    }

    public void Deselect(int id)
    {
        var visual = GetAvatarVisualByid(id);

        if (visual == null)
        {
            Debug.LogWarning("Not found avatar visual by id - " + id);
            return;
        }

        visual.Unselect();
    }

    #endregion

    #region Output

    public event Action<int> OnChooseAvatar;

    private void HandleChooseAvatar(int id)
    {
        OnChooseAvatar?.Invoke(id);
    }

    #endregion

    private AvatarVisual GetAvatarVisualByid(int id)
    {
        return avatarVisuals.FirstOrDefault(data => data.Id == id);
    }
}
