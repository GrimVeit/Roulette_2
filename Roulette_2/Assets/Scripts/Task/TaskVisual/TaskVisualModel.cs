using System;

public class TaskVisualModel
{
    public event Action<int> OnActivate;
    public event Action<int> OnDeactivate;

    public event Action<Task> OnSetActivateTask;
    public event Action<Task> OnSetInactivateTask;
    public event Action<Task> OnSetCompletedTask;

    private readonly ITaskProviderEvents _taskEventsProvider;
    private readonly ICompleteTaskProvider _taskCompletedProvider;

    public TaskVisualModel(ITaskProviderEvents taskEventsProvider, ICompleteTaskProvider completeTaskProvider, ISoundProvider soundProvider)
    {
        _taskEventsProvider = taskEventsProvider;
        _taskCompletedProvider = completeTaskProvider;

        _taskEventsProvider.OnActivate += Activate;
        _taskEventsProvider.OnDeactivate += Deactivate;
        _taskEventsProvider.OnActiveTask += SetActivateTask;
        _taskEventsProvider.OnInactiveTask += SetInactivaTask;
        _taskEventsProvider.OnCompletedTask += SetCompletedTask;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {
        _taskEventsProvider.OnActivate -= Activate;
        _taskEventsProvider.OnDeactivate -= Deactivate;
        _taskEventsProvider.OnActiveTask -= SetActivateTask;
        _taskEventsProvider.OnInactiveTask -= SetInactivaTask;
        _taskEventsProvider.OnCompletedTask -= SetCompletedTask;
    }

    private void Activate(Task task)
    {
        OnActivate?.Invoke(task.Number);
    }

    private void Deactivate(Task task)
    {
        OnDeactivate?.Invoke(task.Number);
    }

    private void SetActivateTask(Task task)
    {
        OnSetActivateTask?.Invoke(task);
    }

    private void SetInactivaTask(Task task)
    {
        OnSetInactivateTask?.Invoke(task);
    }

    private void SetCompletedTask(Task task)
    {
        OnSetCompletedTask?.Invoke(task);
    }



    public void ChooseTask(int number)
    {
        _taskCompletedProvider.CompletedTask(number);
    }
        
}
