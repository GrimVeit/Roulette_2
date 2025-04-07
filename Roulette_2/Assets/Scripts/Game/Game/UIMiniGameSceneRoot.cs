using System;
using UnityEngine;

public class UIMiniGameSceneRoot : UIRoot
{
    private ISoundProvider soundProvider;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {

    }

    public void Activate()
    {

    }

    public void Deactivate()
    {
        if (currentPanel != null)
            CloseOtherPanel(currentPanel);
    }

    #region Input


    #endregion
}
