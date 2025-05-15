using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegistrationState_Menu : IState
{
    private readonly IGlobalStateMachineProvider _globalStateMachineProvider;
    private readonly UIMainMenuRoot _sceneRoot;
    private readonly FirebaseAuthenticationPresenter _firebaseAuthenticationPresenter;
    private readonly FirebaseDatabasePresenter _firebaseDatabasePresenter;

    public RegistrationState_Menu(IGlobalStateMachineProvider globalStateMachineProvider, UIMainMenuRoot sceneRoot, FirebaseAuthenticationPresenter firebaseAuthenticationPresenter, FirebaseDatabasePresenter firebaseDatabasePresenter)
    {
        _globalStateMachineProvider = globalStateMachineProvider;
        _sceneRoot = sceneRoot;
        _firebaseAuthenticationPresenter = firebaseAuthenticationPresenter;
        _firebaseDatabasePresenter = firebaseDatabasePresenter;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - REGISTRATION STATE / MENU</color>");

        _firebaseAuthenticationPresenter.OnSignUp += _firebaseDatabasePresenter.CreateEmptyDataToServer;
        _firebaseAuthenticationPresenter.OnSignUp += ChangeStateTo_03;

        _firebaseAuthenticationPresenter.OnSignUpError += ChangeStateTo02;

        _firebaseAuthenticationPresenter.SignUp();

        _sceneRoot.OpenLoadRegistrationPanel();
    }

    public void ExitState()
    {
        _firebaseAuthenticationPresenter.OnSignUp -= _firebaseDatabasePresenter.CreateEmptyDataToServer;
        _firebaseAuthenticationPresenter.OnSignUp -= ChangeStateTo_03;

        _firebaseAuthenticationPresenter.OnSignUpError -= ChangeStateTo02;

        _sceneRoot.CloseLoadRegistrationPanel();
    }

    private void ChangeStateTo02()
    {
        _globalStateMachineProvider.SetState(_globalStateMachineProvider.GetState<Tutorial_02_RepeatNameAndAvatarInputState_Menu>());
    }

    private void ChangeStateTo_03()
    {
        _globalStateMachineProvider.SetState(_globalStateMachineProvider.GetState<Tutorial_03_IntroConfirmationState_Menu>());
    }
}
