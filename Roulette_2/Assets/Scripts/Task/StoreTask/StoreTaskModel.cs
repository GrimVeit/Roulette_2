using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class StoreTaskModel
{
    public event Action<Task> OnInactiveTask;
    public event Action<Task> OnActiveTask;
    public event Action<Task> OnCompletedTask;

    private readonly TaskGroup _taskGroup;
    private List<TaskData> _taskDatas = new();
    public readonly string FilePath = Path.Combine(Application.persistentDataPath, "Task.json");

    private readonly IMoneyProvider _moneyProvider;

    public StoreTaskModel(TaskGroup taskGroup, IMoneyProvider moneyProvider)
    {
        _taskGroup = taskGroup;
        _moneyProvider = moneyProvider;
    }

    public void Initialize()
    {
        if (File.Exists(FilePath))
        {
            string loadedJson = File.ReadAllText(FilePath);
            TaskDatas taskDatas = JsonUtility.FromJson<TaskDatas>(loadedJson);
            _taskDatas = taskDatas.Datas.ToList();
        }
        else
        {
            _taskDatas = new List<TaskData>();

            for (int i = 0; i < _taskGroup.tasks.Count; i++)
            {
                _taskDatas.Add(new TaskData(TaskStatus.Inactive));
            }
        }

        for (int i = 0; i < _taskGroup.tasks.Count; i++)
        {
            var task = _taskGroup.tasks[i];

            task.SetTaskData(_taskDatas[i]);

            switch (task.TaskData.TaskStatus)
            {
                case TaskStatus.Inactive:
                    OnInactiveTask?.Invoke(task);
                    break;
                case TaskStatus.Active:
                    OnActiveTask?.Invoke(task);
                    break;
                case TaskStatus.Completed:
                    OnCompletedTask?.Invoke(task);
                    break;
            }
        }
    }

    public void Dispose()
    {
        string json = JsonUtility.ToJson(new TaskDatas(_taskDatas.ToArray()));
        File.WriteAllText(FilePath, json);
    }

    public void ActivateTask(string id)
    {
        var task = _taskGroup.GetTaskByID(id);

        if(task.TaskData.TaskStatus != TaskStatus.Inactive) return;

        task.TaskData.SetStatus(TaskStatus.Active);
        OnActiveTask?.Invoke(task);
    }

    public void CompletedTask(int number)
    {
        var task = _taskGroup.GetTaskByNumber(number);

        if(task.TaskData.TaskStatus != TaskStatus.Active) return;

        task.TaskData.SetStatus(TaskStatus.Completed);
        _moneyProvider.SendMoney(task.Bonus);
        OnCompletedTask?.Invoke(task);
    }
}

[Serializable]
public class TaskDatas
{
    public TaskData[] Datas;

    public TaskDatas(TaskData[] datas)
    {
        Datas = datas;
    }
}

[Serializable]
public class TaskData
{
    public TaskStatus TaskStatus;

    public TaskData(TaskStatus taskStatus)
    {
        SetStatus(taskStatus);
    }

    public void SetStatus(TaskStatus taskStatus)
    {
        TaskStatus = taskStatus;
    }
}

public enum TaskStatus
{
    Inactive, Active, Completed
}
