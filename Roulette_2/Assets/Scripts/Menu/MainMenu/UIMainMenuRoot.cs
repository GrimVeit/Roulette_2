using System;
using UnityEngine;

public class UIMainMenuRoot : UIRoot
{
    private ISoundProvider soundProvider;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public void Initialize()
    {

    }

    public void Activate()
    {

    }


    public void Deactivate()
    {
        if(currentPanel != null)
            CloseOtherPanel(currentPanel);
    }

    public void Dispose()
    {

    }



    #region Input


    #endregion

}
