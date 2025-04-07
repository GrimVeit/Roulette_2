using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDescriptionPresenter : ITutorialDescriptionProvider
{
    private readonly TutorialDescriptionModel model;
    private readonly TutorialDescriptionView view;

    public TutorialDescriptionPresenter(TutorialDescriptionModel model, TutorialDescriptionView view)
    {
        this.model = model;
        this.view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        model.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        model.Dispose();
    }

    private void ActivateEvents()
    {
        model.OnLockTutorial += view.DestroyTutorial;

        model.OnActivateTutorial += view.ActivateTutorial;
        model.OnDeactivateTutorial += view.DeactivateTutorial;
    }

    private void DeactivateEvents()
    {
        model.OnLockTutorial -= view.DestroyTutorial;

        model.OnActivateTutorial -= view.ActivateTutorial;
        model.OnDeactivateTutorial -= view.DeactivateTutorial;
    }

    #region Input

    public void ActivateTutorial(string id)
    {
        model.ActivateTutorial(id);
    }

    public void DeactivateTutorial(string id)
    {
        model.DeactivateTutorial(id);
    }

    public void LockTutorial(string id)
    {
        model.LockTutorial(id);
    }

    #endregion
}

public interface ITutorialDescriptionProvider
{
    public void ActivateTutorial(string id);

    public void DeactivateTutorial(string id);

    public void LockTutorial(string id);
}
