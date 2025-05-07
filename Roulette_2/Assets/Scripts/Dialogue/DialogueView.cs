using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogueView : View
{
    [SerializeField] private List<DialogueDesign> dialogueDesignsPrefabs = new List<DialogueDesign>();
    [SerializeField] private Transform transformSpawnDialogues;

    private DialogueDesign currentDialogueDesign;

    public void Activate()
    {
        
    }

    public void Deactivate()
    {
        currentDialogueDesign?.Deactivate();
        currentDialogueDesign = null;
    }

    public void SetDialogue(Dialogue dialogue)
    {
        var dialogueDesign = GetDialogDesignById(dialogue.IdDesign);

        if(dialogueDesign == null)
        {
            Debug.LogError("Not found dialogue design with id - " + dialogue.IdDesign);
            return;
        }

        currentDialogueDesign?.Deactivate();
        currentDialogueDesign = null;
        currentDialogueDesign = Instantiate(dialogueDesign, transformSpawnDialogues);

        currentDialogueDesign.Initialize();
        currentDialogueDesign.SetData(dialogue.IdPerson, dialogue.Description);
        currentDialogueDesign.Activate();
    }

    private DialogueDesign GetDialogDesignById(int id)
    {
        return dialogueDesignsPrefabs.FirstOrDefault(data => data.Id == id);
    }
}
