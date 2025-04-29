using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Task", menuName = "Game/Task/New Task")]
public class Task : ScriptableObject, IIdentify
{
    [SerializeField] private string id;
    [SerializeField] private int number;
    [SerializeField] private string description;
    [SerializeField] private int bonus;
    private TaskData _taskData;

    public string GetID() => id;
    public int Number => number;
    public string Description => description;
    public int Bonus => bonus;
    public TaskData TaskData => _taskData;
    public void SetTaskData(TaskData taskData)
    {
        _taskData = taskData;
    }
}
