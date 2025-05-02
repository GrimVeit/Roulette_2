using System;
using UnityEngine;

public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private ChipGroup chipGroup;
    [SerializeField] private DialogueGroup dialogueGroup;
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
    private ChipMenuCountVisualPresenter chipCountVisualPresenter;

    private StoreGameProgressPresenter storeGameProgressPresenter;
    private GameProgressVisualPresenter gameProgressVisualPresenter;

    private DialoguePresenter dialoguePresenter;

    private TimerDailyPresenter timerDailyPresenter;
    private TimerDailyVisualPresenter timerDailyVisualPresenter;
    private Metric_GameTimeSessionPresenter metric_GameTimeSessionPresenter;
    private Metric_GameCountPresenter metric_GameCountPresenter;
    private Metric_GameTypeCountPresenter metric_GameTypeCountPresenter;
    private Metric_WinCountPresenter metric_WinCountPresenter;
    private Metric_BetNumberPresenter metric_BetNumberPresenter;

    private StateMachine_Menu stateMachine;

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

        timerDailyPresenter = new TimerDailyPresenter(new TimerDailyModel(PlayerPrefsKeys.LAST_EXIT_DATE));
        timerDailyVisualPresenter = new TimerDailyVisualPresenter(new TimerDailyVisualModel(timerDailyPresenter), viewContainer.GetView<TimerDailyVisualView>());

        cooldownPresenter_DailyReward = new CooldownPresenter(new CooldownModel(PlayerPrefsKeys.COOLDOWN_DAILY_REWARD, TimeSpan.FromDays(1)), viewContainer.GetView<CooldownView>());
        dailyRewardPresenter = new DailyRewardPresenter(new DailyRewardModel(PlayerPrefsKeys.DAY_DAILY_REWARD, dailyRewardValues, bankPresenter), viewContainer.GetView<DailyRewardView>());
        dailyRewardScalePresenter = new DailyRewardScalePresenter(new DailyRewardScaleModel(), viewContainer.GetView<DailyRewardScaleView>());
        dailyRewardVisualPresenter = new DailyRewardVisualPresenter(new DailyRewardVisualModel(), viewContainer.GetView<DailyRewardVisualView>());

        storeTaskPresenter = new StoreTaskPresenter(new StoreTaskModel(taskGroup, bankPresenter, timerDailyPresenter));
        taskVisualPresenter = new TaskVisualPresenter(new TaskVisualModel(storeTaskPresenter, storeTaskPresenter), viewContainer.GetView<TaskVisualView>());

        storeChipPresenter = new StoreChipPresenter(new StoreChipModel(chipGroup));
        chipBuyPresenter = new ChipBuyPresenter(new ChipBuyModel(chipGroup, storeChipPresenter, bankPresenter), viewContainer.GetView<ChipBuyView>());
        chipCountVisualPresenter = new ChipMenuCountVisualPresenter(new ChipMenuCountVisualModel(), viewContainer.GetView<ChipMenuCountVisualView>());

        storeGameProgressPresenter = new StoreGameProgressPresenter(new StoreGameProgressModel());
        gameProgressVisualPresenter = new GameProgressVisualPresenter(new GameProgressVisualModel(storeGameProgressPresenter), viewContainer.GetView<GameProgressVisualView>());

        dialoguePresenter = new DialoguePresenter(new DialogueModel(dialogueGroup), viewContainer.GetView<DialogueView>());

        metric_GameTimeSessionPresenter = new Metric_GameTimeSessionPresenter(new Metric_GameTimeSessionModel(PlayerPrefsKeys.METRIC_GAME_TIME_SESSION, timerDailyPresenter, storeTaskPresenter, 15));
        metric_GameCountPresenter = new Metric_GameCountPresenter(new Metric_GameCountModel(PlayerPrefsKeys.METRIC_GAME_COUNTS, storeTaskPresenter, timerDailyPresenter, 10));
        metric_GameTypeCountPresenter = new Metric_GameTypeCountPresenter(new Metric_GameTypeCountModel(PlayerPrefsKeys.METRIC_GAME_TYPE_COUNTS, 4, storeTaskPresenter, timerDailyPresenter));
        metric_WinCountPresenter = new Metric_WinCountPresenter(new Metric_WinCountModel(PlayerPrefsKeys.METRIC_WIN_ROW_COUNTS, 3, timerDailyPresenter, storeTaskPresenter));
        metric_BetNumberPresenter = new Metric_BetNumberPresenter(new Metric_BetNumberModel(PlayerPrefsKeys.METRIC_BET_NUMBER_COUNTS, 1, timerDailyPresenter, storeTaskPresenter));

        stateMachine = new StateMachine_Menu(sceneRoot, dialoguePresenter, storeGameProgressPresenter);

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

        timerDailyPresenter.Initialize();
        timerDailyVisualPresenter.Initialize();

        gameProgressVisualPresenter.Initialize();
        storeGameProgressPresenter.Initialize();

        dialoguePresenter.Initialize();

        metric_GameTimeSessionPresenter.Initialize();
        metric_GameCountPresenter.Initialize();
        metric_GameTypeCountPresenter.Initialize();
        metric_WinCountPresenter.Initialize();
        metric_BetNumberPresenter.Initialize();

        stateMachine.Initialize();

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
    }

    private void ActivateTransitions()
    {
        sceneRoot.OnClickToMini += HandleGoToRoulette_Mini;
        sceneRoot.OnClickToEuro += HandleGoToRoulette_Euro;
        sceneRoot.OnClickToAmerica += HandleGoToRoulette_America;
        sceneRoot.OnClickToAmericaMulti += HandleGoToRoulette_AmericaMulti;
        sceneRoot.OnClickToFrench += HandleGoToRoulette_French;
        sceneRoot.OnClickToAmericaTracker += HandleGoToRoulette_AmericaTracker;
    }

    private void DeactivateTransitions()
    {
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

        timerDailyPresenter?.Dispose();
        timerDailyVisualPresenter?.Dispose();

        gameProgressVisualPresenter.Dispose();
        storeGameProgressPresenter.Dispose();

        dialoguePresenter?.Dispose();

        metric_GameTimeSessionPresenter?.Dispose();
        metric_GameCountPresenter?.Dispose();
        metric_GameTypeCountPresenter?.Dispose();
        metric_WinCountPresenter?.Dispose();
        metric_BetNumberPresenter?.Dispose();

        stateMachine?.Dispose();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    storeGameProgressPresenter.OpenGame(2);
        //}

        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    storeGameProgressPresenter.OpenGame(4);
        //}

        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    storeGameProgressPresenter.OpenGame(1);
        //}

        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    storeGameProgressPresenter.OpenGame(6);
        //}

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    storeGameProgressPresenter.OpenGame(5);
        //}
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
        OnGoToRoulette_AmericaTracker?.Invoke();
    }

    #endregion
}
