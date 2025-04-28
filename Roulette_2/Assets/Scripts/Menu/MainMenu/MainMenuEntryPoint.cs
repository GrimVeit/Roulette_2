using System;
using UnityEngine;

public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private DailyRewardValues dailyRewardValues;
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
        sceneRoot.OnClickToBack_DailyReward += sceneRoot.OpenMainPanel;
        sceneRoot.OnClickToBack_Tasks += sceneRoot.OpenMainPanel;

        sceneRoot.OnClickToDailyReward_Main += sceneRoot.OpenDailyRewardPanel;
        sceneRoot.OnClickToTasks_Main += sceneRoot.OpenTasksPanel;
    }

    private void DeactivateTransitions()
    {
        sceneRoot.OnClickToBack_DailyReward -= sceneRoot.OpenMainPanel;
        sceneRoot.OnClickToBack_Tasks -= sceneRoot.OpenMainPanel;

        sceneRoot.OnClickToDailyReward_Main -= sceneRoot.OpenDailyRewardPanel;
        sceneRoot.OnClickToTasks_Main -= sceneRoot.OpenTasksPanel;
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
    }

    private void OnDestroy()
    {
        Dispose();
    }

    #region Output

    public event Action OnGoToGame;

    private void HandleGoToGame()
    {
        Deactivate();
        OnGoToGame?.Invoke();
    }

    #endregion
}
