using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "TaskGroup", menuName = "Game/Task/New Group")]
public class TaskGroup : ScriptableObject
{
    public List<Task> tasks = new List<Task>();

    public Task GetTaskByNumber(int number)
    {
        return tasks.FirstOrDefault(t => t.Number == number);
    }

    public Task GetTaskByID(string id)
    {
        return tasks.FirstOrDefault(t => t.GetID() == id);
    }
}
