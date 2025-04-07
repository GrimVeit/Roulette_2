using System;

public class ChipMoveModel
{
    public event Action<ChipMove_Player> OnAddChip;
    public event Action<ChipMove_Player> OnRemoveChip;


    public event Action OnActivateChips;
    public event Action OnDeactivateChips;

    public event Action OnDoMotion;
    public event Action OnStoppedChip;

    private ITutorialDescriptionProvider tutorialDescriptionProvider;
    private ISoundProvider soundProvider;
    public ChipMoveModel(ITutorialDescriptionProvider tutorialDescriptionProvider, ISoundProvider soundProvider)
    {
        this.tutorialDescriptionProvider = tutorialDescriptionProvider;
        this.soundProvider = soundProvider;
    }

    public void AddChip(ChipMove_Player chipMove)
    {
        OnAddChip?.Invoke(chipMove);
    }

    public void RemoveChip(ChipMove_Player chipMove)
    {
        OnRemoveChip?.Invoke(chipMove);
    }

    public void ActivateChips()
    {
        OnActivateChips?.Invoke();
    }

    public void DeactivateChips()
    {
        OnDeactivateChips?.Invoke();
    }

    public void StartDrag()
    {
        tutorialDescriptionProvider.LockTutorial("StartGrabChip");

        tutorialDescriptionProvider.ActivateTutorial("DragAndDropChip");

        soundProvider.PlayOneShot("Chip_Aim");
    }

    public void EndDrag()
    {
        soundProvider.PlayOneShot("Chip_Fire");
    }

    public void DoMotion()
    {
        tutorialDescriptionProvider.LockTutorial("DragAndDropChip");

        OnDoMotion?.Invoke();
    }

    public void StopChip()
    {
        OnStoppedChip?.Invoke();
    }
}
