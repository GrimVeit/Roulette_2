using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarModel
{
    public event Action<int> OnSelectAvatar;
    public event Action<int> OnDeselectAvatar;

    private int _currentIndexAvatar = 0;

    public void Initialize()
    {
        OnSelectAvatar?.Invoke(_currentIndexAvatar);
    }

    public void Dispose()
    {

    }

    public void Select(int id)
    {
        if(_currentIndexAvatar == id) return;

        OnDeselectAvatar?.Invoke(_currentIndexAvatar);

        _currentIndexAvatar = id;
        OnSelectAvatar?.Invoke(id);
    }
}
