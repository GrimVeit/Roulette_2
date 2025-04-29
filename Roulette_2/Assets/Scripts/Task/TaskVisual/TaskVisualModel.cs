using System;

public class TaskVisualModel
{
    public event Action<Task> OnSetActivateTask;
    public event Action<Task> OnSetInactivateTask;
    public event Action<Task> OnSetCompletedTask;

    public event Action<int> OnChooseTask;

    public void SetActivateTask(Task task)
    {
        OnSetActivateTask?.Invoke(task);
    }

    public void SetInactivaTask(Task task)
    {
        OnSetInactivateTask?.Invoke(task);
    }

    public void SetCompletedTask(Task task)
    {
        OnSetCompletedTask?.Invoke(task);
    }



    public void ChooseTask(int number)
    {
        OnChooseTask?.Invoke(number);
    }
        
}
