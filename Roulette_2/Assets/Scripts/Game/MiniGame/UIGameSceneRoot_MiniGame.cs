using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameSceneRoot_MiniGame : UIRoot
{
    [SerializeField] private HeaderPanel_MiniGame headerPanel;
    [SerializeField] private MainPanel_MiniGame mainPanel;
    [SerializeField] private FooterPanel_MiniGame footerPanel;
    [SerializeField] private RoulettePanel_MiniGame roulettePanel;

    private ISoundProvider soundProvider;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public void Initialize()
    {
        headerPanel.Initialize();
        mainPanel.Initialize();
        footerPanel.Initialize();
        roulettePanel.Initialize();
    }

    public void Dispose()
    {
        headerPanel.Dispose();
        mainPanel.Dispose();
        footerPanel.Dispose();
        roulettePanel.Dispose();
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

    public void OpenMainPanel()
    {
        if(mainPanel.IsActive) return;

        OpenPanel(mainPanel);
    }

    public void OpenRoulettePanel()
    {
        if(roulettePanel.IsActive) return;

        OpenPanel(roulettePanel);
    }




    public void OpenHeaderPanel()
    {
        if(headerPanel.IsActive) return;

        OpenOtherPanel(headerPanel);
    }

    public void CloseHeaderPanel()
    {
        if(!headerPanel.IsActive) return;

        CloseOtherPanel(headerPanel);
    }



    public void OpenFooterPanel()
    {
        if(footerPanel.IsActive) return;

        OpenOtherPanel(footerPanel);
    }

    public void CloseFooterPanel()
    {
        if(!footerPanel.IsActive) return;

        CloseOtherPanel(footerPanel);
    }

    #endregion
}
