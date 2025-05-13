using System;
using UnityEngine;

public class DialogueModel
{
    public event Action OnActivate;
    public event Action OnDeactivate;
    public event Action<Dialogue> OnChangeDialogue;

    private readonly DialogueGroup _dialogueGroup;

    private int _dialogueIndex = 0;

    private readonly ISoundProvider _soundProvider;

    public DialogueModel(DialogueGroup dialogueGroup, ISoundProvider soundProvider)
    {
        _dialogueGroup = dialogueGroup;
        _soundProvider = soundProvider;
    }

    public void Activate()
    {
        var dialogue = _dialogueGroup.dialogues[_dialogueIndex];

        if (dialogue == null)
        {
            Debug.LogError("Not found dialogue with id - " + _dialogueIndex);
            return;
        }

        OnActivate?.Invoke();
        OnChangeDialogue?.Invoke(dialogue);
        _soundProvider.PlayOneShot("TutorNext");
    }

    public void Deactivate()
    {
        OnDeactivate?.Invoke();
    }

    public void Next()
    {
        _dialogueIndex += 1;

        var dialogue = _dialogueGroup.dialogues[_dialogueIndex];

        if(dialogue == null)
        {
            Debug.LogError("Not found dialogue with id - " + _dialogueIndex);
            return;
        }

        OnChangeDialogue?.Invoke(dialogue);
        _soundProvider.PlayOneShot("TutorNext");
    }
}