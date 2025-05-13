using System.Collections;
using UnityEngine;

public class Tutorial_02_NameAndAvatarInputState_Menu : IState
{
    private readonly IGlobalStateMachineProvider _globalStateMachineProvider;
    private readonly DialoguePresenter _dialoguePresenter;
    private readonly UIMainMenuRoot _sceneRoot;

    public Tutorial_02_NameAndAvatarInputState_Menu(IGlobalStateMachineProvider globalStateMachineProvider, DialoguePresenter dialoguePresenter, UIMainMenuRoot sceneRoot)
    {
        _globalStateMachineProvider = globalStateMachineProvider;
        _dialoguePresenter = dialoguePresenter;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - TUTORIAL 02 STATE / MENU</color>");

        _sceneRoot.OnClickToSave_AvatarNickname += ChangeStateTo_03;

        _dialoguePresenter.Next();

        _sceneRoot.OpenAvatarNicknamePanel();
        _sceneRoot.OpenSaveAvatarDataPanel();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToSave_AvatarNickname -= ChangeStateTo_03;
    }

    private void ChangeStateTo_03()
    {
        _globalStateMachineProvider.SetState(_globalStateMachineProvider.GetState<Tutorial_03_IntroConfirmationState_Menu>());
    }
}
