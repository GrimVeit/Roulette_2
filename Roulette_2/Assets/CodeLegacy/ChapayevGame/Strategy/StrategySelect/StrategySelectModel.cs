using System;

public class StrategySelectModel
{
    public event Action<Strategy> OnSetOpenStrategy;
    public event Action<Strategy> OnSetOpenNewStrategy;
    public event Action<int> OnSelectStrategy;
    public event Action<int> OnDeselectStrategy;

    public event Action<int> OnChooseStrategy;

    private ITutorialDescriptionProvider tutorialDescriptionProvider;
    private ISoundProvider soundProvider;

    public StrategySelectModel(ITutorialDescriptionProvider tutorialDescriptionProvider, ISoundProvider soundProvider)
    {
        this.tutorialDescriptionProvider = tutorialDescriptionProvider;
        this.soundProvider = soundProvider;
    }

    public void SetOpenStrategy(Strategy strategy)
    {
        OnSetOpenStrategy?.Invoke(strategy);
    }

    public void SetOpenNewStrategy(Strategy strategy)
    {
        OnSetOpenNewStrategy?.Invoke(strategy);
    }

    public void SelectStrategy(int id)
    {
        OnSelectStrategy?.Invoke(id);
    }

    public void DeselectStrategy(int id)
    {
        OnDeselectStrategy?.Invoke(id);
    }



    public void ChooseStrategy(int id)
    {
        tutorialDescriptionProvider.LockTutorial("ChooseStrategy");
        soundProvider.PlayOneShot("Select");

        OnChooseStrategy?.Invoke(id);
    }
}
