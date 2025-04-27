using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameSceneRoot_Game : UIRoot
{
    [SerializeField] private HeaderPanel_Game headerPanel;
    [SerializeField] private MainPanel_Game mainPanel;
    [SerializeField] private FooterPanel_Game footerPanel;
    [SerializeField] private RoulettePanel_Game roulettePanel;
    [SerializeField] private ResultPanel_Game resultPanel;

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
        resultPanel.Initialize();
    }

    public void Dispose()
    {
        headerPanel.Dispose();
        mainPanel.Dispose();
        footerPanel.Dispose();
        roulettePanel.Dispose();
        resultPanel.Dispose();
    }

    public void Activate()
    {
        footerPanel.OnClickToSpin += HandleClickToSpin;
        headerPanel.OnClickToMenu += HandleClickToMenu;
    }

    public void Deactivate()
    {
        footerPanel.OnClickToSpin -= HandleClickToSpin;
        headerPanel.OnClickToMenu -= HandleClickToMenu;

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

    public void OpenResultPanel()
    {
        if(resultPanel.IsActive) return;

        OpenPanel(resultPanel);
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

    #region Output

    public event Action OnClickToMenu;

    private void HandleClickToMenu()
    {
        OnClickToMenu?.Invoke();
    }


    public event Action OnClickToSpin;

    private void HandleClickToSpin()
    {
        OnClickToSpin?.Invoke();
    }

    #endregion
}
