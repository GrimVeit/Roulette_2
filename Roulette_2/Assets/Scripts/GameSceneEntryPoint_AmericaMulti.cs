using System;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneEntryPoint_AmericaMulti : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private DialogueGroup dialogueGroup;
    [SerializeField] private TaskGroup taskGroup;
    [SerializeField] private ChipGroup chipGroup;
    [SerializeField] private Bets bets;
    [SerializeField] private UIGameSceneRoot_Game sceneRootPrefab;

    private UIGameSceneRoot_Game sceneRoot;
    private ViewContainer viewContainer;
    private BankPresenter bankPresenter;
    private SoundPresenter soundPresenter;
    private ParticleEffectPresenter particleEffectPresenter;

    private RoulettePresenter roulettePresenter_1;
    private RouletteBallPresenter rouletteBallPresenter_1;
    private RoulettePresenter roulettePresenter_2;
    private RouletteBallPresenter rouletteBallPresenter_2;
    private RoulettePresenter roulettePresenter_3;
    private RouletteBallPresenter rouletteBallPresenter_3;
    private RoulettePresenter roulettePresenter_4;
    private RouletteBallPresenter rouletteBallPresenter_4;
    private RoulettePresenter roulettePresenter_5;
    private RouletteBallPresenter rouletteBallPresenter_5;
    private RoulettePresenter roulettePresenter_6;
    private RouletteBallPresenter rouletteBallPresenter_6;

    private RouletteValueHistoryPresenter rouletteValueHistoryPresenter;

    private StoreGameProgressPresenter storeGameProgressPresenter;
    private DialoguePresenter dialoguePresenter;

    private StoreChipPresenter storeChipPresenter;
    private ChipGameCountVisualPresenter chipGameCountVisualPresenter;
    private PseudoChipPresenter pseudoChipPresenter;
    private BetCellPresenter betCellPresenter;
    private BetPresenter betPresenter;
    private ChipGameVisualPresenter chipGameVisualPresenter;
    private ScoreRecordPresenter scoreRecordPresenter;

    private StoreTaskPresenter storeTaskPresenter;
    private TimerDailyPresenter timerDailyPresenter;
    private Metric_GameCountPresenter metric_GameCountPresenter;
    private Metric_GameTypeCountPresenter metric_GameTypeCountPresenter;
    private Metric_GameTimeSessionPresenter metric_GameTimeSessionPresenter;
    private Metric_WinCountPresenter metric_WinCountPresenter;
    private Metric_BetNumberPresenter metric_BetNumberPresenter;

    private AnimationFramePresenter animationFramePresenter;

    private NotificationPresenter notificationPresenter;
    private NotificationGameTypePresenter notificationGameTypePresenter;
    private NotificationTaskPresenter notificationTaskPresenter;

    private StateMachine_AmericaMulti stateMachine;

    public void Run(UIRootView uIRootView)
    {
        sceneRoot = sceneRootPrefab;

        uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);

        viewContainer = sceneRoot.GetComponent<ViewContainer>();
        viewContainer.Initialize();

        soundPresenter = new SoundPresenter(new SoundModel(sounds.sounds, PlayerPrefsKeys.IS_MUTE_SOUNDS), viewContainer.GetView<SoundView>());
        bankPresenter = new BankPresenter(new BankModel(), viewContainer.GetView<BankView>());
        particleEffectPresenter = new ParticleEffectPresenter(new ParticleEffectModel(), viewContainer.GetView<ParticleEffectView>());

        roulettePresenter_1 = new RoulettePresenter(new RouletteModel(soundPresenter), viewContainer.GetView<RouletteView>("Roulette_1"));
        rouletteBallPresenter_1 = new RouletteBallPresenter(new RouletteBallModel(soundPresenter), viewContainer.GetView<RouletteBallView>("Roulette_1"));
        roulettePresenter_2 = new RoulettePresenter(new RouletteModel(soundPresenter), viewContainer.GetView<RouletteView>("Roulette_2"));
        rouletteBallPresenter_2 = new RouletteBallPresenter(new RouletteBallModel(soundPresenter), viewContainer.GetView<RouletteBallView>("Roulette_2"));
        roulettePresenter_3 = new RoulettePresenter(new RouletteModel(soundPresenter), viewContainer.GetView<RouletteView>("Roulette_3"));
        rouletteBallPresenter_3 = new RouletteBallPresenter(new RouletteBallModel(soundPresenter), viewContainer.GetView<RouletteBallView>("Roulette_3"));
        roulettePresenter_4 = new RoulettePresenter(new RouletteModel(soundPresenter), viewContainer.GetView<RouletteView>("Roulette_4"));
        rouletteBallPresenter_4 = new RouletteBallPresenter(new RouletteBallModel(soundPresenter), viewContainer.GetView<RouletteBallView>("Roulette_4"));
        roulettePresenter_5 = new RoulettePresenter(new RouletteModel(soundPresenter), viewContainer.GetView<RouletteView>("Roulette_5"));
        rouletteBallPresenter_5 = new RouletteBallPresenter(new RouletteBallModel(soundPresenter), viewContainer.GetView<RouletteBallView>("Roulette_5"));
        roulettePresenter_6 = new RoulettePresenter(new RouletteModel(soundPresenter), viewContainer.GetView<RouletteView>("Roulette_6"));
        rouletteBallPresenter_6 = new RouletteBallPresenter(new RouletteBallModel(soundPresenter), viewContainer.GetView<RouletteBallView>("Roulette_6"));

        rouletteValueHistoryPresenter = new RouletteValueHistoryPresenter(new RouletteValueHistoryModel(new List<IRouletteValueProvider>() 
        { roulettePresenter_1, roulettePresenter_2, roulettePresenter_3, roulettePresenter_4, roulettePresenter_5, roulettePresenter_6 }), viewContainer.GetView<RouletteValueHistoryView>());

        timerDailyPresenter = new TimerDailyPresenter(new TimerDailyModel(PlayerPrefsKeys.LAST_EXIT_DATE));
        storeTaskPresenter = new StoreTaskPresenter(new StoreTaskModel(taskGroup, bankPresenter, timerDailyPresenter, soundPresenter));
        metric_GameCountPresenter = new Metric_GameCountPresenter(new Metric_GameCountModel(PlayerPrefsKeys.METRIC_GAME_COUNTS, storeTaskPresenter, timerDailyPresenter, 10));
        metric_GameTypeCountPresenter = new Metric_GameTypeCountPresenter(new Metric_GameTypeCountModel(PlayerPrefsKeys.METRIC_GAME_TYPE_COUNTS, 4, storeTaskPresenter, timerDailyPresenter));
        metric_GameTimeSessionPresenter = new Metric_GameTimeSessionPresenter(new Metric_GameTimeSessionModel(PlayerPrefsKeys.METRIC_GAME_TIME_SESSION, timerDailyPresenter, storeTaskPresenter, 15));
        metric_WinCountPresenter = new Metric_WinCountPresenter(new Metric_WinCountModel(PlayerPrefsKeys.METRIC_WIN_ROW_COUNTS, 3, timerDailyPresenter, storeTaskPresenter));
        metric_BetNumberPresenter = new Metric_BetNumberPresenter(new Metric_BetNumberModel(PlayerPrefsKeys.METRIC_BET_NUMBER_COUNTS, 1, timerDailyPresenter, storeTaskPresenter));

        storeGameProgressPresenter = new StoreGameProgressPresenter(new StoreGameProgressModel());
        dialoguePresenter = new DialoguePresenter(new DialogueModel(dialogueGroup, soundPresenter), viewContainer.GetView<DialogueView>());

        notificationPresenter = new NotificationPresenter(new NotificationModel(), viewContainer.GetView<NotificationView>());
        notificationGameTypePresenter = new NotificationGameTypePresenter(new NotificationGameTypeModel(notificationPresenter, storeGameProgressPresenter, soundPresenter), viewContainer.GetView<NotificationGameTypeView>());
        notificationTaskPresenter = new NotificationTaskPresenter(new NotificationTaskModel(notificationPresenter, storeTaskPresenter, soundPresenter), viewContainer.GetView<NotificationTaskView>());

        scoreRecordPresenter = new ScoreRecordPresenter(new ScoreRecordModel(PlayerPrefsKeys.RECORD));
        storeChipPresenter = new StoreChipPresenter(new StoreChipModel(chipGroup));
        chipGameCountVisualPresenter = new ChipGameCountVisualPresenter(new ChipGameCountVisualModel(storeChipPresenter), viewContainer.GetView<ChipGameCountVisualView>());
        pseudoChipPresenter = new PseudoChipPresenter(new PseudoChipModel(soundPresenter), viewContainer.GetView<PseudoChipView>());
        betPresenter = new BetPresenter(new BetModel(chipGroup, storeChipPresenter, bets, new List<IRouletteValueProvider>() { roulettePresenter_1, roulettePresenter_2, roulettePresenter_3, roulettePresenter_4, roulettePresenter_5, roulettePresenter_6 }, bankPresenter, metric_BetNumberPresenter, metric_WinCountPresenter, notificationPresenter, soundPresenter, scoreRecordPresenter), viewContainer.GetView<BetView>());
        betCellPresenter = new BetCellPresenter(new BetCellModel(betPresenter), viewContainer.GetView<BetCellView>());
        chipGameVisualPresenter = new ChipGameVisualPresenter(new ChipGameVisualModel(betPresenter), viewContainer.GetView<ChipGameVisualView>());

        animationFramePresenter = new AnimationFramePresenter(new AnimationFrameModel(), viewContainer.GetView<AnimationFrameView>());

        stateMachine = new StateMachine_AmericaMulti(
            sceneRoot, 
            new List<RouletteBallPresenter>() { 
                rouletteBallPresenter_1, 
                rouletteBallPresenter_2, 
                rouletteBallPresenter_3, 
                rouletteBallPresenter_4, 
                rouletteBallPresenter_5, 
                rouletteBallPresenter_6},
            new List<RoulettePresenter>()
            {
                roulettePresenter_1,
                roulettePresenter_2,
                roulettePresenter_3,
                roulettePresenter_4,
                roulettePresenter_5,
                roulettePresenter_6,
            },
            rouletteValueHistoryPresenter,
            betPresenter,
            storeGameProgressPresenter,
            betCellPresenter,
            pseudoChipPresenter,
            dialoguePresenter,
            metric_GameCountPresenter,
            metric_GameTypeCountPresenter,
            animationFramePresenter,
            soundPresenter);

        sceneRoot.SetSoundProvider(soundPresenter);
        sceneRoot.Activate();

        ActivateEvents();

        sceneRoot.Initialize();

        bankPresenter.Initialize();
        soundPresenter.Initialize();
        particleEffectPresenter.Initialize();

        rouletteBallPresenter_1.Initialize();
        roulettePresenter_1.Initialize();
        rouletteBallPresenter_2.Initialize();
        roulettePresenter_2.Initialize();
        rouletteBallPresenter_3.Initialize();
        roulettePresenter_3.Initialize();
        rouletteBallPresenter_4.Initialize();
        roulettePresenter_4.Initialize();
        rouletteBallPresenter_5.Initialize();
        roulettePresenter_5.Initialize();
        rouletteBallPresenter_6.Initialize();
        roulettePresenter_6.Initialize();

        rouletteValueHistoryPresenter.Initialize();

        chipGameCountVisualPresenter.Initialize();
        storeChipPresenter.Initialize();
        pseudoChipPresenter.Initialize();
        betPresenter.Initialize();
        betCellPresenter.Initialize();
        chipGameVisualPresenter.Initialize();
        scoreRecordPresenter.Initialize();

        timerDailyPresenter.Initialize();
        storeTaskPresenter.Initialize();
        metric_GameCountPresenter.Initialize();
        metric_GameTypeCountPresenter.Initialize();
        metric_GameTimeSessionPresenter.Initialize();
        metric_WinCountPresenter.Initialize();
        metric_BetNumberPresenter.Initialize();

        animationFramePresenter.Initialize();

        dialoguePresenter.Initialize();
        storeGameProgressPresenter.Initialize();

        notificationTaskPresenter.Initialize();
        notificationGameTypePresenter.Initialize();
        notificationPresenter.Initialize();

        stateMachine.Initialize();
    }

    private void ActivateEvents()
    {
        ActivateTransitionsSceneEvents();
    }

    private void DeactivateEvents()
    {
        DeactivateTransitionsSceneEvents();
    }

    private void ActivateTransitionsSceneEvents()
    {
        sceneRoot.OnClickToMenu += HandleGoToMenu;
    }

    private void DeactivateTransitionsSceneEvents()
    {
        sceneRoot.OnClickToMenu -= HandleGoToMenu;
    }

    public void Dispose()
    {
        sceneRoot.Deactivate();
        sceneRoot.Dispose();

        DeactivateEvents();

        bankPresenter.Dispose();
        particleEffectPresenter.Dispose();

        rouletteBallPresenter_1.Dispose();
        roulettePresenter_1.Dispose();
        rouletteBallPresenter_2.Dispose();
        roulettePresenter_2.Dispose();
        rouletteBallPresenter_3.Dispose();
        roulettePresenter_3.Dispose();
        rouletteBallPresenter_4.Dispose();
        roulettePresenter_4.Dispose();
        rouletteBallPresenter_5.Dispose();
        roulettePresenter_5.Dispose();
        rouletteBallPresenter_6.Dispose();
        roulettePresenter_6.Dispose();

        rouletteValueHistoryPresenter.Dispose();

        chipGameCountVisualPresenter.Dispose();
        storeChipPresenter.Dispose();
        pseudoChipPresenter?.Dispose();
        betPresenter.Dispose();
        betCellPresenter.Dispose();
        chipGameVisualPresenter?.Dispose();
        scoreRecordPresenter.Dispose();

        timerDailyPresenter.Dispose();
        storeTaskPresenter.Dispose();
        metric_GameCountPresenter.Dispose();
        metric_GameTypeCountPresenter.Dispose();
        metric_GameTimeSessionPresenter.Dispose();
        metric_WinCountPresenter?.Dispose();
        metric_BetNumberPresenter?.Dispose();

        animationFramePresenter.Dispose();

        dialoguePresenter.Dispose();
        storeGameProgressPresenter.Dispose();

        notificationPresenter?.Dispose();
        notificationGameTypePresenter?.Dispose();
        notificationTaskPresenter?.Dispose();

        stateMachine.Dispose();
    }

    private void OnDestroy()
    {
        Dispose();
    }

    #region Output

    public event Action OnGoToMenu;

    private void HandleGoToMenu()
    {
        sceneRoot.Deactivate();
        soundPresenter.Dispose();
        OnGoToMenu?.Invoke();
    }

    #endregion
}
