using System;
using UnityEngine;

public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private ChipGroup chipGroup;
    [SerializeField] private DailyRewardValues dailyRewardValues;
    [SerializeField] private TaskGroup taskGroup;
    [SerializeField] private UIMainMenuRoot menuRootPrefab;

    private UIMainMenuRoot sceneRoot;
    private ViewContainer viewContainer;

    private BankPresenter bankPresenter;
    private ParticleEffectPresenter particleEffectPresenter;
    private SoundPresenter soundPresenter;

    private CooldownPresenter cooldownPresenter_DailyReward;
    private DailyRewardPresenter dailyRewardPresenter;
    private DailyRewardScalePresenter dailyRewardScalePresenter;
    private DailyRewardVisualPresenter dailyRewardVisualPresenter;

    private StoreTaskPresenter storeTaskPresenter;
    private TaskVisualPresenter taskVisualPresenter;

    private StoreChipPresenter storeChipPresenter;
    private ChipBuyPresenter chipBuyPresenter;
    private ChipCountVisualPresenter chipCountVisualPresenter;

    public void Run(UIRootView uIRootView)
    {
        sceneRoot = menuRootPrefab;
 
        uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);

        viewContainer = sceneRoot.GetComponent<ViewContainer>();
        viewContainer.Initialize();

        soundPresenter = new SoundPresenter
            (new SoundModel(sounds.sounds, PlayerPrefsKeys.IS_MUTE_SOUNDS),
            viewContainer.GetView<SoundView>());

        particleEffectPresenter = new ParticleEffectPresenter
            (new ParticleEffectModel(),
            viewContainer.GetView<ParticleEffectView>());

        bankPresenter = new BankPresenter(new BankModel(), viewContainer.GetView<BankView>());

        cooldownPresenter_DailyReward = new CooldownPresenter(new CooldownModel(PlayerPrefsKeys.COOLDOWN_DAILY_REWARD, TimeSpan.FromSeconds(5)), viewContainer.GetView<CooldownView>());
        dailyRewardPresenter = new DailyRewardPresenter(new DailyRewardModel(PlayerPrefsKeys.DAY_DAILY_REWARD, dailyRewardValues, bankPresenter), viewContainer.GetView<DailyRewardView>());
        dailyRewardScalePresenter = new DailyRewardScalePresenter(new DailyRewardScaleModel(), viewContainer.GetView<DailyRewardScaleView>());
        dailyRewardVisualPresenter = new DailyRewardVisualPresenter(new DailyRewardVisualModel(), viewContainer.GetView<DailyRewardVisualView>());

        storeTaskPresenter = new StoreTaskPresenter(new StoreTaskModel(taskGroup, bankPresenter));
        taskVisualPresenter = new TaskVisualPresenter(new TaskVisualModel(), viewContainer.GetView<TaskVisualView>());

        storeChipPresenter = new StoreChipPresenter(new StoreChipModel(chipGroup));
        chipBuyPresenter = new ChipBuyPresenter(new ChipBuyModel(chipGroup, storeChipPresenter, bankPresenter), viewContainer.GetView<ChipBuyView>());
        chipCountVisualPresenter = new ChipCountVisualPresenter(new ChipCountVisualModel(), viewContainer.GetView<ChipCountVisualView>());

        sceneRoot.SetSoundProvider(soundPresenter);
        sceneRoot.Activate();

        ActivateEvents();

        soundPresenter.Initialize();
        particleEffectPresenter.Initialize();
        sceneRoot.Initialize();
        bankPresenter.Initialize();

        dailyRewardPresenter.Initialize();
        cooldownPresenter_DailyReward.Initialize();
        dailyRewardScalePresenter.Initialize();
        dailyRewardVisualPresenter.Initialize();

        taskVisualPresenter.Initialize();
        storeTaskPresenter.Initialize();

        chipBuyPresenter.Initialize();
        chipCountVisualPresenter.Initialize();
        storeChipPresenter.Initialize();

    }

    private void ActivateEvents()
    {
        ActivateTransitions();

        cooldownPresenter_DailyReward.OnRewardOverDay += dailyRewardPresenter.ResetDailyReward;
        cooldownPresenter_DailyReward.OnAvailable += dailyRewardPresenter.ActivateButtonReward;
        cooldownPresenter_DailyReward.OnUnvailable += dailyRewardPresenter.DeactivateButtonReward;
        dailyRewardPresenter.OnGetDailyReward += cooldownPresenter_DailyReward.ActivateCooldown;
        dailyRewardPresenter.OnChangeDay += dailyRewardScalePresenter.SetIndex;
        dailyRewardPresenter.OnResetDays += dailyRewardVisualPresenter.DeactivateDays;
        dailyRewardPresenter.OnLastOpenDay += dailyRewardVisualPresenter.ActivateDay;

        storeTaskPresenter.OnActiveTask += taskVisualPresenter.SetActivateTask;
        storeTaskPresenter.OnInactiveTask += taskVisualPresenter.SetInactivateTask;
        storeTaskPresenter.OnCompletedTask += taskVisualPresenter.SetCompletedTask;
        taskVisualPresenter.OnChooseTask += storeTaskPresenter.CompletedTask;

        storeChipPresenter.OnChangeCountChips += chipCountVisualPresenter.ChangeChipsCount;
    }

    private void DeactivateEvents()
    {
        DeactivateTransitions();

        cooldownPresenter_DailyReward.OnRewardOverDay -= dailyRewardPresenter.ResetDailyReward;
        cooldownPresenter_DailyReward.OnAvailable -= dailyRewardPresenter.ActivateButtonReward;
        cooldownPresenter_DailyReward.OnUnvailable -= dailyRewardPresenter.DeactivateButtonReward;
        dailyRewardPresenter.OnGetDailyReward -= cooldownPresenter_DailyReward.ActivateCooldown;
        dailyRewardPresenter.OnChangeDay -= dailyRewardScalePresenter.SetIndex;
        dailyRewardPresenter.OnResetDays -= dailyRewardVisualPresenter.DeactivateDays;
        dailyRewardPresenter.OnLastOpenDay -= dailyRewardVisualPresenter.ActivateDay;

        storeTaskPresenter.OnActiveTask -= taskVisualPresenter.SetActivateTask;
        storeTaskPresenter.OnInactiveTask -= taskVisualPresenter.SetInactivateTask;
        storeTaskPresenter.OnCompletedTask -= taskVisualPresenter.SetCompletedTask;
        taskVisualPresenter.OnChooseTask -= storeTaskPresenter.CompletedTask;
    }

    private void ActivateTransitions()
    {
        sceneRoot.OnClickToBack_DailyReward += sceneRoot.OpenMainPanel;
        sceneRoot.OnClickToBack_Tasks += sceneRoot.OpenMainPanel;
        sceneRoot.OnClickToBack_Chips += sceneRoot.OpenMainPanel;

        sceneRoot.OnClickToDailyReward_Main += sceneRoot.OpenDailyRewardPanel;
        sceneRoot.OnClickToTasks_Main += sceneRoot.OpenTasksPanel;
        sceneRoot.OnClickToChips_Main += sceneRoot.OpenChipsPanel;


        sceneRoot.OnClickToMini += HandleGoToRoulette_Mini;
        sceneRoot.OnClickToEuro += HandleGoToRoulette_Euro;
        sceneRoot.OnClickToAmerica += HandleGoToRoulette_America;
        sceneRoot.OnClickToAmericaMulti += HandleGoToRoulette_AmericaMulti;
        sceneRoot.OnClickToFrench += HandleGoToRoulette_French;
        sceneRoot.OnClickToAmericaTracker += HandleGoToRoulette_AmericaTracker;
    }

    private void DeactivateTransitions()
    {
        sceneRoot.OnClickToBack_DailyReward -= sceneRoot.OpenMainPanel;
        sceneRoot.OnClickToBack_Tasks -= sceneRoot.OpenMainPanel;
        sceneRoot.OnClickToBack_Chips -= sceneRoot.OpenMainPanel;

        sceneRoot.OnClickToDailyReward_Main -= sceneRoot.OpenDailyRewardPanel;
        sceneRoot.OnClickToTasks_Main -= sceneRoot.OpenTasksPanel;
        sceneRoot.OnClickToChips_Main -= sceneRoot.OpenChipsPanel;

        sceneRoot.OnClickToMini -= HandleGoToRoulette_Mini;
        sceneRoot.OnClickToEuro -= HandleGoToRoulette_Euro;
        sceneRoot.OnClickToAmerica -= HandleGoToRoulette_America;
        sceneRoot.OnClickToAmericaMulti -= HandleGoToRoulette_AmericaMulti;
        sceneRoot.OnClickToFrench -= HandleGoToRoulette_French;
        sceneRoot.OnClickToAmericaTracker -= HandleGoToRoulette_AmericaTracker;
    }

    private void Deactivate()
    {
        sceneRoot.Deactivate();
        soundPresenter?.Dispose();
    }

    private void Dispose()
    {
        DeactivateEvents();

        soundPresenter?.Dispose();
        sceneRoot?.Dispose();
        particleEffectPresenter?.Dispose();
        bankPresenter?.Dispose();

        cooldownPresenter_DailyReward?.Dispose();
        dailyRewardPresenter?.Dispose();
        dailyRewardScalePresenter?.Dispose();
        dailyRewardVisualPresenter?.Dispose();

        taskVisualPresenter?.Dispose();
        storeTaskPresenter?.Dispose();

        chipCountVisualPresenter?.Dispose();
        chipBuyPresenter?.Dispose();
        storeChipPresenter?.Dispose();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            storeTaskPresenter.ActivateTask("10games");
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            storeTaskPresenter.ActivateTask("4DifferentRoulettes");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            storeTaskPresenter.ActivateTask("Win3TimesRow");
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            storeTaskPresenter.ActivateTask("Spend15Minutes");
        }
    }

    private void OnDestroy()
    {
        Dispose();
    }

    #region Output

    public event Action OnGoToRoulette_Mini;
    public event Action OnGoToRoulette_Euro;
    public event Action OnGoToRoulette_America;
    public event Action OnGoToRoulette_AmericaMulti;
    public event Action OnGoToRoulette_French;
    public event Action OnGoToRoulette_AmericaTracker;

    private void HandleGoToRoulette_Mini()
    {
        Deactivate();
        OnGoToRoulette_Mini?.Invoke();
    }

    private void HandleGoToRoulette_Euro()
    {
        Deactivate();
        OnGoToRoulette_Euro?.Invoke();
    }

    private void HandleGoToRoulette_America()
    {
        Deactivate();
        OnGoToRoulette_America?.Invoke();
    }

    private void HandleGoToRoulette_AmericaMulti()
    {
        Deactivate();
        OnGoToRoulette_AmericaMulti?.Invoke();
    }

    private void HandleGoToRoulette_French()
    {
        Deactivate();
        OnGoToRoulette_French?.Invoke();
    }

    private void HandleGoToRoulette_AmericaTracker()
    {
        Deactivate();
        OnGoToRoulette_French?.Invoke();
    }

    #endregion
}
