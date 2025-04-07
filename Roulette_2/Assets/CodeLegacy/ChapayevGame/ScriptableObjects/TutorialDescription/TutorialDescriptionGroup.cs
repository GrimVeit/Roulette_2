using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "TutorialDescriptionGroup", menuName = "Game/Tutuorial Description/New Group")]
public class TutorialDescriptionGroup : ScriptableObject
{
    public List<TutorialDescription> TutorialDescriptions = new();

    public TutorialDescription GetTutorialById(string id)
    {
        return TutorialDescriptions.FirstOrDefault(t => t.GetID() == id);
    }
}
