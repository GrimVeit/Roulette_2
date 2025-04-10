using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameSceneRoot_MiniGame : UIRoot
{
    [SerializeField] private HeaderPanel_MiniGame headerPanel;
    [SerializeField] private MainPanel_MiniGame mainPanel;
    [SerializeField] private FooterPanel_MiniGame footerPanel;
    [SerializeField] private ChooseChipPanel_MiniGame chooseChipPanel;
    //[SerializeField] private 

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
