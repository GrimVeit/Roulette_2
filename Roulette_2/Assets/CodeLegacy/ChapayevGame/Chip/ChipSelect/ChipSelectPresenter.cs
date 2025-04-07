using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipSelectPresenter
{
    private readonly ChipSelectModel model;
    private readonly ChipSelectView view;

    public ChipSelectPresenter(ChipSelectModel model, ChipSelectView view)
    {
        this.model = model;
        this.view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        view.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        view.Dispose();
    }

    private void ActivateEvents()
    {
        view.OnChooseChip += model.ChooseChip;

        model.OnSetOpenChip += view.SetOpenChip;
        model.OnSetOpenNewChip += view.SetOpenNewChip;
        model.OnSelectChip += view.SelectChip;
        model.OnDeselectChip += view.DeselectChip;
        model.OnActivate += view.Activate;
        model.OnDeactivate += view.Deactivate;
        model.OnSetChipCount += view.SetChipCount;
    }

    private void DeactivateEvents()
    {
        view.OnChooseChip -= model.ChooseChip;

        model.OnSetOpenChip -= view.SetOpenChip;
        model.OnSetOpenNewChip -= view.SetOpenNewChip;
        model.OnSelectChip -= view.SelectChip;
        model.OnDeselectChip -= view.DeselectChip;
        model.OnActivate -= view.Activate;
        model.OnDeactivate -= view.Deactivate;
        model.OnSetChipCount -= view.SetChipCount;
    }

    #region Input

    public event Action<int> OnChooseChip
    {
        add => model.OnChooseChip += value;
        remove => model.OnChooseChip -= value;
    }

    public void SetStrategy(Strategy strategy)
    {
        model.SetCountChip(strategy.ChipCount);
    }

    public void SelectChip(Chip chip)
    {
        model.SelectChip(chip.ID);
    }

    public void DeselectChip(Chip chip)
    {
        model.DeselectChip(chip.ID);
    }

    public void SetOpenChip(Chip chip)
    {
        model.SetOpenChip(chip);
    }

    public void SetOpenNewChip(Chip chip)
    {
        model.SetOpenNewChip(chip);
    }

    #endregion
}
