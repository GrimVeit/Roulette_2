using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class StoreTaskModel
{
    public event Action<Task> OnActivate;
    public event Action<Task> OnDeactivate;

    public event Action<Task> OnComplete;

    public event Action<Task> OnInactiveTask;
    public event Action<Task> OnActiveTask;
    public event Action<Task> OnCompletedTask;

    private readonly TaskGroup _taskGroup;
    private List<TaskData> _taskDatas = new();
    public readonly string FilePath = Path.Combine(Application.persistentDataPath, "Task.json");

    private readonly IMoneyProvider _moneyProvider;
    private readonly ISoundProvider _soundProvider;
    private readonly ITimerDailyChangeDay _timerDailyChangeDay;

    public StoreTaskModel(TaskGroup taskGroup, IMoneyProvider moneyProvider, ITimerDailyChangeDay timerDailyChangeDay, ISoundProvider soundProvider)
    {
        _taskGroup = taskGroup;
        _moneyProvider = moneyProvider;
        _timerDailyChangeDay = timerDailyChangeDay;

        _timerDailyChangeDay.OnChangeDay += ChangeTasks;
        _soundProvider = soundProvider;
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
                if(i == 0 || i == 1 || i == 2)
                {
                    _taskDatas.Add(new TaskData(TaskStatus.Inactive, true));
                }
                else
                {
                    _taskDatas.Add(new TaskData(TaskStatus.Inactive, false));
                }
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

            if (task.TaskData.IsActive)
            {
                OnActivate?.Invoke(task);
            }
            else
            {
                OnDeactivate?.Invoke(task);
            }
        }
    }

    public void ChangeTasks()
    {
        var numbers = GetThreeNumbers(new int[] { 0, 1, 2, 3, 4}, 3);
        Debug.Log(string.Join(", ", numbers));

        _taskGroup.tasks.ForEach(task => 
        {
            task.TaskData.TaskStatus = TaskStatus.Inactive;
            OnInactiveTask?.Invoke(task);

            task.TaskData.IsActive = false;
            OnDeactivate?.Invoke(task);
        });

        for (int i = 0; i < numbers.Length; i++)
        {
            var task = _taskGroup.GetTaskByNumber(numbers[i]);
            task.TaskData.IsActive = true;
            OnActivate?.Invoke(task);
        }
    }

    public void Dispose()
    {
        _timerDailyChangeDay.OnChangeDay -= ChangeTasks;

        string json = JsonUtility.ToJson(new TaskDatas(_taskDatas.ToArray()));
        File.WriteAllText(FilePath, json);
    }

    public void ActivateTask(string id)
    {
        var task = _taskGroup.GetTaskByID(id);

        if(task.TaskData.TaskStatus != TaskStatus.Inactive || task.TaskData.IsActive == false) return;

        task.TaskData.SetStatus(TaskStatus.Active);
        OnComplete?.Invoke(task);
        OnActiveTask?.Invoke(task);
    }

    public void CompletedTask(int number)
    {
        var task = _taskGroup.GetTaskByNumber(number);

        if(task.TaskData.TaskStatus != TaskStatus.Active || task.TaskData.TaskStatus == TaskStatus.Completed) return;

        task.TaskData.SetStatus(TaskStatus.Completed);
        _moneyProvider.SendMoney(task.Bonus);
        _soundProvider.PlayOneShot("DailyBonus");
        OnCompletedTask?.Invoke(task);
    }

    private int[] GetThreeNumbers(int[] intArray, int count)
    {
        if(intArray.Length < count)
        {
            Debug.LogWarning("Error");
            return null;
        }

        List<int> numbers = new(intArray);

        for (int i = 0; i < numbers.Count; i++)
        {
            int rndIndex = Random.Range(i, numbers.Count);
            int temp = numbers[i];
            numbers[i] = numbers[rndIndex];
            numbers[rndIndex] = temp;
        }

        return numbers.GetRange(0, count).ToArray();
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
    public bool IsActive;

    public TaskData(TaskStatus taskStatus, bool isActive)
    {
        SetStatus(taskStatus);
        IsActive = isActive;
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
