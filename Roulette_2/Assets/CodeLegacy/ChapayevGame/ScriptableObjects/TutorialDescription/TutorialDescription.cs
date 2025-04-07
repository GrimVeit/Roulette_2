using UnityEngine;

[CreateAssetMenu(fileName = "TutorialDescription", menuName = "Game/Tutuorial Description/New Tutuoral Description")]
public class TutorialDescription : ScriptableObject, IIdentify
{
    [SerializeField] private string id;
    [SerializeField] private string description;
    [SerializeField] private Vector3 vectorFrom;
    [SerializeField] private Vector3 vectorTo;
    private TutorialDescriptionData data;

    public string Description => description;
    public TutorialDescriptionData Data => data;
    public string GetID() => id;
    public Vector3 GetVectorFrom() => vectorFrom;
    public Vector3 GetVectorTo() => vectorTo;

    internal void SetData(TutorialDescriptionData data)
    {
        this.data = data;
    }
}
