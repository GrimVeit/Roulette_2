using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TutorialDescriptionView : View
{
    [SerializeField] private TutorialDescriptionVisual tutorialDescriptionVisualPrefab;
    [SerializeField] private Transform transformParent;

    private List<TutorialDescriptionVisual> tutorialDescriptionVisuals = new List<TutorialDescriptionVisual>();

    public void ActivateTutorial(TutorialDescription tutorialDescription)
    {
        var tutorialDescriptionVisual = tutorialDescriptionVisuals.FirstOrDefault(data => data.ID == tutorialDescription.GetID());

        if(tutorialDescriptionVisual == null)
        {
            tutorialDescriptionVisual = Instantiate(tutorialDescriptionVisualPrefab, transformParent);
            tutorialDescriptionVisual.OnTimerCloseEnd += DeactivateTutorial;
            tutorialDescriptionVisual.SetData(tutorialDescription);
            tutorialDescriptionVisual.Initialize();
            tutorialDescriptionVisuals.Add(tutorialDescriptionVisual);
        }

        if (tutorialDescriptionVisual.IsActive) return;

        tutorialDescriptionVisual.ActivatePanel();
    }

    public void DeactivateTutorial(TutorialDescription tutorialDescription)
    {
        var tutorialDescriptionVisual = tutorialDescriptionVisuals.FirstOrDefault(data => data.ID == tutorialDescription.GetID());

        DeactivateTutorial(tutorialDescriptionVisual);
    }

    public void DestroyTutorial(TutorialDescription tutorialDescription)
    {
        var tutorialDescriptionVisual = tutorialDescriptionVisuals.FirstOrDefault(data => data.ID == tutorialDescription.GetID());

        if (tutorialDescriptionVisual == null) return;

        tutorialDescriptionVisuals.Remove(tutorialDescriptionVisual);

        if (tutorialDescriptionVisual.IsActive)
        {
            tutorialDescriptionVisual.OnDeactivatePanel += DestroyTutorial;
            tutorialDescriptionVisual.DeactivateTimerClose();
            tutorialDescriptionVisual.DeactivatePanel();
        }
        else
        {
            DestroyTutorial(tutorialDescriptionVisual);
        }
    }

    private void DeactivateTutorial(TutorialDescriptionVisual tutorialDescriptionVisual)
    {
        if (tutorialDescriptionVisual == null) return;

        if(!tutorialDescriptionVisual.IsActive) return;

        tutorialDescriptionVisual.DeactivateTimerClose();
        tutorialDescriptionVisual.DeactivatePanel();
    }

    private void DestroyTutorial(MovePanel movePanel)
    {
        movePanel.OnDeactivatePanel -= DestroyTutorial;
        movePanel.Dispose();

        Destroy(movePanel.gameObject);
    }
}
