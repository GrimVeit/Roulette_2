using System;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneEntryPoint_America : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private TaskGroup taskGroup;
    [SerializeField] private UIGameSceneRoot_Game sceneRootPrefab;

    private UIGameSceneRoot_Game sceneRoot;
    private ViewContainer viewContainer;
    private BankPresenter bankPresenter;
    private SoundPresenter soundPresenter;
    private ParticleEffectPresenter particleEffectPresenter;

    private RoulettePresenter roulettePresenter;
    private RouletteBallPresenter rouletteBallPresenter;

    private RouletteValueHistoryPresenter rouletteValueHistoryPresenter;

    private StoreTaskPresenter storeTaskPresenter;
    private TimerDailyPresenter timerDailyPresenter;
    private Metric_GameCountPresenter metric_GameCountPresenter;
    private Metric_GameTypeCountPresenter metric_GameTypeCountPresenter;
    private Metric_GameTimeSessionPresenter metric_GameTimeSessionPresenter;

    private StateMachine_America stateMachine;

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
        storeTaskPresenter = new StoreTaskPresenter(new StoreTaskModel(taskGroup, bankPresenter, timerDailyPresenter));
        metric_GameCountPresenter = new Metric_GameCountPresenter(new Metric_GameCountModel(PlayerPrefsKeys.METRIC_GAME_COUNTS, storeTaskPresenter, timerDailyPresenter, 10));
        metric_GameTypeCountPresenter = new Metric_GameTypeCountPresenter(new Metric_GameTypeCountModel(PlayerPrefsKeys.METRIC_GAME_TYPE_COUNTS, 4, storeTaskPresenter, timerDailyPresenter));
        metric_GameTimeSessionPresenter = new Metric_GameTimeSessionPresenter(new Metric_GameTimeSessionModel(PlayerPrefsKeys.METRIC_GAME_TIME_SESSION, timerDailyPresenter, storeTaskPresenter, 15));

        stateMachine = new StateMachine_America(sceneRoot, rouletteBallPresenter, roulettePresenter, rouletteValueHistoryPresenter, metric_GameCountPresenter, metric_GameTypeCountPresenter);

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

        timerDailyPresenter.Initialize();
        storeTaskPresenter.Initialize();
        metric_GameCountPresenter.Initialize();
        metric_GameTypeCountPresenter.Initialize();
        metric_GameTimeSessionPresenter.Initialize();

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
        sceneRoot.Dispose();

        DeactivateEvents();

        bankPresenter.Dispose();
        particleEffectPresenter.Dispose();

        rouletteBallPresenter.Dispose();
        roulettePresenter.Dispose();

        rouletteValueHistoryPresenter.Dispose();

        timerDailyPresenter.Dispose();
        storeTaskPresenter.Dispose();
        metric_GameCountPresenter.Dispose();
        metric_GameTypeCountPresenter.Dispose();
        metric_GameTimeSessionPresenter.Dispose();

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
