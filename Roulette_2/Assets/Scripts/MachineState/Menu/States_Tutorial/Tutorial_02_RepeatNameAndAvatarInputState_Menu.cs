using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_02_RepeatNameAndAvatarInputState_Menu : IState
{
    private readonly IGlobalStateMachineProvider _globalStateMachineProvider;
    private readonly UIMainMenuRoot _sceneRoot;
    private readonly NicknamePresenter _nicknamePresenter;
    private readonly AvatarPresenter _avatarPresenter;
    private readonly FirebaseAuthenticationPresenter _firebaseAuthenticationPresenter;
    private readonly FirebaseDatabasePresenter _firebaseDatabasePresenter;

    public Tutorial_02_RepeatNameAndAvatarInputState_Menu(IGlobalStateMachineProvider globalStateMachineProvider, UIMainMenuRoot sceneRoot, NicknamePresenter nicknamePresenter, AvatarPresenter avatarPresenter, FirebaseAuthenticationPresenter firebaseAuthenticationPresenter, FirebaseDatabasePresenter firebaseDatabasePresenter)
    {
        _globalStateMachineProvider = globalStateMachineProvider;
        _sceneRoot = sceneRoot;
        _nicknamePresenter = nicknamePresenter;
        _avatarPresenter = avatarPresenter;
        _firebaseAuthenticationPresenter = firebaseAuthenticationPresenter;
        _firebaseDatabasePresenter = firebaseDatabasePresenter;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - TUTORIAL 02 STATE / MENU</color>");

        _nicknamePresenter.OnChooseNickname += _firebaseAuthenticationPresenter.SetNickname;
        _nicknamePresenter.OnChooseNickname += _firebaseDatabasePresenter.SetNickname;
        _avatarPresenter.OnChooseAvatar += _firebaseDatabasePresenter.SetAvatar;

        _sceneRoot.OnClickToSave_AvatarNickname += ChangeStateToRegistration;

        _sceneRoot.OpenAvatarNicknamePanel();
        _sceneRoot.OpenSaveAvatarDataPanel();
    }

    public void ExitState()
    {
        _nicknamePresenter.OnChooseNickname -= _firebaseAuthenticationPresenter.SetNickname;
        _nicknamePresenter.OnChooseNickname -= _firebaseDatabasePresenter.SetNickname;
        _avatarPresenter.OnChooseAvatar -= _firebaseDatabasePresenter.SetAvatar;

        _sceneRoot.OnClickToSave_AvatarNickname -= ChangeStateToRegistration;

        _sceneRoot.CloseAvatarNicknamePanel();
        _sceneRoot.CloseSaveAvatarDataPanel();
    }

    private void ChangeStateToRegistration()
    {
        _globalStateMachineProvider.SetState(_globalStateMachineProvider.GetState<RegistrationState_Menu>());
    }
}
