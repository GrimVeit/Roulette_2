using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine_Menu : IGlobalStateMachineProvider
{
    private readonly Dictionary<Type, IState> states = new();

    private IState _currentState;

    public StateMachine_Menu
        (UIMainMenuRoot sceneRoot,
        NicknamePresenter nicknamePresenter,
        AvatarPresenter avatarPresenter,
        FirebaseAuthenticationPresenter firebaseAuthenticationPresenter,
        FirebaseDatabasePresenter firebaseDatabasePresenter,
        DialoguePresenter dialoguePresenter,
        IHandPointerProvider handPointerProvider,
        ITutorialProgressProvider_Read tutorialProgressProvider_Read,
        ITutorialProgressProvider_Write tutorialProgressProvider_Write,
        IGameProgressProvider_Write gameProgressProvider_Write)
    {
        states[typeof(CheckTutorialState_Menu)] = new CheckTutorialState_Menu(this, tutorialProgressProvider_Read);
        states[typeof(Tutorial_01_IntroGreetingState_Menu)] = new Tutorial_01_IntroGreetingState_Menu(this, dialoguePresenter);
        states[typeof(Tutorial_02_NameAndAvatarInputState_Menu)] = new Tutorial_02_NameAndAvatarInputState_Menu(this, dialoguePresenter, sceneRoot, nicknamePresenter, avatarPresenter, firebaseAuthenticationPresenter, firebaseDatabasePresenter);
        states[typeof(RegistrationState_Menu)] = new RegistrationState_Menu(this, sceneRoot, firebaseAuthenticationPresenter, firebaseDatabasePresenter);
        states[typeof(Tutorial_02_RepeatNameAndAvatarInputState_Menu)] = new Tutorial_02_RepeatNameAndAvatarInputState_Menu(this, sceneRoot, nicknamePresenter, avatarPresenter, firebaseAuthenticationPresenter, firebaseDatabasePresenter);
        states[typeof(Tutorial_03_IntroConfirmationState_Menu)] = new Tutorial_03_IntroConfirmationState_Menu(this, dialoguePresenter, sceneRoot);
        states[typeof(Tutorial_04_ShowBalanceState_Menu)] = new Tutorial_04_ShowBalanceState_Menu(this, dialoguePresenter, sceneRoot, handPointerProvider);
        states[typeof(Tutorial_05_HighlightBonusBtnState_Menu)] = new Tutorial_05_HighlightBonusBtnState_Menu(this, dialoguePresenter, sceneRoot, handPointerProvider);
        states[typeof(Tutorial_06_DailyBonus1State_Menu)] = new Tutorial_06_DailyBonus1State_Menu(this, dialoguePresenter, sceneRoot, handPointerProvider);
        states[typeof(Tutorial_07_DailyBonus2State_Menu)] = new Tutorial_07_DailyBonus2State_Menu(this, dialoguePresenter, handPointerProvider);
        states[typeof(Tutorial_08_HighlightTasksBtnState_Menu)] = new Tutorial_08_HighlightTasksBtnState_Menu(this, dialoguePresenter, sceneRoot, handPointerProvider);
        states[typeof(Tutorial_09_DailyTasksState_Menu)] = new Tutorial_09_DailyTasksState_Menu(this, dialoguePresenter, sceneRoot);
        states[typeof(Tutorial_10_CompleteState_Menu)] = new Tutorial_10_CompleteState_Menu(this, dialoguePresenter, tutorialProgressProvider_Write, sceneRoot, gameProgressProvider_Write);


        states[typeof(StartMainState_Menu)] = new StartMainState_Menu(this, firebaseDatabasePresenter, firebaseAuthenticationPresenter);
        states[typeof(MainState_Menu)] = new MainState_Menu(this, sceneRoot);
        states[typeof(DailyRewardState_Menu)] = new DailyRewardState_Menu(this, sceneRoot);
        states[typeof(LeaderboardState_Menu)] = new LeaderboardState_Menu(this, sceneRoot);
        states[typeof(DailyTasksState_Menu)] = new DailyTasksState_Menu(this, sceneRoot);
        states[typeof(ChipStoreState_Menu)] = new ChipStoreState_Menu(this, sceneRoot);
    }

    public void Initialize()
    {
        SetState(GetState<CheckTutorialState_Menu>());
    }

    public void Dispose()
    {

    }

    public IState GetState<T>() where T : IState
    {
        return states[typeof(T)];
    }

    public void SetState(IState state)
    {
        _currentState?.ExitState();

        _currentState = state;
        _currentState.EnterState();
    }
}
