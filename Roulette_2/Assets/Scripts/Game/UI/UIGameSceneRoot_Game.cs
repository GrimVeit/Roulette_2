using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameSceneRoot_Game : UIRoot
{
    [SerializeField] private HeaderPanel_Game balancePanel;
    [SerializeField] private MenuPanel_Game menuPanel;
    [SerializeField] private MainPanel_Game mainPanel;
    [SerializeField] private FooterPanel_Game footerPanel;
    [SerializeField] private RoulettePanel_Game roulettePanel;
    [SerializeField] private ResultPanel_Game resultPanel;

    private ISoundProvider _soundProvider;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        this._soundProvider = soundProvider;
    }

    public void Initialize()
    {
        balancePanel.Initialize();
        menuPanel.Initialize();
        mainPanel.Initialize();
        footerPanel.Initialize();
        roulettePanel.Initialize();
        resultPanel.Initialize();
    }

    public void Dispose()
    {
        balancePanel.Dispose();
        menuPanel.Dispose();
        mainPanel.Dispose();
        footerPanel.Dispose();
        roulettePanel.Dispose();
        resultPanel.Dispose();
    }

    public void Activate()
    {
        footerPanel.OnClickToSpin += HandleClickToSpin;
        menuPanel.OnClickToMenu += HandleClickToMenu;
    }

    public void Deactivate()
    {
        footerPanel.OnClickToSpin -= HandleClickToSpin;
        menuPanel.OnClickToMenu -= HandleClickToMenu;

        if (currentPanel != null)
            CloseOtherPanel(currentPanel);

        CloseBalancePanel();
        CloseFooterPanel();
        CloseMainPanel();
        CloseMenuPanel();
    }


    #region Input

    public void OpenMainPanel()
    {
        if (mainPanel.IsActive) return;

        OpenPanel(mainPanel);
    }

    public void CloseMainPanel()
    {
        CloseOtherPanel(mainPanel);
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




    public void OpenBalancePanel()
    {
        if(balancePanel.IsActive) return;

        OpenOtherPanel(balancePanel);
    }

    public void CloseBalancePanel()
    {
        if(!balancePanel.IsActive) return;

        CloseOtherPanel(balancePanel);
    }




    public void OpenMenuPanel()
    {
        if(menuPanel.IsActive) return;

        OpenOtherPanel(menuPanel);
    }

    public void CloseMenuPanel()
    {
        if(!menuPanel.IsActive) return;

        CloseOtherPanel(menuPanel);
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
        _soundProvider.PlayOneShot("Click");

        OnClickToMenu?.Invoke();
    }


    public event Action OnClickToSpin;

    private void HandleClickToSpin()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToSpin?.Invoke();
    }

    #endregion
}
