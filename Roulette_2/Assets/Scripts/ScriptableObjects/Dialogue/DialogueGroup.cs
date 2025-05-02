using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueGroup", menuName = "Game/Dialogue/DialogueGroup")]
public class DialogueGroup : ScriptableObject
{
    public List<Dialogue> dialogues = new List<Dialogue>();
}
