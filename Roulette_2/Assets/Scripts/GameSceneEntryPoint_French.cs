using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneEntryPoint_French : MonoBehaviour
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

    private RoulettePresenter roulettePresenter;
    private RouletteBallPresenter rouletteBallPresenter;

    private RouletteValueHistoryPresenter rouletteValueHistoryPresenter;

    private StoreGameProgressPresenter storeGameProgressPresenter;
    private DialoguePresenter dialoguePresenter;
    private HighlightPresenter highlightPresenter;
    private HandPointerPresenter handPointerPresenter;

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

    private StateMachine_French stateMachine;

    public void Run(UIRootView uIRootView)
    {
        sceneRoot = sceneRootPrefab;

        uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);

        viewContainer = sceneRoot.GetComponent<ViewContainer>();
        viewContainer.Initialize();

        soundPresenter = new SoundPresenter(new SoundModel(sounds.sounds, PlayerPrefsKeys.IS_MUTE_SOUNDS), viewContainer.GetView<SoundView>());
        bankPresenter = new BankPresenter(new BankModel(), viewContainer.GetView<BankView>());
        particleEffectPresenter = new ParticleEffectPresenter(new ParticleEffectModel(), viewContainer.GetView<ParticleEffectView>());

        roulettePresenter = new RoulettePresenter(new RouletteModel(soundPresenter), viewContainer.GetView<RouletteView>());
        rouletteBallPresenter = new RouletteBallPresenter(new RouletteBallModel(soundPresenter), viewContainer.GetView<RouletteBallView>());

        rouletteValueHistoryPresenter = new RouletteValueHistoryPresenter(new RouletteValueHistoryModel(new List<IRouletteValueProvider>() { roulettePresenter }), viewContainer.GetView<RouletteValueHistoryView>());

        timerDailyPresenter = new TimerDailyPresenter(new TimerDailyModel(PlayerPrefsKeys.LAST_EXIT_DATE));
        storeTaskPresenter = new StoreTaskPresenter(new StoreTaskModel(taskGroup, bankPresenter, timerDailyPresenter, soundPresenter));
        metric_GameCountPresenter = new Metric_GameCountPresenter(new Metric_GameCountModel(PlayerPrefsKeys.METRIC_GAME_COUNTS, storeTaskPresenter, timerDailyPresenter, 10));
        metric_GameTypeCountPresenter = new Metric_GameTypeCountPresenter(new Metric_GameTypeCountModel(PlayerPrefsKeys.METRIC_GAME_TYPE_COUNTS, 4, storeTaskPresenter, timerDailyPresenter));
        metric_GameTimeSessionPresenter = new Metric_GameTimeSessionPresenter(new Metric_GameTimeSessionModel(PlayerPrefsKeys.METRIC_GAME_TIME_SESSION, timerDailyPresenter, storeTaskPresenter, 15));
        metric_WinCountPresenter = new Metric_WinCountPresenter(new Metric_WinCountModel(PlayerPrefsKeys.METRIC_WIN_ROW_COUNTS, 3, timerDailyPresenter, storeTaskPresenter));
        metric_BetNumberPresenter = new Metric_BetNumberPresenter(new Metric_BetNumberModel(PlayerPrefsKeys.METRIC_BET_NUMBER_COUNTS, 1, timerDailyPresenter, storeTaskPresenter));

        storeGameProgressPresenter = new StoreGameProgressPresenter(new StoreGameProgressModel());
        dialoguePresenter = new DialoguePresenter(new DialogueModel(dialogueGroup, soundPresenter), viewContainer.GetView<DialogueView>());
        highlightPresenter = new HighlightPresenter(new HighlightModel(), viewContainer.GetView<HighlightView>());
        handPointerPresenter = new HandPointerPresenter(new HandPointerModel(), viewContainer.GetView<HandPointerView>());

        notificationPresenter = new NotificationPresenter(new NotificationModel(), viewContainer.GetView<NotificationView>());
        notificationGameTypePresenter = new NotificationGameTypePresenter(new NotificationGameTypeModel(notificationPresenter, storeGameProgressPresenter, soundPresenter), viewContainer.GetView<NotificationGameTypeView>());
        notificationTaskPresenter = new NotificationTaskPresenter(new NotificationTaskModel(notificationPresenter, storeTaskPresenter, soundPresenter), viewContainer.GetView<NotificationTaskView>());

        scoreRecordPresenter = new ScoreRecordPresenter(new ScoreRecordModel(PlayerPrefsKeys.RECORD));
        storeChipPresenter = new StoreChipPresenter(new StoreChipModel(chipGroup));
        chipGameCountVisualPresenter = new ChipGameCountVisualPresenter(new ChipGameCountVisualModel(storeChipPresenter), viewContainer.GetView<ChipGameCountVisualView>());
        pseudoChipPresenter = new PseudoChipPresenter(new PseudoChipModel(soundPresenter), viewContainer.GetView<PseudoChipView>());
        betPresenter = new BetPresenter(new BetModel(chipGroup, storeChipPresenter, bets, new List<IRouletteValueProvider>() { roulettePresenter }, bankPresenter, metric_BetNumberPresenter, metric_WinCountPresenter, notificationPresenter, soundPresenter, scoreRecordPresenter), viewContainer.GetView<BetView>());
        betCellPresenter = new BetCellPresenter(new BetCellModel(betPresenter), viewContainer.GetView<BetCellView>());
        chipGameVisualPresenter = new ChipGameVisualPresenter(new ChipGameVisualModel(betPresenter), viewContainer.GetView<ChipGameVisualView>());

        animationFramePresenter = new AnimationFramePresenter(new AnimationFrameModel(), viewContainer.GetView<AnimationFrameView>());

        stateMachine = new StateMachine_French
            (sceneRoot,
            rouletteBallPresenter,
            roulettePresenter,
            rouletteValueHistoryPresenter,
            storeGameProgressPresenter,
            betPresenter,
            dialoguePresenter,
            highlightPresenter,
            handPointerPresenter,
            betCellPresenter,
            pseudoChipPresenter,
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

        rouletteBallPresenter.Initialize();
        roulettePresenter.Initialize();

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

        handPointerPresenter.Initialize();
        highlightPresenter.Initialize();
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

        rouletteBallPresenter.Dispose();
        roulettePresenter.Dispose();

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
        metric_WinCountPresenter.Dispose();
        metric_BetNumberPresenter.Dispose();

        animationFramePresenter.Dispose();

        handPointerPresenter.Dispose();
        highlightPresenter.Dispose();
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
