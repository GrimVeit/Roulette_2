using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TaskVisualView : View
{
    [SerializeField] private List<TaskVisual> taskVisuals = new List<TaskVisual>();

    public void Initialize()
    {
        taskVisuals.ForEach((tv) =>
        {
            tv.OnChooseTask += HandleChooseActiveTask;
            tv.Initialize();

        });
    }

    public void Dispose()
    {
        taskVisuals.ForEach((tv) =>
        {
            tv.OnChooseTask -= HandleChooseActiveTask;
            tv.Dispose();

        });
    }

    #region Input

    public void SetActivateTask(Task task)
    {
        var taskVisual = GetTaskVisualByNumber(task.Number);

        if (taskVisual == null) return;

        taskVisual.Activate();
    }

    public void SetDeactivateTask(Task task)
    {
        var taskVisual = GetTaskVisualByNumber(task.Number);

        if (taskVisual == null) return;

        taskVisual.Deactivate();
    }

    public void SetCompletedTask(Task task)
    {
        var taskVisual = GetTaskVisualByNumber(task.Number);

        if(taskVisual == null) return;

        taskVisual.Complete();
    }

    private TaskVisual GetTaskVisualByNumber(int number)
    {
        var taskVisual = taskVisuals.FirstOrDefault(tv => tv.TaskNumber == number);

        if (taskVisual == null)
        {
            Debug.LogWarning($"Not found Task Visual with Number - {number}");
            return null;
        }

        return taskVisual;
    }

    #endregion

    #region Output

    public event Action<int> OnChooseActiveTask;

    private void HandleChooseActiveTask(int number)
    {
        OnChooseActiveTask?.Invoke(number);
    }

    #endregion
}
